 

namespace MSALib
{

    /// <summary>
    /// Represents this Application as it is registered in Azure Microsoft Entra Registered Applications.
    /// </summary>
    public class MSALApp
    {
        const string SFileName = "MSALAppSettings.json";

        /* properties */
        /// <summary>
        /// Constructor
        /// </summary>
        public MSALApp()
        {
            Load();
        }

        public void Load()
        {
            if (!File.Exists(SFileName))
            {
                if (MSALAppDialog.ShowModal(this))
                {
                    Save();                   
                }
                else
                {
                    throw new ApplicationException("MSAL Application not defined.");
                }
            }

            Json.LoadFromFile(this, SFileName);
        }
        public void Save()
        {
            Json.SaveToFile(this, SFileName);
        }

        /* properties */
        /// <summary>
        /// DisplayName
        /// </summary>
        public string DisplayName { get; set; }  
        /// <summary>
        /// TenantId
        /// </summary>
        public string TenantId { get; set; }  
        /// <summary>
        /// ClientId
        /// </summary>
        public string ClientId { get; set; } 
        /// <summary>
        /// Client Secret
        /// </summary>
        public string ClientSecret { get; set; } 
 
    }
}
