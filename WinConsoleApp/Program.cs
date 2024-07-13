using MSALib;

void AppendLine(string Text = "--------------------------------------------")
{
    Console.WriteLine(Text);
}


PublicAuthenticationResult PublicClientResult = null;

AppendLine("Microsoft Authentication Library Sign-in test.");
AppendLine();
AppendLine("Hit Enter key to sign-in");
Console.ReadLine();


IntPtr ConsoleHandle = MsWindows.GetConsoleWindowHandle();

MSALApp AA = new MSALApp();
PublicClientResult = MSAL.LoginUserInteractiveWAM(AA.DisplayName, AA.TenantId, AA.ClientId, ConsoleHandle);

if (PublicClientResult.IsValid)
{
    AppendLine("Login. DONE");
    AppendLine(MSAL.GetAsText(PublicClientResult.AuthenticationResult));
    AppendLine();

    AppendLine("Hit Enter key to log-out");
    Console.ReadLine();
    PublicClientResult.SignOut();
    AppendLine("Logout. DONE");
    AppendLine();    
}
else
{
    AppendLine("Login. FAILED");
    AppendLine(PublicClientResult.ErrorMessage);
    AppendLine();
}

AppendLine("Hit Enter key to exit");
Console.ReadLine();




