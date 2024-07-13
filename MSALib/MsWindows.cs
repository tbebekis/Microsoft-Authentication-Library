using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSALib
{
    /// <summary>
    /// Used with the <see cref="GetConsoleOrTerminalWindowHandle"/> method.
    /// </summary>
    public enum GAFlags
    {
        GetParent = 1,
        GetRoot = 2,
        GetRootOwner = 3
    }


    /// <summary>
    /// Helper with MS Windows API native functions
    /// </summary>
    static public class MsWindows
    {

        /// <summary>
        /// Used with the <see cref="GetConsoleWindowHandle"/> method.
        /// </summary>
        [DllImport("user32.dll", ExactSpelling = true)]
        static extern IntPtr GetAncestor(IntPtr hwnd, GAFlags flags);
        /// <summary>
        /// Used with the <see cref="GetConsoleWindowHandle"/> method.
        /// </summary>
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        /// <summary>
        /// Returns the Handle of the Console Window.
        /// <para>To be used with the <see cref="MSAL.LoginUserInteractiveWAMAsync"/> method when a Console application is used, instead of a Windows.Forms application. </para>
        /// </summary>
        static public IntPtr GetConsoleWindowHandle()
        {
            IntPtr ConsoleHandle = GetConsoleWindow();
            IntPtr Handle = GetAncestor(ConsoleHandle, GAFlags.GetRootOwner);
            return Handle;
        }

    }
}
