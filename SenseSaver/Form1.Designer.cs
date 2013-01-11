namespace SenseSaver
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.GoBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.BookBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GoBtn
            // 
            this.GoBtn.Location = new System.Drawing.Point(12, 12);
            this.GoBtn.Name = "GoBtn";
            this.GoBtn.Size = new System.Drawing.Size(133, 23);
            this.GoBtn.TabIndex = 0;
            this.GoBtn.Text = "Dictionary";
            this.GoBtn.UseVisualStyleBackColor = true;
            this.GoBtn.Click += new System.EventHandler(this.GoBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*.txt";
            this.openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            // 
            // BookBtn
            // 
            this.BookBtn.Location = new System.Drawing.Point(151, 12);
            this.BookBtn.Name = "BookBtn";
            this.BookBtn.Size = new System.Drawing.Size(121, 23);
            this.BookBtn.TabIndex = 3;
            this.BookBtn.Text = "Book";
            this.BookBtn.UseVisualStyleBackColor = true;
            this.BookBtn.Click += new System.EventHandler(this.BookBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 47);
            this.Controls.Add(this.BookBtn);
            this.Controls.Add(this.GoBtn);
            this.Name = "Form1";
            this.Text = "SenseSaver Analiser";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GoBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button BookBtn;
    }
}

