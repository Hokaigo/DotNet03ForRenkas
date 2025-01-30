namespace ProcessManagerWindowsFormsApp
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
            this.ProcessTableDataGridView = new System.Windows.Forms.DataGridView();
            this.update_button = new System.Windows.Forms.Button();
            this.stop_button = new System.Windows.Forms.Button();
            this.change_priority_button = new System.Windows.Forms.Button();
            this.priority_box = new System.Windows.Forms.ComboBox();
            this.path = new System.Windows.Forms.TextBox();
            this.start_app_button = new System.Windows.Forms.Button();
            this.stop_textBox = new System.Windows.Forms.TextBox();
            this.priority_textBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessTableDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ProcessTableDataGridView
            // 
            this.ProcessTableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProcessTableDataGridView.Location = new System.Drawing.Point(12, 0);
            this.ProcessTableDataGridView.Name = "ProcessTableDataGridView";
            this.ProcessTableDataGridView.Size = new System.Drawing.Size(657, 299);
            this.ProcessTableDataGridView.TabIndex = 0;
            // 
            // update_button
            // 
            this.update_button.Location = new System.Drawing.Point(12, 305);
            this.update_button.Name = "update_button";
            this.update_button.Size = new System.Drawing.Size(105, 39);
            this.update_button.TabIndex = 1;
            this.update_button.Text = "Оновити список";
            this.update_button.UseVisualStyleBackColor = true;
            this.update_button.Click += new System.EventHandler(this.update_button_Click);
            // 
            // stop_button
            // 
            this.stop_button.Location = new System.Drawing.Point(123, 305);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(105, 39);
            this.stop_button.TabIndex = 2;
            this.stop_button.Text = "Зупинити процес";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.stop_button_Click);
            // 
            // change_priority_button
            // 
            this.change_priority_button.Location = new System.Drawing.Point(234, 305);
            this.change_priority_button.Name = "change_priority_button";
            this.change_priority_button.Size = new System.Drawing.Size(105, 39);
            this.change_priority_button.TabIndex = 3;
            this.change_priority_button.Text = "Змінити пріорітет";
            this.change_priority_button.UseVisualStyleBackColor = true;
            this.change_priority_button.Click += new System.EventHandler(this.change_priority_button_Click);
            // 
            // priority_box
            // 
            this.priority_box.AutoCompleteCustomSource.AddRange(new string[] {
            "Idle",
            "BelowNormal",
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.priority_box.FormattingEnabled = true;
            this.priority_box.Items.AddRange(new object[] {
            "Idle",
            "BelowNormal",
            "Normal",
            "AboveNormal",
            "High",
            "RealTime"});
            this.priority_box.Location = new System.Drawing.Point(345, 315);
            this.priority_box.Name = "priority_box";
            this.priority_box.Size = new System.Drawing.Size(102, 21);
            this.priority_box.TabIndex = 4;
            // 
            // path
            // 
            this.path.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.path.Location = new System.Drawing.Point(453, 350);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(127, 29);
            this.path.TabIndex = 5;
            this.path.Text = "path";
            // 
            // start_app_button
            // 
            this.start_app_button.Location = new System.Drawing.Point(453, 305);
            this.start_app_button.Name = "start_app_button";
            this.start_app_button.Size = new System.Drawing.Size(129, 39);
            this.start_app_button.TabIndex = 6;
            this.start_app_button.Text = "Запустити програму";
            this.start_app_button.UseVisualStyleBackColor = true;
            this.start_app_button.Click += new System.EventHandler(this.start_app_button_Click);
            // 
            // stop_textBox
            // 
            this.stop_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.stop_textBox.Location = new System.Drawing.Point(123, 350);
            this.stop_textBox.Name = "stop_textBox";
            this.stop_textBox.Size = new System.Drawing.Size(105, 29);
            this.stop_textBox.TabIndex = 7;
            this.stop_textBox.Text = "id";
            // 
            // priority_textBox
            // 
            this.priority_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.priority_textBox.Location = new System.Drawing.Point(234, 350);
            this.priority_textBox.Name = "priority_textBox";
            this.priority_textBox.Size = new System.Drawing.Size(105, 29);
            this.priority_textBox.TabIndex = 8;
            this.priority_textBox.Text = "id";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 397);
            this.Controls.Add(this.priority_textBox);
            this.Controls.Add(this.stop_textBox);
            this.Controls.Add(this.start_app_button);
            this.Controls.Add(this.path);
            this.Controls.Add(this.priority_box);
            this.Controls.Add(this.change_priority_button);
            this.Controls.Add(this.stop_button);
            this.Controls.Add(this.update_button);
            this.Controls.Add(this.ProcessTableDataGridView);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ProcessTableDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ProcessTableDataGridView;
        private System.Windows.Forms.Button update_button;
        private System.Windows.Forms.Button stop_button;
        private System.Windows.Forms.Button change_priority_button;
        private System.Windows.Forms.ComboBox priority_box;
        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Button start_app_button;
        private System.Windows.Forms.TextBox stop_textBox;
        private System.Windows.Forms.TextBox priority_textBox;
    }
}

