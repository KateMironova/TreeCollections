namespace BsTreeDraw
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
            this.pictBox = new System.Windows.Forms.PictureBox();
            this.btnDrawTree = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictBox
            // 
            this.pictBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictBox.Location = new System.Drawing.Point(0, 0);
            this.pictBox.Name = "pictBox";
            this.pictBox.Size = new System.Drawing.Size(721, 369);
            this.pictBox.TabIndex = 0;
            this.pictBox.TabStop = false;
            // 
            // btnDrawTree
            // 
            this.btnDrawTree.Location = new System.Drawing.Point(13, 13);
            this.btnDrawTree.Name = "btnDrawTree";
            this.btnDrawTree.Size = new System.Drawing.Size(96, 23);
            this.btnDrawTree.TabIndex = 1;
            this.btnDrawTree.Text = "Draw BsTree";
            this.btnDrawTree.UseVisualStyleBackColor = true;
            this.btnDrawTree.Click += new System.EventHandler(this.btnDrawTree_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 369);
            this.Controls.Add(this.btnDrawTree);
            this.Controls.Add(this.pictBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictBox;
        private System.Windows.Forms.Button btnDrawTree;
    }
}

