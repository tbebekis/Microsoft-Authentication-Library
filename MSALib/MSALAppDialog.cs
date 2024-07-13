using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSALib
{
    public partial class MSALAppDialog : Form
    {
        MSALApp App;

        void AnyClick(object sender, EventArgs e)
        {
            if (btnOK == sender)
            {
                ControlsToItem();
            }
        }


        void FormInitialize()
        {
            AcceptButton = btnOK;
            CancelButton = btnCancel;

            btnOK.Click += AnyClick;

            ItemToControls();
        }
        void ItemToControls()
        {
            edtDisplayName.Text = App.DisplayName;
            edtTenantId.Text = App.TenantId;
            edtClientId.Text = App.ClientId;
            edtClientSecret.Text = App.ClientSecret;
        }
        void ControlsToItem()
        {
            if (string.IsNullOrWhiteSpace(edtDisplayName.Text.Trim())
                || string.IsNullOrWhiteSpace(edtTenantId.Text.Trim())
                || string.IsNullOrWhiteSpace(edtClientId.Text.Trim())
                || string.IsNullOrWhiteSpace(edtClientSecret.Text.Trim())
                )
            {
                MessageBox.Show("MSAL Application not fully defined.");
                return;
            }


            App.DisplayName = edtDisplayName.Text.Trim();
            App.TenantId = edtTenantId.Text.Trim();
            App.ClientId = edtClientId.Text.Trim();
            App.ClientSecret = edtClientSecret.Text.Trim();


            this.DialogResult = DialogResult.OK;
        }

        protected override void OnShown(EventArgs e)
        {
            if (!DesignMode)
                FormInitialize();
            base.OnShown(e);
        }


        public MSALAppDialog()
        {
            InitializeComponent();
        }


        static public bool ShowModal(MSALApp App)
        {
            using (MSALAppDialog F = new MSALAppDialog())
            {
                F.App = App;
                return F.ShowDialog() == DialogResult.OK;
            }
        }
    }
}
