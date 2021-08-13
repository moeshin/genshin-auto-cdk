using System;

namespace genshin_auto_cdk
{
    internal class InitCommand: BaseCommand
    {
        private int _step;

        internal override void OnHotKey()
        {
            switch (_step)
            {
                case 0:
                    Program.Config.PastePoint = GetMousePoint();
                    break;
                case 1:
                    Program.Config.ButtonPoint = GetMousePoint();
                    break;
                case 2:
                    Program.Config.DialogPoint = GetMousePoint();
                    break;
                case 3:
                    Program.Config.ErrorPoint = GetMouseColorPoint();
                    break;
            }

            if (++_step > 3)
            {
                Program.Config.Save();
                Program.Exit();
                return;
            }
            Start();
        }

        internal override void Start()
        {
            switch (_step)
            {
                case 0:
                    Console.WriteLine("校准「粘贴」按钮");
                    break;
                case 1:
                    Console.WriteLine("校准「兑换」按钮");
                    break;
                case 2:
                    Console.WriteLine("校准「确定」按钮");
                    break;
                case 3:
                    Console.WriteLine("校准错误提示");
                    break;
            }
        }
    }
}