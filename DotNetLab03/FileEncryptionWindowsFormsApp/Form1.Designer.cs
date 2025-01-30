namespace FileEncryptionWindowsFormsApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.keyInputLabel = new System.Windows.Forms.Label();
            this.keyInputTextBox = new System.Windows.Forms.TextBox();
            this.EncryptOrDecryptButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            this.GenerateRandomKeyLabel = new System.Windows.Forms.Label();
            this.randomKeyTextBox = new System.Windows.Forms.TextBox();
            this.generateRandomKeyButton = new System.Windows.Forms.Button();
            this.setOutputPathButton = new System.Windows.Forms.Button();
            this.outputPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // keyInputLabel
            // 
            this.keyInputLabel.AutoSize = true;
            this.keyInputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.keyInputLabel.Location = new System.Drawing.Point(69, 29);
            this.keyInputLabel.Name = "keyInputLabel";
            this.keyInputLabel.Size = new System.Drawing.Size(251, 24);
            this.keyInputLabel.TabIndex = 0;
            this.keyInputLabel.Text = "Введіть ключ шифрування:";
            // 
            // keyInputTextBox
            // 
            this.keyInputTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.keyInputTextBox.Location = new System.Drawing.Point(32, 69);
            this.keyInputTextBox.Name = "keyInputTextBox";
            this.keyInputTextBox.Size = new System.Drawing.Size(334, 29);
            this.keyInputTextBox.TabIndex = 1;
            // 
            // EncryptOrDecryptButton
            // 
            this.EncryptOrDecryptButton.Location = new System.Drawing.Point(122, 104);
            this.EncryptOrDecryptButton.Name = "EncryptOrDecryptButton";
            this.EncryptOrDecryptButton.Size = new System.Drawing.Size(154, 41);
            this.EncryptOrDecryptButton.TabIndex = 3;
            this.EncryptOrDecryptButton.Text = "Шифрувати/Розшифрувати";
            this.EncryptOrDecryptButton.UseVisualStyleBackColor = true;
            this.EncryptOrDecryptButton.Click += new System.EventHandler(this.EncryptOrDecryptButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(32, 480);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(331, 30);
            this.progressBar.TabIndex = 5;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.progressLabel.Location = new System.Drawing.Point(303, 442);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(0, 24);
            this.progressLabel.TabIndex = 6;
            // 
            // GenerateRandomKeyLabel
            // 
            this.GenerateRandomKeyLabel.AutoSize = true;
            this.GenerateRandomKeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.GenerateRandomKeyLabel.Location = new System.Drawing.Point(57, 166);
            this.GenerateRandomKeyLabel.Name = "GenerateRandomKeyLabel";
            this.GenerateRandomKeyLabel.Size = new System.Drawing.Size(293, 24);
            this.GenerateRandomKeyLabel.TabIndex = 7;
            this.GenerateRandomKeyLabel.Text = "Згенерувати випадковий ключ:";
            // 
            // randomKeyTextBox
            // 
            this.randomKeyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.randomKeyTextBox.Location = new System.Drawing.Point(32, 206);
            this.randomKeyTextBox.Name = "randomKeyTextBox";
            this.randomKeyTextBox.ReadOnly = true;
            this.randomKeyTextBox.Size = new System.Drawing.Size(334, 29);
            this.randomKeyTextBox.TabIndex = 8;
            // 
            // generateRandomKeyButton
            // 
            this.generateRandomKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.generateRandomKeyButton.Location = new System.Drawing.Point(99, 241);
            this.generateRandomKeyButton.Name = "generateRandomKeyButton";
            this.generateRandomKeyButton.Size = new System.Drawing.Size(204, 40);
            this.generateRandomKeyButton.TabIndex = 9;
            this.generateRandomKeyButton.Text = "Згенерувати випадковий ключ";
            this.generateRandomKeyButton.UseVisualStyleBackColor = true;
            this.generateRandomKeyButton.Click += new System.EventHandler(this.generateRandomKeyButton_Click);
            // 
            // setOutputPathButton
            // 
            this.setOutputPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.setOutputPathButton.Location = new System.Drawing.Point(102, 377);
            this.setOutputPathButton.Name = "setOutputPathButton";
            this.setOutputPathButton.Size = new System.Drawing.Size(204, 40);
            this.setOutputPathButton.TabIndex = 11;
            this.setOutputPathButton.Text = "Задати місце збереження";
            this.setOutputPathButton.UseVisualStyleBackColor = true;
            this.setOutputPathButton.Click += new System.EventHandler(this.setOutputPathButton_Click);
            // 
            // outputPathTextBox
            // 
            this.outputPathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.outputPathTextBox.Location = new System.Drawing.Point(32, 342);
            this.outputPathTextBox.Name = "outputPathTextBox";
            this.outputPathTextBox.ReadOnly = true;
            this.outputPathTextBox.Size = new System.Drawing.Size(334, 29);
            this.outputPathTextBox.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.Location = new System.Drawing.Point(69, 300);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 24);
            this.label1.TabIndex = 13;
            this.label1.Text = "Задати місце збереження:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 536);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputPathTextBox);
            this.Controls.Add(this.setOutputPathButton);
            this.Controls.Add(this.generateRandomKeyButton);
            this.Controls.Add(this.randomKeyTextBox);
            this.Controls.Add(this.GenerateRandomKeyLabel);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.EncryptOrDecryptButton);
            this.Controls.Add(this.keyInputTextBox);
            this.Controls.Add(this.keyInputLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Encryptor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label keyInputLabel;
        private System.Windows.Forms.TextBox keyInputTextBox;
        private System.Windows.Forms.Button EncryptOrDecryptButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label GenerateRandomKeyLabel;
        private System.Windows.Forms.TextBox randomKeyTextBox;
        private System.Windows.Forms.Button generateRandomKeyButton;
        private System.Windows.Forms.Button setOutputPathButton;
        private System.Windows.Forms.TextBox outputPathTextBox;
        private System.Windows.Forms.Label label1;
    }
}

