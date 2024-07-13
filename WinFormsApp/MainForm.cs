 

namespace MSALib
{
    public partial class MainForm : Form
    {
        ClientAuthenticationResult AuthResult;

        /* event handlers */
        void AnyClick(object sender, EventArgs e)
        {
            if (btnClearLog == sender)
            {
                ClearLog();
            }
            else if (btnSignIn == sender)
            {
                SignIn();
            }
            else if (btnSignOut == sender)
            {
                SignOut();
            }
        }

        /* private */
        void FormInitialize()
        {
            btnClearLog.Click += AnyClick;
            btnSignIn.Click += AnyClick;
            btnSignOut.Click += AnyClick;

            cboLoginMode.Items.AddRange(new object[] { 
                "Public with WAM",
                "Public with WebView",
                "Public with Browser",
                "Confidential (Client ID + Client Secret)",
            });

            cboLoginMode.SelectedIndex = 0;

            cboLoginMode.SelectedIndexChanged += cboLoginMode_SelectedIndexChanged;
        }

        void cboLoginMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoginMode Mode = (LoginMode)(cboLoginMode.SelectedIndex);
            btnSignOut.Enabled = Mode != LoginMode.Confidential;
        }

        void SignIn()
        {
            SignOut();

            MSALApp AA = new MSALApp();

            LoginMode Mode = (LoginMode)(cboLoginMode.SelectedIndex);

            switch (Mode)
            {
                case LoginMode.PublicWAM:
                    AuthResult = MSAL.LoginUserInteractiveWAM(AA.DisplayName, AA.TenantId, AA.ClientId, this.Handle);
                    break;
                case LoginMode.PublicWebView:
                    AuthResult = MSAL.LoginUserInteractiveWebView(AA.DisplayName, AA.TenantId, AA.ClientId, UseBrowser: false);
                    break;
                case LoginMode.PublicBrowser:
                    AuthResult = MSAL.LoginUserInteractiveWebView(AA.DisplayName, AA.TenantId, AA.ClientId, UseBrowser: true);
                    break;
                case LoginMode.Confidential:
                    AuthResult = MSAL.LoginClientCredentials(AA.TenantId, AA.ClientId, AA.ClientSecret, RedirectUri: null);
                    break;
            }
 
            
            if (AuthResult.IsValid)
            {
                AppendLine($"Login {cboLoginMode.Text}. DONE");
                AppendLine(MSAL.GetAsText(AuthResult.AuthenticationResult));
                AppendLine();
            }
            else
            {
                AppendLine($"Login {cboLoginMode.Text}. FAILED");
                AppendLine(AuthResult.ErrorMessage);
                AppendLine();
            }

        }
        void SignOut()
        {
            if (AuthResult != null && AuthResult is PublicAuthenticationResult)
            {
                (AuthResult as PublicAuthenticationResult).SignOut();
                AuthResult = null;
                AppendLine("Logout. DONE");
                AppendLine();
            }
        }
 
        /* overrides */
        protected override void OnShown(EventArgs e)
        {
            if (!DesignMode)
                FormInitialize();
            base.OnShown(e);
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            SignOut();
        }

        /* public */
        public void ClearLog()
        {
            edtLog.Clear();
            Application.DoEvents();
        }
        public void Append(string Text)
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                edtLog.AppendText(Text);
            }
        }
        public void AppendLine(string Text = "--------------------------------------------")
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                edtLog.AppendText(Text + Environment.NewLine);
            }
        }
        public void Log(string Text = null)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                ClearLog();
            }
            else
            {
                AppendLine(Text);
            }
        }

        /* construction */
        public MainForm()
        {
            InitializeComponent();
        }

 
    }


}
