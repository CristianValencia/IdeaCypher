using System;

namespace IdeaCipher
{
    /// <summary>
    /// Crea una instancia del procesado de cifrado IDEA.
    /// </summary>
    public class Idea
    {
        #region Campos
        //Una ronda completa consta de una serie de 14 pasos.
        internal static int rounds = 8;
        // llave criptográfica interna de cifrado.
        internal int[] subKey;
        #endregion

        #region Constructor
        /// <summary>
        /// Inicialización de las llaves a partir de los parámetros de entrada.
        /// </summary>
        /// <param name="charKey">Una llave binaria de 16 bytes.</param>
        /// <param name="encrypt">Indica si se desea encriptar o desencriptar. Verdadero para encriptar, falso para desencriptar.</param>
        public Idea(String charKey, bool encrypt)
        {
            byte[] key = GenerateUserKeyFromCharKey(charKey);
            int[] tempSubKey = ExpandUserKey(key);

            if (encrypt)
            {
                subKey = tempSubKey;
            }
            else
            {
                subKey = InvertSubKey(tempSubKey);
            }
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Encripta o desencripta un bloque de 8 bytes de datos.
        /// </summary>
        /// <param name="data">Un arreglo de bytes que contiene los 8 bytes de datos a ser enncriptados o desencriptados.</param>
        public void Crypt(byte[] data)
        {
            Crypt(data, dataPos: 0);
        }

        /// <summary>
        /// Encripta o desencripta un bloque de 8 bytes de datos.
        /// </summary>
        /// <param name="data">Un arreglo de bytes que contiene los 8 bytes de datos a ser enncriptados o desencriptados.</param>
        /// <param name="dataPos">Posición inicial de los 8 bytes dentro del buffer.</param>
        public void Crypt(byte[] data, int dataPos)
        {

            int x0 = ((data[dataPos + 0] & 0xFF) << 8) | (data[dataPos + 1] & 0xFF);
            int x1 = ((data[dataPos + 2] & 0xFF) << 8) | (data[dataPos + 3] & 0xFF);
            int x2 = ((data[dataPos + 4] & 0xFF) << 8) | (data[dataPos + 5] & 0xFF);
            int x3 = ((data[dataPos + 6] & 0xFF) << 8) | (data[dataPos + 7] & 0xFF);


            int subkeyPosition = 0;
            for (int round = 0; round < rounds; round++)
            {
                int y0 = Multiplication(x0, subKey[subkeyPosition++]);
                int y1 = Add(x1, subKey[subkeyPosition++]);
                int y2 = Add(x2, subKey[subkeyPosition++]);
                int y3 = Multiplication(x3, subKey[subkeyPosition++]);

                int t0 = Multiplication(y0 ^ y2, subKey[subkeyPosition++]);
                int t1 = Add(y1 ^ y3, t0);
                int t2 = Multiplication(t1, subKey[subkeyPosition++]);
                int t3 = Add(t0, t2);

                x0 = y0 ^ t2;
                x1 = y2 ^ t2;
                x2 = y1 ^ t3;
                x3 = y3 ^ t3;
            }

            int r0 = Multiplication(x0, subKey[subkeyPosition++]);
            int r1 = Add(x2, subKey[subkeyPosition++]);
            int r2 = Add(x1, subKey[subkeyPosition++]);
            int r3 = Multiplication(x3, subKey[subkeyPosition++]);

            data[dataPos + 0] = (byte)(r0 >> 8);
            data[dataPos + 1] = (byte)r0;
            data[dataPos + 2] = (byte)(r1 >> 8);
            data[dataPos + 3] = (byte)r1;
            data[dataPos + 4] = (byte)(r2 >> 8);
            data[dataPos + 5] = (byte)r2;
            data[dataPos + 6] = (byte)(r3 >> 8);
            data[dataPos + 7] = (byte)r3;
        }

        /// <summary>
        /// Expande la llave del usuario, que debe ser de 16 bytes a las llaves de enciptación internas.
        /// </summary>
        /// <param name="userKey">Llave de 16 bytes.</param>
        /// <returns>Un arreglo de subllaves.</returns>
        private static int[] ExpandUserKey(byte[] userKey)
        {
            if (userKey.Length != 16)
            {
                throw new ArgumentException("Key length must be 128 bit", "key");
            }

            int[] key = new int[rounds * 6 + 4];

            for (int i = 0; i < userKey.Length / 2; i++)
            {
                key[i] = ((userKey[2 * i] & 0xFF) << 8) | (userKey[2 * i + 1] & 0xFF);
            }

            for (int i = userKey.Length / 2; i < key.Length; i++)
            {
                key[i] = ((key[(i + 1) % 8 != 0 ? i - 7 : i - 15] << 9) | (key[(i + 2) % 8 < 2 ? i - 14 : i - 6] >> 7)) & 0xFFFF;
            }

            return key;
        }

        /// <summary>
        /// Invierte las llaves de encriptación y desencriptación de la siguiente manera:
        /// encriptación -> desencriptación.
        /// desencriptación -> encriptación.
        /// </summary>
        /// <param name="userKey">Llave de 16 bytes.</param>
        /// <returns>La sub-llave invertida.</returns>
        private static int[] InvertSubKey(int[] key)
        {
            int[] invKey = new int[key.Length];
            int p = 0;
            int i = rounds * 6;

            invKey[i + 0] = MultiplicativeInverse(key[p++]);
            invKey[i + 1] = AddInverse(key[p++]);
            invKey[i + 2] = AddInverse(key[p++]);
            invKey[i + 3] = MultiplicativeInverse(key[p++]);

            for (int r = rounds - 1; r >= 0; r--)
            {
                i = r * 6;
                int m = r > 0 ? 2 : 1;
                int n = r > 0 ? 1 : 2;
                invKey[i + 4] = key[p++];
                invKey[i + 5] = key[p++];
                invKey[i + 0] = MultiplicativeInverse(key[p++]);
                invKey[i + m] = AddInverse(key[p++]);
                invKey[i + n] = AddInverse(key[p++]);
                invKey[i + 3] = MultiplicativeInverse(key[p++]);
            }
            return invKey;
        }

        /// <summary>
        /// Suma de dos valores que queden en un rango 0 - 0xFFFF.
        /// </summary>
        /// <param name="a">Valor 1.</param>
        /// <param name="b">Valor 2.</param>
        /// <returns>Entero, resultado de la suma en el rango de 0 - 0xFFFF</returns>
        private static int Add(int a, int b)
        {
            return (a + b) & 0xFFFF;
        }

        /// <summary>
        /// Multiplicación del grupo multiplicativo.
        /// </summary>
        /// <param name="a">Valor 1.</param>
        /// <param name="b">Valor 2.</param>
        /// <returns>Entero, resultado de la multiplicación en el rango 0-0xFFFF.</returns>
        private static int Multiplication(int a, int b)
        {
            long r = (long)a * b;
            if (r != 0)
            {
                return (int)(r % 0x10001) & 0xFFFF;
            }
            else
            {
                return (1 - a - b) & 0xFFFF;
            }
        }

        /// <summary>
        /// Suma de dos valores que queden en un rango 0 - 0xFFFF. El resultado de invierte del top 0x10000.
        /// </summary>
        /// <param name="a">Valor 1.</param>
        /// <param name="b">Valor 2.</param>
        /// <returns>Entero, resultado de la suma inversa en el rango de 0 - 0xFFFF</returns>
        private static int AddInverse(int x)
        {
            return (0x10000 - x) & 0xFFFF;
        }

        /// <summary>
        /// Multiplicación del grupo multiplicativo, a la inversa.
        /// La condición es válida para todos los valores del valor inicial iguales a 1.
        /// </summary>
        /// <param name="a">Valor 1.</param>
        /// <param name="b">Valor 2.</param>
        /// <returns>Entero, resultado de la multiplicación en el rango 0-0xFFFF.</returns>
        private static int MultiplicativeInverse(int x)
        {
            if (x <= 1)
            {
                return x;
            }

            int y = 0x10001;
            int t0 = 1;
            int t1 = 0;

            while (true)
            {
                t1 += y / x * t0;
                y %= x;

                if (y == 1)
                {
                    return 0x10001 - t1;
                }

                t0 += x / y * t1;
                x %= y;

                if (x == 1)
                {
                    return t0;
                }
            }
        }

        /// <summary>
        /// Genera una llave binaria de 16 bytes de una llave, la cual es una cadena de caracteres.
        /// </summary>
        /// <param name="charKey">La cadena de carateres de entrada.</param>
        /// <returns>La llave generada.</returns>
        private static byte[] GenerateUserKeyFromCharKey(string charKey)
        {
            //Número de los distintos caracteres válidos.
            int nofChar = 0x7E - 0x21 + 1;
            int[] a = new int[8];

            for (int p = 0; p < charKey.Length; p++)
            {
                int c = charKey[p];

                for (int i = a.Length - 1; i >= 0; i--)
                {
                    c += a[i] * nofChar;
                    a[i] = c & 0xFFFF;
                    c >>= 16;
                }
            }

            byte[] key = new byte[16];

            for (int i = 0; i < 8; i++)
            {
                key[i * 2] = (byte)(a[i] >> 8);
                key[i * 2 + 1] = (byte)a[i];
            }
            return key;
        }
		#endregion
	}
}
