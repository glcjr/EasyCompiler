namespace TestCompiler2
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtassembly = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.txtoutput = new System.Windows.Forms.TextBox();
            this.rbCSharp = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbvisualbasic = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbdll = new System.Windows.Forms.RadioButton();
            this.rbexe = new System.Windows.Forms.RadioButton();
            this.rbmemory = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(39, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Files";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(39, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Add Embeds";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtassembly
            // 
            this.txtassembly.Location = new System.Drawing.Point(95, 83);
            this.txtassembly.Name = "txtassembly";
            this.txtassembly.Size = new System.Drawing.Size(100, 20);
            this.txtassembly.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(202, 81);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "ADD";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Assembly";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(173, 117);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Compile";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtoutput
            // 
            this.txtoutput.Location = new System.Drawing.Point(3, 157);
            this.txtoutput.Multiline = true;
            this.txtoutput.Name = "txtoutput";
            this.txtoutput.Size = new System.Drawing.Size(274, 92);
            this.txtoutput.TabIndex = 6;
            // 
            // rbCSharp
            // 
            this.rbCSharp.AutoSize = true;
            this.rbCSharp.Checked = true;
            this.rbCSharp.Location = new System.Drawing.Point(13, 3);
            this.rbCSharp.Name = "rbCSharp";
            this.rbCSharp.Size = new System.Drawing.Size(60, 17);
            this.rbCSharp.TabIndex = 7;
            this.rbCSharp.TabStop = true;
            this.rbCSharp.Text = "CSharp";
            this.rbCSharp.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbvisualbasic);
            this.panel1.Controls.Add(this.rbCSharp);
            this.panel1.Location = new System.Drawing.Point(42, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 24);
            this.panel1.TabIndex = 8;
            // 
            // rbvisualbasic
            // 
            this.rbvisualbasic.AutoSize = true;
            this.rbvisualbasic.Location = new System.Drawing.Point(80, 4);
            this.rbvisualbasic.Name = "rbvisualbasic";
            this.rbvisualbasic.Size = new System.Drawing.Size(79, 17);
            this.rbvisualbasic.TabIndex = 8;
            this.rbvisualbasic.Text = "VisualBasic";
            this.rbvisualbasic.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbmemory);
            this.panel2.Controls.Add(this.rbexe);
            this.panel2.Controls.Add(this.rbdll);
            this.panel2.Location = new System.Drawing.Point(3, 108);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(175, 43);
            this.panel2.TabIndex = 9;
            // 
            // rbdll
            // 
            this.rbdll.AutoSize = true;
            this.rbdll.Location = new System.Drawing.Point(10, 9);
            this.rbdll.Name = "rbdll";
            this.rbdll.Size = new System.Drawing.Size(45, 17);
            this.rbdll.TabIndex = 0;
            this.rbdll.Text = "DLL";
            this.rbdll.UseVisualStyleBackColor = true;
            // 
            // rbexe
            // 
            this.rbexe.AutoSize = true;
            this.rbexe.Checked = true;
            this.rbexe.Location = new System.Drawing.Point(57, 9);
            this.rbexe.Name = "rbexe";
            this.rbexe.Size = new System.Drawing.Size(46, 17);
            this.rbexe.TabIndex = 1;
            this.rbexe.TabStop = true;
            this.rbexe.Text = "EXE";
            this.rbexe.UseVisualStyleBackColor = true;
            // 
            // rbmemory
            // 
            this.rbmemory.AutoSize = true;
            this.rbmemory.Location = new System.Drawing.Point(106, 9);
            this.rbmemory.Name = "rbmemory";
            this.rbmemory.Size = new System.Drawing.Size(62, 17);
            this.rbmemory.TabIndex = 2;
            this.rbmemory.Text = "Memory";
            this.rbmemory.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtoutput);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtassembly);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtassembly;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtoutput;
        private System.Windows.Forms.RadioButton rbCSharp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbvisualbasic;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbmemory;
        private System.Windows.Forms.RadioButton rbexe;
        private System.Windows.Forms.RadioButton rbdll;
    }
}

