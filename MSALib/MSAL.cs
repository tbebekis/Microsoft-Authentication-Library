namespace MSALib
{

    /// <summary>
    /// Helper for siging-in users using Microsoft Entra ID.
    /// <para>SEE: <c>https://learn.microsoft.com/en-us/entra/identity/</c></para>
    /// </summary>
    static public class MSAL
    {
 
        /// <summary>
        /// Used when calling async methods as synchronous
        /// </summary>
        static public readonly TaskFactory TaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);
        /// <summary>
        /// Used when calling async methods as synchronous
        /// </summary>
        static public readonly CultureInfo UiCulture = CultureInfo.CurrentUICulture;
        /// <summary>
        /// Used when calling async methods as synchronous
        /// </summary>
        static public readonly CultureInfo Culture = CultureInfo.CurrentCulture;
 
        /* private */
        /// <summary>
        /// Sets-up a token cache for a <see cref="IClientApplicationBase"/> client application.
        /// <para><strong>NOTE</strong>: No cache is used if the cache file name is null or empty.</para>
        /// </summary>
        static public async Task UseCacheAsync(IClientApplicationBase ClientApplication, string CacheFileName = "cache.bin")
        {
            if (!string.IsNullOrWhiteSpace(CacheFileName))
            {
                StorageCreationProperties StorageProperties = new StorageCreationPropertiesBuilder(CacheFileName, AppDomain.CurrentDomain.BaseDirectory).Build();

                MsalCacheHelper CacheHelper = await MsalCacheHelper.CreateAsync(StorageProperties);
                CacheHelper.RegisterCache(ClientApplication.UserTokenCache);
            }
        }

         
        /* Public Client */
        /// <summary>
        /// <strong>NOTE</strong>: To be used with Windows.Forms desktop applications or console applications in <strong>MS Windows only</strong>.
        /// <para>Signs-in an Microsoft Entra ID (former Azure Active Directory) User by displaying the login Window of the <c>https://login.microsoftonline.com</c> .  </para>
        /// <para>Uses the <strong>Web Account Manager (WAM)</strong>  authentication broker and <strong>Account Caching</strong> .</para>
        /// <para><strong>Aquiring tokens interactively</strong>: https://learn.microsoft.com/en-us/entra/msal/dotnet/acquiring-tokens/desktop-mobile/acquiring-tokens-interactively</para>
        /// <para><strong>WAM</strong>: <c>https://learn.microsoft.com/en-us/entra/msal/dotnet/acquiring-tokens/desktop-mobile/wam</c> </para>
        /// <para><strong>Account Caching</strong>: <c>https://learn.microsoft.com/en-us/entra/msal/dotnet/how-to/token-cache-serialization</c>  </para> 
        /// <para></para>
        /// <para><strong>Required in App Registration in Microsoft Entra ID </strong></para>
        /// <list type="bullet">
        ///     <item><term>Platform configurations</term>
        ///         <description>The platform <i>Mobile and desktop applications</i> should be added with the following custom <i>RedirectUri</i>:
        ///             <para><c>ms-appx-web://microsoft.aad.brokerplugin/cc3376b0-573f-4f21-8cf6-31d694225b43</c></para>
        ///         </description>
        ///     </item>
        ///     <item><term>Permissions</term>
        ///         <description>The registered application should have at least the following permission: 
        ///           <para><c>User.Read</c></para>
        ///         </description>
        ///     </item>
        /// </list>
        /// <para><strong>WARNING:</strong>Since this method uses Account Caching, a logout call is necessary when the user is done working. Otherwise the user remains logged-in.</para>
        /// </summary>
        /// <param name="AppDisplayName">The name the app is registered with.</param>
        /// <param name="TenantId">The Tenant Id (is the Directory (tenant) ID in the registered app Overview.</param>
        /// <param name="ClientId">The Client Id (is the Application (client) ID in the registered app Overview.</param>
        /// <param name="ParentWindowHandle">The Windows Handle of the calling UI. SEE: <c>https://learn.microsoft.com/el-gr/windows/apps/develop/ui-input/retrieve-hwnd</c></param>
        /// <param name="CacheFileName">The file name of the token cache. The file is saved in the folder of this assembly. No cache is used if this parameter is null or empty.</param> 
        static public async Task<PublicAuthenticationResult> LoginUserInteractiveWAMAsync(string AppDisplayName, string TenantId, string ClientId, IntPtr ParentWindowHandle, string CacheFileName = "cache.bin")
        {
            string[] Scopes = new[] { "User.Read" };
            string Authority = "https://login.microsoftonline.com/common";

            PublicClientApplicationOptions ApplicationOptions = new PublicClientApplicationOptions();
            ApplicationOptions.Instance = "https://login.microsoftonline.com"; 
            ApplicationOptions.ClientName = AppDisplayName;
            ApplicationOptions.ClientId = ClientId;
            ApplicationOptions.TenantId = TenantId;
            ApplicationOptions.RedirectUri = "ms-appx-web://microsoft.aad.brokerplugin/cc3376b0-573f-4f21-8cf6-31d694225b43";
 
            PublicClientApplicationBuilder Builder = PublicClientApplicationBuilder
                            .CreateWithApplicationOptions(ApplicationOptions)
                            .WithParentActivityOrWindow(() => ParentWindowHandle)
                            .WithAuthority(Authority);

            BrokerOptions BrokerOptions = new BrokerOptions(BrokerOptions.OperatingSystems.Windows) { Title = AppDisplayName };
            Microsoft.Identity.Client.Broker.BrokerExtension.WithBroker(Builder, BrokerOptions);

            IPublicClientApplication ClientApplication = Builder.Build();

            if (string.IsNullOrWhiteSpace(CacheFileName))
                CacheFileName = "cache.bin";
            await UseCacheAsync(ClientApplication, CacheFileName);

            // Get a cached Account, if there is any, from a previous execution  
            IEnumerable<IAccount> AccountList = await ClientApplication.GetAccountsAsync();
            IAccount FirstAccount = AccountList.FirstOrDefault();

            string ErrorMessage = string.Empty;
            AuthenticationResult AuthResult = null;
            try
            {
                // if there is a cached Account, then do a silent login 
                // else aquire a token interactively (by displaying the Web Account Manager (WAM) window)
                AuthResult = FirstAccount != null
                    ? await ClientApplication.AcquireTokenSilent(Scopes, FirstAccount).ExecuteAsync()
                    : await ClientApplication.AcquireTokenInteractive(Scopes).ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                AuthResult = await ClientApplication.AcquireTokenInteractive(Scopes).ExecuteAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return new PublicAuthenticationResult(ClientApplication, AuthResult, ErrorMessage);
        }
        /// <summary>     
        /// <strong>NOTE</strong>: To be used with Windows.Forms desktop applications or console applications in <strong>MS Windows only</strong>.
        /// <para>Signs-in an Microsoft Entra ID (former Azure Active Directory) User by displaying the login Window of the <c>https://login.microsoftonline.com</c> .  </para>
        /// <para>Uses the <strong>Web Account Manager (WAM)</strong>  authentication broker and <strong>Account Caching</strong> .</para>
        /// <para><strong>Aquiring tokens interactively</strong>: https://learn.microsoft.com/en-us/entra/msal/dotnet/acquiring-tokens/desktop-mobile/acquiring-tokens-interactively</para>
        /// <para><strong>WAM</strong>: <c>https://learn.microsoft.com/en-us/entra/msal/dotnet/acquiring-tokens/desktop-mobile/wam</c> </para>
        /// <para><strong>Account Caching</strong>: <c>https://learn.microsoft.com/en-us/entra/msal/dotnet/how-to/token-cache-serialization</c>  </para> 
        /// <para></para>
        /// <para><strong>Required in App Registration in Microsoft Entra ID </strong></para>
        /// <list type="bullet">
        ///     <item><term>Platform configurations</term>
        ///         <description>The platform <i>Mobile and desktop applications</i> should be added with the following custom <i>RedirectUri</i>:
        ///             <para><c>ms-appx-web://microsoft.aad.brokerplugin/cc3376b0-573f-4f21-8cf6-31d694225b43</c></para>
        ///         </description>
        ///     </item>
        ///     <item><term>Permissions</term>
        ///         <description>The registered application should have at least the following permission: 
        ///           <para><c>User.Read</c></para>
        ///         </description>
        ///     </item>
        /// </list>
        /// <para><strong>WARNING:</strong>Since this method uses Account Caching, a logout call is necessary when the user is done working. Otherwise the user remains logged-in.</para>
        /// <para>NOTE: This is the synchronous version of the <see cref="LoginUserInteractiveWAMAsync"/> async method.</para>
        /// </summary>
        /// <param name="AppDisplayName">The name the app is registered with.</param>
        /// <param name="TenantId">The Tenant Id (is the Directory (tenant) ID in the registered app Overview.</param>
        /// <param name="ClientId">The Client Id (is the Application (client) ID in the registered app Overview.</param>
        /// <param name="ParentWindowHandle">The Windows Handle of the calling Form</param>
        /// <param name="CacheFileName">The file name of the token cache. The file is saved in the folder of this assembly. No cache is used if this parameter is null or empty.</param>
        static public PublicAuthenticationResult LoginUserInteractiveWAM(string AppDisplayName, string TenantId, string ClientId, IntPtr ParentWindowHandle, string CacheFileName = "cache.bin")
        {
            return TaskFactory.StartNew(() =>
            {
                Thread.CurrentThread.CurrentCulture = Culture;
                Thread.CurrentThread.CurrentUICulture = UiCulture;
                return LoginUserInteractiveWAMAsync(AppDisplayName, TenantId, ClientId, ParentWindowHandle, CacheFileName);

            }).Unwrap().GetAwaiter().GetResult();
        }

        static public async Task<PublicAuthenticationResult> LoginUserInteractiveWebViewAsync(string AppDisplayName, string TenantId, string ClientId, string CacheFileName = "cache.bin", bool UseBrowser = false)
        {
            string[] Scopes = new[] { "User.Read" };
            string Authority = "https://login.microsoftonline.com/common";

            PublicClientApplicationOptions ApplicationOptions = new PublicClientApplicationOptions();
            ApplicationOptions.Instance = "https://login.microsoftonline.com";
            ApplicationOptions.ClientName = AppDisplayName;
            ApplicationOptions.ClientId = ClientId;
            ApplicationOptions.TenantId = TenantId;
            ApplicationOptions.RedirectUri = "http://localhost";

            BrokerOptions BrokerOptions = new BrokerOptions(BrokerOptions.OperatingSystems.Windows) { Title = AppDisplayName };

            IPublicClientApplication ClientApplication = PublicClientApplicationBuilder
                .CreateWithApplicationOptions(ApplicationOptions)
                .WithWindowsEmbeddedBrowserSupport()
                .WithAuthority(Authority)
                .Build();

            if (string.IsNullOrWhiteSpace(CacheFileName))
                CacheFileName = "cache.bin";

            await MSAL.UseCacheAsync(ClientApplication, CacheFileName);

            // Get a cached Account, if there is any, from a previous execution  
            IEnumerable<IAccount> AccountList = await ClientApplication.GetAccountsAsync();
            IAccount FirstAccount = AccountList.FirstOrDefault();

            string ErrorMessage = string.Empty;
            AuthenticationResult AuthResult = null;
            try
            {
                // if there is a cached Account, then do a silent login 
                // else aquire a token interactively (by displaying the Web Account Manager (WAM) window)
                AuthResult = FirstAccount != null
                    ? await ClientApplication.AcquireTokenSilent(Scopes, FirstAccount)
                            .ExecuteAsync()
                    : await ClientApplication.AcquireTokenInteractive(Scopes)
                            .WithUseEmbeddedWebView(UseBrowser? false: true)
                            .ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                AuthResult = await ClientApplication.AcquireTokenInteractive(Scopes).ExecuteAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return new PublicAuthenticationResult(ClientApplication, AuthResult, ErrorMessage);
        }
        static public PublicAuthenticationResult LoginUserInteractiveWebView(string AppDisplayName, string TenantId, string ClientId, string CacheFileName = "cache.bin", bool UseBrowser = false)
        {
            return TaskFactory.StartNew(() =>
            {
                Thread.CurrentThread.CurrentCulture = Culture;
                Thread.CurrentThread.CurrentUICulture = UiCulture;
                return LoginUserInteractiveWebViewAsync(AppDisplayName, TenantId, ClientId, CacheFileName, UseBrowser);

            }).Unwrap().GetAwaiter().GetResult();

        }

        /* Confidential Client */
        /// <summary>
        /// Creates and returns an instance which represents a registered confidential client application such as a Web Application or a WebApi application.
        /// <para><strong>NOTE</strong>: The <c>RedirectUri</c> is <strong>NOT</strong> needed when the confidential client application uses only the client credentials flow.</para>
        /// </summary>
        static public IConfidentialClientApplication CreateConfidentialClientApplication(string TenantId, string ApplicationId, string ApplicationSecret, string RedirectUri = "")
        {
            string Authority = $"https://login.microsoftonline.com/{TenantId}/v2.0";

            ConfidentialClientApplicationBuilder Builder = ConfidentialClientApplicationBuilder.Create(ApplicationId)
                                                            .WithAuthority(Authority)
                                                            .WithClientSecret(ApplicationSecret);

            if (!string.IsNullOrWhiteSpace(RedirectUri))
                Builder = Builder.WithRedirectUri(RedirectUri);

            IConfidentialClientApplication Result = Builder.Build();

            return Result;
        }
        static public async Task<ConfidentialAuthenticationResult> LoginClientCredentialsAsync(string TenantId, string ApplicationId, string ApplicationSecret, string RedirectUri = "")
        {
            string[] Scopes = new[] { "https://graph.microsoft.com/.default" };

            IConfidentialClientApplication ClientApplication = CreateConfidentialClientApplication(TenantId, ApplicationId, ApplicationSecret, RedirectUri);


            ClientApplication.AddInMemoryTokenCache();

            string ErrorMessage = string.Empty;
            AuthenticationResult AuthResult = null;

            AuthenticationResult msalAuthenticationResult = await ClientApplication.AcquireTokenForClient(Scopes)
                .ExecuteAsync();

            try
            {
                // if there is a cached Account, then do a silent login 
                // else aquire a token interactively (by displaying the Web Account Manager (WAM) window)
                AuthResult = await ClientApplication.AcquireTokenForClient(Scopes)
                .ExecuteAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return new ConfidentialAuthenticationResult(ClientApplication, AuthResult, ErrorMessage);
        }
        static public ConfidentialAuthenticationResult LoginClientCredentials(string TenantId, string ApplicationId, string ApplicationSecret, string RedirectUri = "")
        {
            return TaskFactory.StartNew(() =>
            {
                Thread.CurrentThread.CurrentCulture = Culture;
                Thread.CurrentThread.CurrentUICulture = UiCulture;
                return LoginClientCredentialsAsync(TenantId, ApplicationId, ApplicationSecret, RedirectUri);

            }).Unwrap().GetAwaiter().GetResult();
        }


        /* Common */
        /// <summary>
        /// Logs-out a user.
        /// </summary>
        static public async Task LogOutUserAsync(IClientApplicationBase ClientApplication, AuthenticationResult AuthResult)
        {
            if (AuthResult != null)
            {
                await ClientApplication.RemoveAsync(AuthResult.Account);
            }
        }
        /// <summary>
        /// Logs-out a user.
        /// </summary>
        static public void LogOutUser(IClientApplicationBase ClientApplication, AuthenticationResult AuthResult)
        {
            if (AuthResult != null)
            {
                MSAL.TaskFactory.StartNew(() =>
                {
                    Thread.CurrentThread.CurrentCulture = MSAL.Culture;
                    Thread.CurrentThread.CurrentUICulture = MSAL.UiCulture;
                    return LogOutUserAsync(ClientApplication, AuthResult);

                }).Unwrap().GetAwaiter().GetResult();
            }
        }

        /* helpers */
        /// <summary>
        /// Clears the token cache be removing all <see cref="IAccount"/> accounts from it.
        /// </summary>
        static public async Task ClearAccountCacheAsync(IClientApplicationBase ClientApplication)
        {
            try
            {
                var AccountList = await ClientApplication.GetAccountsAsync();
                while (AccountList.Any())
                {
                    await ClientApplication.RemoveAsync(AccountList.First());
                    AccountList = await ClientApplication.GetAccountsAsync();
                }
            }
            catch  
            { 
            }
        }

        /// <summary>
        /// Returns a string representation of the authentication result, for display purposes.
        /// </summary>
        static public string GetAsText(AuthenticationResult AuthResult)
        {
            StringBuilder SB = new StringBuilder();
            SB.AppendLine($"User Name: {AuthResult.Account.Username}");
            SB.AppendLine($"Expires On: {AuthResult.ExpiresOn.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss")}"); 
            return SB.ToString().Trim();
        }
        /// <summary>
        /// Decodes a token.
        /// </summary>
        static public string DecodeToken(string Token)
        {
            JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
            SecurityToken SecurityToken = TokenHandler.ReadToken(Token);
            JwtSecurityToken JWT = SecurityToken as JwtSecurityToken;
            string Result = JWT.ToString();
            return Result;
        }
 
    }
}
