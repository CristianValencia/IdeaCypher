namespace IdeaCipher
{
    partial class cipherForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.inputKey = new System.Windows.Forms.TextBox();
			this.lblKey = new System.Windows.Forms.Label();
			this.inputPlainText = new System.Windows.Forms.TextBox();
			this.lblInput = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.inputEncryptedText = new System.Windows.Forms.TextBox();
			this.btnEncrypt = new System.Windows.Forms.Button();
			this.btnDecrypt = new System.Windows.Forms.Button();
			this.btnEncryptedSave = new System.Windows.Forms.Button();
			this.btnEncryptedLoad = new System.Windows.Forms.Button();
			this.btnPlainSave = new System.Windows.Forms.Button();
			this.btnPlainLoad = new System.Windows.Forms.Button();
			this.dlgEncryptedFile = new System.Windows.Forms.OpenFileDialog();
			this.dlgPlainFile = new System.Windows.Forms.OpenFileDialog();
			this.dlgEncryptedFileSave = new System.Windows.Forms.SaveFileDialog();
			this.dlgPlainFileSave = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// inputKey
			// 
			this.inputKey.Location = new System.Drawing.Point(231, 54);
			this.inputKey.Name = "inputKey";
			this.inputKey.Size = new System.Drawing.Size(408, 20);
			this.inputKey.TabIndex = 0;
			// 
			// lblKey
			// 
			this.lblKey.AutoSize = true;
			this.lblKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.11881F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblKey.Location = new System.Drawing.Point(402, 21);
			this.lblKey.Name = "lblKey";
			this.lblKey.Size = new System.Drawing.Size(54, 20);
			this.lblKey.TabIndex = 1;
			this.lblKey.Text = "Llave:";
			// 
			// inputPlainText
			// 
			this.inputPlainText.Location = new System.Drawing.Point(34, 208);
			this.inputPlainText.Multiline = true;
			this.inputPlainText.Name = "inputPlainText";
			this.inputPlainText.Size = new System.Drawing.Size(285, 141);
			this.inputPlainText.TabIndex = 2;
			// 
			// lblInput
			// 
			this.lblInput.AutoSize = true;
			this.lblInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.68317F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblInput.Location = new System.Drawing.Point(90, 111);
			this.lblInput.Name = "lblInput";
			this.lblInput.Size = new System.Drawing.Size(195, 29);
			this.lblInput.TabIndex = 3;
			this.lblInput.Text = "Entrada de datos";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.68317F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(539, 111);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(207, 29);
			this.label1.TabIndex = 5;
			this.label1.Text = "Datos encriptados";
			// 
			// inputEncryptedText
			// 
			this.inputEncryptedText.Location = new System.Drawing.Point(524, 208);
			this.inputEncryptedText.Multiline = true;
			this.inputEncryptedText.Name = "inputEncryptedText";
			this.inputEncryptedText.ReadOnly = true;
			this.inputEncryptedText.Size = new System.Drawing.Size(267, 138);
			this.inputEncryptedText.TabIndex = 4;
			// 
			// btnEncrypt
			// 
			this.btnEncrypt.Location = new System.Drawing.Point(353, 208);
			this.btnEncrypt.Name = "btnEncrypt";
			this.btnEncrypt.Size = new System.Drawing.Size(133, 54);
			this.btnEncrypt.TabIndex = 9;
			this.btnEncrypt.Text = "Encryptar =>";
			this.btnEncrypt.UseVisualStyleBackColor = true;
			this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
			// 
			// btnDecrypt
			// 
			this.btnDecrypt.Location = new System.Drawing.Point(353, 287);
			this.btnDecrypt.Name = "btnDecrypt";
			this.btnDecrypt.Size = new System.Drawing.Size(133, 59);
			this.btnDecrypt.TabIndex = 8;
			this.btnDecrypt.Text = "<= Desencriptar";
			this.btnDecrypt.UseVisualStyleBackColor = true;
			this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
			// 
			// btnEncryptedSave
			// 
			this.btnEncryptedSave.Location = new System.Drawing.Point(670, 168);
			this.btnEncryptedSave.Name = "btnEncryptedSave";
			this.btnEncryptedSave.Size = new System.Drawing.Size(121, 23);
			this.btnEncryptedSave.TabIndex = 19;
			this.btnEncryptedSave.Text = "Guardar en Archivo";
			this.btnEncryptedSave.UseVisualStyleBackColor = true;
			this.btnEncryptedSave.Click += new System.EventHandler(this.btnEncryptedSave_Click);
			// 
			// btnEncryptedLoad
			// 
			this.btnEncryptedLoad.Location = new System.Drawing.Point(529, 168);
			this.btnEncryptedLoad.Name = "btnEncryptedLoad";
			this.btnEncryptedLoad.Size = new System.Drawing.Size(119, 23);
			this.btnEncryptedLoad.TabIndex = 18;
			this.btnEncryptedLoad.Text = "Cargar Archivo";
			this.btnEncryptedLoad.UseVisualStyleBackColor = true;
			this.btnEncryptedLoad.Click += new System.EventHandler(this.btnEncryptedLoad_Click);
			// 
			// btnPlainSave
			// 
			this.btnPlainSave.Location = new System.Drawing.Point(195, 168);
			this.btnPlainSave.Name = "btnPlainSave";
			this.btnPlainSave.Size = new System.Drawing.Size(124, 23);
			this.btnPlainSave.TabIndex = 17;
			this.btnPlainSave.Text = "Guardar en Archivo";
			this.btnPlainSave.UseVisualStyleBackColor = true;
			this.btnPlainSave.Click += new System.EventHandler(this.btnPlainSave_Click);
			// 
			// btnPlainLoad
			// 
			this.btnPlainLoad.Location = new System.Drawing.Point(37, 168);
			this.btnPlainLoad.Name = "btnPlainLoad";
			this.btnPlainLoad.Size = new System.Drawing.Size(124, 23);
			this.btnPlainLoad.TabIndex = 16;
			this.btnPlainLoad.Text = "Cargar Archivo";
			this.btnPlainLoad.UseVisualStyleBackColor = true;
			this.btnPlainLoad.Click += new System.EventHandler(this.btnPlainLoad_Click);
			// 
			// dlgEncryptedFile
			// 
			this.dlgEncryptedFile.Filter = "Ecrypted files|*.dat";
			// 
			// dlgPlainFile
			// 
			this.dlgPlainFile.Filter = "Plain text|*.txt";
			// 
			// dlgEncryptedFileSave
			// 
			this.dlgEncryptedFileSave.Filter = "Encrypted data|*.dat";
			// 
			// dlgPlainFileSave
			// 
			this.dlgPlainFileSave.Filter = "Plain text|*.txt";
			// 
			// cipherForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(841, 387);
			this.Controls.Add(this.btnEncryptedSave);
			this.Controls.Add(this.btnEncryptedLoad);
			this.Controls.Add(this.btnPlainSave);
			this.Controls.Add(this.btnPlainLoad);
			this.Controls.Add(this.btnEncrypt);
			this.Controls.Add(this.btnDecrypt);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.inputEncryptedText);
			this.Controls.Add(this.lblInput);
			this.Controls.Add(this.inputPlainText);
			this.Controls.Add(this.lblKey);
			this.Controls.Add(this.inputKey);
			this.Name = "cipherForm";
			this.Text = "Idea (Block cipher)";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputKey;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.TextBox inputPlainText;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputEncryptedText;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnEncryptedSave;
        private System.Windows.Forms.Button btnEncryptedLoad;
        private System.Windows.Forms.Button btnPlainSave;
        private System.Windows.Forms.Button btnPlainLoad;
        private System.Windows.Forms.OpenFileDialog dlgEncryptedFile;
        private System.Windows.Forms.OpenFileDialog dlgPlainFile;
        private System.Windows.Forms.SaveFileDialog dlgEncryptedFileSave;
        private System.Windows.Forms.SaveFileDialog dlgPlainFileSave;
    }
}

