namespace MSALib
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSignIn = new Button();
            panel1 = new Panel();
            cboLoginMode = new ComboBox();
            btnSignOut = new Button();
            btnClearLog = new Button();
            edtLog = new RichTextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnSignIn
            // 
            btnSignIn.Location = new Point(16, 12);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Size = new Size(152, 32);
            btnSignIn.TabIndex = 0;
            btnSignIn.Text = "Sign-in";
            btnSignIn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(cboLoginMode);
            panel1.Controls.Add(btnSignOut);
            panel1.Controls.Add(btnClearLog);
            panel1.Controls.Add(btnSignIn);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(494, 94);
            panel1.TabIndex = 1;
            // 
            // cboLoginMode
            // 
            cboLoginMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoginMode.FormattingEnabled = true;
            cboLoginMode.Location = new Point(190, 16);
            cboLoginMode.Name = "cboLoginMode";
            cboLoginMode.Size = new Size(257, 23);
            cboLoginMode.TabIndex = 3;
            // 
            // btnSignOut
            // 
            btnSignOut.Location = new Point(16, 50);
            btnSignOut.Name = "btnSignOut";
            btnSignOut.Size = new Size(152, 32);
            btnSignOut.TabIndex = 2;
            btnSignOut.Text = "Sign-out";
            btnSignOut.UseVisualStyleBackColor = true;
            // 
            // btnClearLog
            // 
            btnClearLog.Location = new Point(190, 48);
            btnClearLog.Name = "btnClearLog";
            btnClearLog.Size = new Size(152, 32);
            btnClearLog.TabIndex = 1;
            btnClearLog.Text = "Clear Log";
            btnClearLog.UseVisualStyleBackColor = true;
            // 
            // edtLog
            // 
            edtLog.BackColor = Color.WhiteSmoke;
            edtLog.Dock = DockStyle.Fill;
            edtLog.Font = new Font("Courier New", 9F);
            edtLog.Location = new Point(0, 94);
            edtLog.Name = "edtLog";
            edtLog.ReadOnly = true;
            edtLog.Size = new Size(494, 230);
            edtLog.TabIndex = 2;
            edtLog.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(494, 324);
            Controls.Add(edtLog);
            Controls.Add(panel1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Microsoft Authentication Library test";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnSignIn;
        private Panel panel1;
        private RichTextBox edtLog;
        private Button btnClearLog;
        private Button btnSignOut;
        private ComboBox cboLoginMode;
    }
}
