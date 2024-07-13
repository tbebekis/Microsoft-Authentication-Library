using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSALib
{
    public class ClientAuthenticationResult
    {

        /* construction */
        /// <summary>
        /// Constructor.
        /// </summary>
        public ClientAuthenticationResult(IClientApplicationBase ClientApplication, AuthenticationResult AuthResult, string ErrorMessage)
        {
            this.ClientApplication = ClientApplication;
            this.AuthenticationResult = AuthResult;
            this.ErrorMessage = ErrorMessage;
        }


        /* properties */
        /// <summary>
        /// True when the authentication was successful.
        /// </summary>
        public virtual bool IsValid { get { return ClientApplication != null && AuthenticationResult != null && string.IsNullOrWhiteSpace(ErrorMessage); } }
        /// <summary>
        /// The client application instance.
        /// </summary>
        public IClientApplicationBase ClientApplication { get; }
        /// <summary>
        /// The authentication result. 
        /// <para><strong>WARNING</strong>: Null on failure.</para>
        /// </summary>
        public AuthenticationResult AuthenticationResult { get; }
        /// <summary>
        /// The expiration date-time.
        /// <para><strong>WARNING</strong>: Null on failure.</para>
        /// </summary>
        public DateTime ExpiresOn { get { return AuthenticationResult.ExpiresOn.LocalDateTime; } }
        /// <summary>
        /// Has a value only when the <see cref="IsValid"/> is false, i.e. in case of a failed authentication.
        /// </summary>
        public string ErrorMessage { get; } = string.Empty;
    }


    /// <summary>
    /// The result of a call to <see cref="MSAL.LoginUserInteractiveWAMAsync"/> or <see cref="MSAL.LoginUserInteractiveWebViewAsync"/> method.
    /// <para>It is returned in either case of a success or failure. The <see cref="IsValid"/> indicates the status.</para>
    /// </summary>
    public class PublicAuthenticationResult : ClientAuthenticationResult
    {
        bool IsSignOutDone = false;

        /* construction */
        /// <summary>
        /// Constructor.
        /// </summary>
        public PublicAuthenticationResult(IPublicClientApplication ClientApplication, AuthenticationResult AuthResult, string ErrorMessage)
            : base(ClientApplication, AuthResult, ErrorMessage)
        {
        }

        /* public */
        /// <summary>
        /// Logs-out the user.
        /// </summary>
        public void SignOut()
        {
            if (IsValid && !IsSignOutDone)
            {
                MSAL.LogOutUser(ClientApplication, AuthenticationResult);
            }
        }

        /* properties */
        /// <summary>
        /// The client application instance.
        /// </summary>
        public IPublicClientApplication Client { get { return base.ClientApplication as IPublicClientApplication; } }
        /// <summary>
        /// The account. 
        /// <para><strong>WARNING</strong>: Null on failure.</para>
        /// </summary>
        public IAccount Account { get { return AuthenticationResult.Account; } }
        /// <summary>
        /// The user name.
        /// <para><strong>WARNING</strong>: Null on failure.</para>
        /// </summary>
        public string UserName { get { return Account.Username; } }
    }


    /// <summary>
    /// The result of a call to <see cref="MSAL.LoginClientCredentialsAsync"/> or method.
    /// <para>It is returned in either case of a success or failure. The <see cref="IsValid"/> indicates the status.</para>
    /// </summary>
    public class ConfidentialAuthenticationResult : ClientAuthenticationResult
    {

        /* construction */
        /// <summary>
        /// Constructor.
        /// </summary>
        public ConfidentialAuthenticationResult(IConfidentialClientApplication ClientApplication, AuthenticationResult AuthResult, string ErrorMessage)
            : base(ClientApplication, AuthResult, ErrorMessage)
        {
        }

        /* properties */
        /// <summary>
        /// The client application instance.
        /// </summary>
        public IConfidentialClientApplication Client { get { return base.ClientApplication as IConfidentialClientApplication; } }

    }
}
