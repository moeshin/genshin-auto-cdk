using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace genshin_auto_cdk
{
    internal class WorkCommand: BaseCommand
    {
        private string[] _list;
        private int _index;

        private bool _isRunning;

        internal WorkCommand(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException(file);
            }
            InitList(File.ReadAllText(file));
        }

        internal WorkCommand()
        {
            var sb = new StringBuilder();
            string str;
            while ((str = Console.ReadLine()) != null)
            {
                sb.Append(str);
            }
            InitList(sb.ToString());
        }

        internal override void OnHotKey()
        { 
            if ((_isRunning = !_isRunning) == false)
            {
                Console.WriteLine("暂停自动兑换，按下热键继续");
            }
            else
            {
                Console.WriteLine("开始自动兑换，按下热键暂停");
                Task.Run(Run);
            }
        }

        internal override void Start()
        {
            Console.WriteLine("按热键开始自动兑换，再次按下暂停或继续");
        }

        private async Task Run()
        {
            for (; _index < _list.Length; ++_index)
            {
                if (!_isRunning) return;
                var cdk = _list[_index];
                Console.WriteLine("当前 CDK：" + cdk);
                SetTextToClipboard(cdk);
                MouseClick(Program.Config.PastePoint);
                Console.WriteLine("点击「粘贴」按钮");
                await Task.Delay(500);
                if (!_isRunning) return;
                MouseClick(Program.Config.ButtonPoint);
                Console.WriteLine("点击「兑换」按钮");
                await Task.Delay(500);
                if (!_isRunning) return;
                var cp = Program.Config.ErrorPoint;
                if (GetPixelColor(cp.X, cp.Y) == cp.Color)
                {
                    Console.WriteLine("兑换错误");
                    await Task.Delay(500);
                    MouseClick(Program.Config.PastePoint);
                    await Task.Delay(4000);
                    continue;
                }
                MouseClick(Program.Config.DialogPoint);
                await Task.Delay(4500);
            }
            if (!_isRunning) return;
            Program.Exit();
        }

        private void InitList(string text)
        {
            var match = Regex.Match(text, "\\b[a-zA-Z\\d]{12}\\b");
            var list = new List<string>();
            while (match.Success)
            {
                list.Add(match.Value);
                match = match.NextMatch();
            }
            Console.WriteLine(string.Join("\n", list));
            if (list.Count == 0)
            {
                Console.WriteLine("没有找到 CDK");
                Program.Exit();
                return;
            }
            _list = list.ToArray();
        }
    }
}