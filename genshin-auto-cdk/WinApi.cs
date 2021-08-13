using System;
using System.Runtime.InteropServices;

namespace genshin_auto_cdk
{
    internal static class WinApi
    {
#pragma warning disable 649
        // ReSharper disable InconsistentNaming, IdentifierTypo
        internal struct Message
        {

            internal int hwnd;
            internal uint message;
            internal int wParam;
            internal int lParam;
            internal int time;
            internal Point pt;
        }

        internal struct Point
        {
            internal int x;
            internal int y;
        }
        // ReSharper restore InconsistentNaming, IdentifierTypo
#pragma warning restore 649

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool RegisterHotKey(
            int hWnd,
            int id,
            int fsModifiers,
            int vk
        );

        [DllImport("user32.dll")]
        internal static extern bool GetMessageA(
            out Message lpMsg,
            int hWnd,
            uint wMsgFilterMin,
            uint wMsgFilterMax
        );

        [DllImport("user32.dll")]
        internal static extern bool TranslateMessage(ref Message lpMsg);

        [DllImport("user32.dll")]
        internal static extern bool DispatchMessageA(ref Message lpMsg);

        [DllImport("user32")]
        internal static extern void mouse_event(
            int dwFlags,
            int dx,
            int dy,
            int dwData,
            int dwExtraInfo
        );

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        internal static extern int GetDC(int hWnd);

        [DllImport("user32.dll")]
        internal static extern bool ReleaseDC(int hWnd, int hDc);

        [DllImport("gdi32.dll")]
        internal static extern int GetPixel(int hDc, int nXPos, int nYPos);

        [DllImport("user32.dll")]
        internal static extern int GetForegroundWindow();
        
        [DllImport("user32.dll")]
        internal static extern int OpenClipboard(int hWndNewOwner);
        
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool EmptyClipboard();
        
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool CloseClipboard();
        
        [DllImport("user32.dll")]
        internal static extern int SetClipboardData(int uFormat, int hMem);
        
        [DllImport("kernel32.dll")]
        internal static extern int GlobalAlloc(uint wFlags, int dwBytes);
        
        [DllImport("kernel32.dll")]
        internal static extern int GlobalLock(int hMem);
        
        [DllImport("kernel32.dll")]
        internal static extern int GlobalUnlock(int hMem);
        
        [DllImport("kernel32.dll")]
        internal static extern int RtlMoveMemory(int dest, string source, int length);
        
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int MessageBeep(uint uType);

        // ReSharper disable once UnusedMember.Global
        internal static T TryTestLastError<T>(T any)
        {
            TryTestLastError();
            return any;
        }

        // ReSharper disable once UnusedMethodReturnValue.Global
        // ReSharper disable once MemberCanBePrivate.Global
        internal static bool TryTestLastError()
        {
            var err = Marshal.GetLastWin32Error();
            if (err == 0)
            {
                return true;
            }
            Console.WriteLine("LastError: " + err);
            return false;
        }
    }
}