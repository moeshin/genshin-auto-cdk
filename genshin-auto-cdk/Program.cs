using System;
using System.Runtime.InteropServices;

namespace genshin_auto_cdk
{
    internal static class Program
    {
        private const int HotKeyHandle = 0;
        private const int HotKeyId = 0x04f4;
        internal static Config Config;

        internal static void Main(string[] args)
        {
            var command = args.Length == 0 ? null : args[0];
            if (command == null)
            {
                Usage();
                return;
            }

            Config = Config.Get();

            BaseCommand baseCommand;

            switch (command)
            {
                case "init":
                    baseCommand = new InitCommand();
                    break;
                case "read":
                    baseCommand = new WorkCommand();
                    break;
                case "file":
                    baseCommand = new WorkCommand((args.Length == 1 ? null : args[1]) ?? "list.txt");
                    break;
                default:
                    Usage();
                    return;
            }

            if (!WinApi.RegisterHotKey(HotKeyHandle, HotKeyId, 0x2 | 0x4, 90))
            {
                var err = Marshal.GetLastWin32Error();
                if (err == 1409)
                {
                    Console.WriteLine("热键被占用 ！");
                }
                else
                {
                    Console.WriteLine("注册热键失败！错误代码：" + err);
                }
                return;
            }
            Console.WriteLine("注册全局热键：Ctrl + Shift + Z");
            baseCommand.Start();
            while (WinApi.GetMessageA(out var msg, HotKeyHandle, 0, 0))
            {
                if (msg.message == 0x0312) // WM_HOTKEY
                {
                    Console.WriteLine("热键按下");
                    try
                    {
                        // OnHotKey();
                        baseCommand.OnHotKey();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                WinApi.TranslateMessage(ref msg);
                WinApi.DispatchMessageA(ref msg);
            }
        }

        internal static void Exit()
        {
            WinApi.MessageBeep(0);
            Environment.Exit(0);
        }

        private static void Usage()
        {
           Console.WriteLine(@"使用说明：
.\genshin-auto-cdk.exe init             初始化、校准
.\genshin-auto-cdk.exe read             从终端读取 CDK 列表，输入完按 Ctrl + Z 或 F6 然后按回车开始
.\genshin-auto-cdk.exe file [list.txt]  从文件读取 CDK 列表");
        }
    }
}
