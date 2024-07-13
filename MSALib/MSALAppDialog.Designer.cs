namespace MSALib
{
    partial class MSALAppDialog
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            edtDisplayName = new TextBox();
            edtTenantId = new TextBox();
            edtClientId = new TextBox();
            edtClientSecret = new TextBox();
            btnCancel = new Button();
            btnOK = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 21);
            label1.Name = "label1";
            label1.Size = new Size(80, 15);
            label1.TabIndex = 0;
            label1.Text = "Display Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 48);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 1;
            label2.Text = "Tenant ID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(45, 75);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 2;
            label3.Text = "Client ID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 102);
            label4.Name = "label4";
            label4.Size = new Size(73, 15);
            label4.TabIndex = 3;
            label4.Text = "Client Secret";
            // 
            // edtDisplayName
            // 
            edtDisplayName.Font = new Font("Courier New", 9F);
            edtDisplayName.Location = new Point(103, 18);
            edtDisplayName.Name = "edtDisplayName";
            edtDisplayName.Size = new Size(445, 21);
            edtDisplayName.TabIndex = 4;
            // 
            // edtTenantId
            // 
            edtTenantId.Font = new Font("Courier New", 9F);
            edtTenantId.Location = new Point(103, 45);
            edtTenantId.Name = "edtTenantId";
            edtTenantId.Size = new Size(445, 21);
            edtTenantId.TabIndex = 5;
            // 
            // edtClientId
            // 
            edtClientId.Font = new Font("Courier New", 9F);
            edtClientId.Location = new Point(103, 72);
            edtClientId.Name = "edtClientId";
            edtClientId.Size = new Size(445, 21);
            edtClientId.TabIndex = 6;
            // 
            // edtClientSecret
            // 
            edtClientSecret.Font = new Font("Courier New", 9F);
            edtClientSecret.Location = new Point(103, 99);
            edtClientSecret.Name = "edtClientSecret";
            edtClientSecret.Size = new Size(445, 21);
            edtClientSecret.TabIndex = 7;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(473, 144);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 32);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(392, 144);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 32);
            btnOK.TabIndex = 9;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // MSALAppDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(557, 179);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(edtClientSecret);
            Controls.Add(edtClientId);
            Controls.Add(edtTenantId);
            Controls.Add(edtDisplayName);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MSALAppDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "MSAL Application Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox edtDisplayName;
        private TextBox edtTenantId;
        private TextBox edtClientId;
        private TextBox edtClientSecret;
        private Button btnCancel;
        private Button btnOK;
    }
}