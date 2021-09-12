
namespace OCR
{
    partial class UserControlShowMath
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_Priklad = new System.Windows.Forms.TextBox();
            this.textBox_Vysledek = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox_Priklad
            // 
            this.textBox_Priklad.AllowDrop = true;
            this.textBox_Priklad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Priklad.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Priklad.Location = new System.Drawing.Point(0, 18);
            this.textBox_Priklad.Multiline = true;
            this.textBox_Priklad.Name = "textBox_Priklad";
            this.textBox_Priklad.Size = new System.Drawing.Size(380, 50);
            this.textBox_Priklad.TabIndex = 0;
            this.textBox_Priklad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_Vysledek
            // 
            this.textBox_Vysledek.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_Vysledek.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Vysledek.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Vysledek.Location = new System.Drawing.Point(0, 95);
            this.textBox_Vysledek.Multiline = true;
            this.textBox_Vysledek.Name = "textBox_Vysledek";
            this.textBox_Vysledek.Size = new System.Drawing.Size(380, 74);
            this.textBox_Vysledek.TabIndex = 1;
            this.textBox_Vysledek.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserControlShowMath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.textBox_Vysledek);
            this.Controls.Add(this.textBox_Priklad);
            this.Name = "UserControlShowMath";
            this.Size = new System.Drawing.Size(380, 411);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UserControlShowMath_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Priklad;
        private System.Windows.Forms.TextBox textBox_Vysledek;
    }
}
