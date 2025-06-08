using System.Runtime.InteropServices;
using System.Text;

namespace EasySharp.Utility;

public static class ClipboardHelper
{
    [DllImport("user32.dll")]
    private static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll")]
    private static extern bool CloseClipboard();

    [DllImport("user32.dll")]
    private static extern bool SetClipboardData(uint uFormat, IntPtr hMem);

    [DllImport("user32.dll")]
    private static extern bool EmptyClipboard();

    [DllImport("kernel32.dll")]
    private static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GlobalLock(IntPtr hMem);

    [DllImport("kernel32.dll")]
    private static extern bool GlobalUnlock(IntPtr hMem);

    private const uint CF_TEXT = 1;
    private const uint GMEM_MOVEABLE = 0x0002;

    public static void SetText(string text)
    {
        if (!OpenClipboard(IntPtr.Zero))
            return;

        EmptyClipboard();

        byte[] data = Encoding.UTF8.GetBytes(text + '\0');
        IntPtr hGlobal = GlobalAlloc(GMEM_MOVEABLE, (UIntPtr)data.Length);

        if (hGlobal != IntPtr.Zero)
        {
            IntPtr ptr = GlobalLock(hGlobal);
            if (ptr != IntPtr.Zero)
            {
                Marshal.Copy(data, 0, ptr, data.Length);
                GlobalUnlock(hGlobal);
                SetClipboardData(CF_TEXT, hGlobal);
            }
        }

        CloseClipboard();
    }
}