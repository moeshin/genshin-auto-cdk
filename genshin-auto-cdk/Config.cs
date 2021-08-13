using System.IO;
using Newtonsoft.Json;

namespace genshin_auto_cdk
{
    // 默认参数是 1920x1080 下的
    public class Config
    {
        private const string Name = "Config.json5";

        // 「粘贴」按钮
        public Point PastePoint { get; set; } = new Point
        {
            X = 1283,
            Y = 506
        };

        // 「兑换」按钮
        public Point ButtonPoint { get; set; } = new Point
        {
            X = 1171,
            Y = 730
        };
        
        // 兑换成功弹出对话框的「确定」按钮
        public Point DialogPoint { get; set; } = new Point
        {
            X = 964,
            Y = 756
        };

        // 错误提示
        public ColorPoint ErrorPoint { get; set; } = new ColorPoint()
        {
            Color = 13588549,
            X = 588,
            Y = 586
        };

        internal static Config Get()
        {
            return File.Exists(Name)
                ? JsonConvert.DeserializeObject<Config>(File.ReadAllText(Name))
                : new Config();
        }

        internal void Save()
        {
            File.WriteAllText(Name, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        internal void FromWinApi(WinApi.Point point)
        {
            X = point.x;
            Y = point.y;
        }
    }
    
    public class ColorPoint: Point
    {
        public int Color { get; set; }
    }
}