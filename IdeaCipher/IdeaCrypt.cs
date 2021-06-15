using System;
using System.IO;

namespace IdeaCipher
{

    /// <summary>
    /// Clase para el procesado de archivos. 
    /// </summary>
    public class IdeaCrypt
    {
        #region Campos estáticos.
        private static int blockSize = 8;
        #endregion


        /// <summary>
        /// Encripta o desencripta un archivo.
        /// </summary>
        /// <param name="inputFileName">Nombre del archivo de entrada.</param>
        /// <param name="outputFileName">Nombre del archivo de salida.</param>
        /// <param name="charKey">Llave de cifrado.</param>
        /// <param name="encrypt">Modo de operación: encripta o desencripta.</param>
        public static void cryptFile(string inputFileName, string outputFileName, string charKey, bool encrypt)
        {
            FileStream inStream = null;
            FileStream outStream = null;

            try
            {
                Idea idea = new Idea(charKey, encrypt);
                BlockStreamCrypter bsc = new BlockStreamCrypter(idea, encrypt);
                inStream = new FileStream(inputFileName, FileMode.Open, FileAccess.ReadWrite);
                long inFileSize = inStream.Length;
                long inDataLen;
                long outDataLen;
                if (encrypt)
                {
                    inDataLen = inFileSize;
                    outDataLen = (inDataLen + blockSize - 1) / blockSize * blockSize;
                }
                else
                {
                    if (inFileSize == 0)
                    {
                        throw new IOException("El archivo está vacío.");
                    }

                    if (inFileSize % blockSize != 0)
                    {
                        throw new IOException("El archivo de entrada no es un múltiplo de " + blockSize + ".");
                    }

                    inDataLen = inFileSize - blockSize;
                    outDataLen = inDataLen;
                }

                outStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write);
                ProcessData(inStream, inDataLen, outStream, outDataLen, bsc);

                if (encrypt)
                {
                    WriteDataLength(outStream, inDataLen, bsc);
                }
                else
                {
                    long outFileSize = readDataLength(inStream, bsc);
                    if (outFileSize < 0 || outFileSize > inDataLen || outFileSize < inDataLen - blockSize + 1)
                    {
                        throw new IOException("El archivo no ha sido firmado con la misma llave.");
                    }
                    if (outFileSize != outDataLen)
                    {
                        outStream.SetLength(outFileSize);
                    }
                }
                outStream.Close();
            }
            finally
            {
                if (inStream != null)
                {
                    inStream.Close();
                }
                if (outStream != null)
                {
                    outStream.Close();
                }
            }
        }

        /// <summary>
        /// Genera bloques de cifrado con el stream de datos.
        /// </summary>
        private class BlockStreamCrypter
        {
            Idea idea;
            bool encrypt;

            byte[] prev;
            byte[] newPrev;


            public BlockStreamCrypter(Idea idea, bool encrypt)
            {
                this.idea = idea;
                this.encrypt = encrypt;
                prev = new byte[blockSize];
                newPrev = new byte[blockSize];
            }


            public void Crypt(byte[] data, int pos)
            {
                if (encrypt)
                {
                    Xor(data, pos, prev);
                    idea.Crypt(data, pos);
                    Array.Copy(data, pos, prev, 0, blockSize);
                }
                else
                {
                    Array.Copy(data, pos, newPrev, 0, blockSize);
                    idea.Crypt(data, pos);
                    Xor(data, pos, prev);
                    byte[] temp = prev;
                    prev = newPrev;
                    newPrev = temp;
                }
            }
        }

        /// <summary>
        /// Se encarga de procesar los datos de los archivos.
        /// </summary>
        private static void ProcessData(FileStream inStream, long inDataLen, FileStream outStream, long outDataLen, BlockStreamCrypter blockStreamCrypter)
        {
            int bufSize = 0x200000;
            byte[] buffer = new byte[bufSize];
            long filePos = 0;
            while (filePos < inDataLen)
            {
                int reqLen = (int)Math.Min(inDataLen - filePos, bufSize);
                int trLen = inStream.Read(buffer, 0, reqLen);
                if (trLen != reqLen)
                {
                    throw new Exception("Hubo un error en la lectura de datos, ya que han sido leidos de manera incompleta.");
                }
                int chunkLength = (trLen + blockSize - 1) / blockSize * blockSize;
                for (int i = trLen; i <= chunkLength; i++)
                {
                    buffer[i] = 0;
                }
                for (int pos = 0; pos < chunkLength; pos += blockSize)
                {
                    blockStreamCrypter.Crypt(buffer, pos);
                }
                reqLen = (int)Math.Min(outDataLen - filePos, chunkLength);

                outStream.Write(buffer, 0, reqLen);

                filePos += chunkLength;
            }
        }

        /// <summary>
        /// Hace una operación XOR con los bytes y la posición.
        /// </summary>
        /// <param name="a">Arreglo de bytes 1.</param>
        /// <param name="position">Posición</param>
        /// <param name="b">Arreglo de bytes 2.</param>
        private static void Xor(byte[] a, int position, byte[] b)
        {
            for (int i = 0; i < blockSize; i++)
            {
                a[position + i] ^= b[i];
            }
        }

        /// <summary>
        /// Lee la longitud del stream de datos.
        /// </summary>
        /// <param name="stream">El stream.</param>
        /// <param name="bsc">El bloque de cifrado.</param>
        /// <returns></returns>
        private static long readDataLength(FileStream stream, BlockStreamCrypter bsc)
        {
            byte[] buffer = new byte[blockSize];
            int trLen = stream.Read(buffer, 0, blockSize);
            if (trLen != blockSize)
            {
                throw new Exception("No se pudo leer el stream de datos.");
            }
            bsc.Crypt(buffer, 0);
            return unpackDataLength(buffer);
        }

        /// <summary>
        /// Escribe los datos con la longitud dada.
        /// </summary>
        /// <param name="stream">Stream de datos.</param>
        /// <param name="dataLength">Longitud del dato</param>
        /// <param name="bsc">Bloque de cifrado.</param>
        private static void WriteDataLength(FileStream stream, long dataLength, BlockStreamCrypter bsc)
        {
            byte[] a = packDataLength(dataLength);
            bsc.Crypt(a, 0);
            stream.Write(a, 0, blockSize);
        }


        /// <summary>
        /// Empaqueta el entero en un bloque de 8 bytes usado para coodificar el tamaño del archivo.
        /// </summary>
        /// <param name="i">Longitud del archivo.</param>
        /// <returns>El arreglo de bytes.</returns>
        private static byte[] packDataLength(long i)
        {
            if (i > 0x1FFFFFFFFFFFL) // 45 bits
            {
                throw new ArgumentException("Lo sentimos, usa un texto menos largo.");
            }
            byte[] b = new byte[blockSize];
            b[7] = (byte)(i << 3);
            b[6] = (byte)(i >> 5);
            b[5] = (byte)(i >> 13);
            b[4] = (byte)(i >> 21);
            b[3] = (byte)(i >> 29);
            b[2] = (byte)(i >> 37);
            return b;
        }

        /// <summary>
        /// Empaqueta el entero en un bloque de 8 bytes usado para coodificar el tamaño del archivo.
        /// </summary>
        /// <param name="i">Longitud del archivo.</param>
        /// <returns>El arreglo de bytes.
        /// Si se regresa -1, el valor codificado es invalido, lo que significa que el archivo de entrada no es un criptograma válido.</returns>
        private static long unpackDataLength(byte[] b)
        {
            if (b[0] != 0 || b[1] != 0 || (b[7] & 7) != 0)
            {
                return -1;
            }
            return
               (long)(b[7] & 0xFF) >> 3 |
               (long)(b[6] & 0xFF) << 5 |
               (long)(b[5] & 0xFF) << 13 |
               (long)(b[4] & 0xFF) << 21 |
               (long)(b[3] & 0xFF) << 29 |
               (long)(b[2] & 0xFF) << 37;
        }
    }
}
