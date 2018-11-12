namespace EasyOne.Controls.Editor
{
    using System;
    using System.Runtime.InteropServices;

    internal static class NativeMethods
    {
        [DllImport("msvcrt.dll", SetLastError=true)]
        internal static extern int _mkdir(string path);
    }
}

