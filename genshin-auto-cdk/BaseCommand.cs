namespace genshin_auto_cdk
{
    internal abstract class BaseCommand
    {
        internal abstract void OnHotKey();

        internal abstract void Start();

        internal static int GetPixelColor(int x, int y)
        {
            var hDc = WinApi.GetDC(0);
            var pixel = WinApi.GetPixel(hDc, x, y);
            WinApi.ReleaseDC(0, hDc);
            return ((pixel & 0x0000ff) << 16) | (pixel & 0x00ff00) | ((pixel & 0xff0000) >> 16);
        }

        private static void MouseClick(int x, int y)
        {
            WinApi.SetCursorPos(x, y);
            WinApi.mouse_event(0x2 | 0x4, x, y, 0, 0);
        }
        
        internal static void MouseClick(Point point)
        {
            MouseClick(point.X, point.Y);
        }

        internal static void SetTextToClipboard(string text)
        {
            var len = text.Length;
            var hGlobalMemory = WinApi.GlobalAlloc(2, len + 1);
            var lpGlobalMemory = WinApi.GlobalLock(hGlobalMemory);
            WinApi.RtlMoveMemory(lpGlobalMemory, text, len);
            WinApi.GlobalUnlock(hGlobalMemory);
            var hWnd = WinApi.GetForegroundWindow();
            WinApi.OpenClipboard(hWnd);
            WinApi.EmptyClipboard();
            WinApi.SetClipboardData(1, hGlobalMemory);
            WinApi.CloseClipboard();
        }

        internal static Point GetMousePoint()
        {
            WinApi.GetCursorPos(out var point);
            var p = new Point();
            p.FromWinApi(point);
            return p;
        }

        internal static ColorPoint GetMouseColorPoint()
        {
            WinApi.GetCursorPos(out var point);
            var p = new ColorPoint();
            p.FromWinApi(point);
            p.Color = GetPixelColor(point.x, point.y);
            return p;
        }
    }
}