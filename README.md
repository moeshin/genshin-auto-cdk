# 原神自动兑换

[![Release](https://img.shields.io/github/v/release/moeshin/genshin-auto-cdk?style=flat-square)](https://github.com/moeshin/genshin-auto-cdk/releases/latest)
[![License](https://img.shields.io/github/license/moeshin/genshin-auto-cdk?style=flat-square)](https://github.com/moeshin/genshin-auto-cdk/blob/master/LICENSE)

运行时需要 **管理员权限**，可以右击 `*.cmd` 脚本选择「**以管理员身份运行**」。

```text
.\genshin-auto-cdk.exe init             初始化、校准
.\genshin-auto-cdk.exe read             从终端读取 CDK 列表，输入完按 Ctrl + Z 或 F6 然后按回车开始
.\genshin-auto-cdk.exe file [list.txt]  从文件读取 CDK 列表
```

### 关于热键

程序注册一个全局热键：`Ctrl + Shift + Z`

* 在 `init` 模式下，按下它来获取鼠标位置等信息
* 在 `read` 和 `file` 模式下，按下它来暂停、继续自动兑换

### 配置文件 `Config.json5`

默认参数是 `1920x1080` 下的

```json5
{
  // 「粘贴」按钮
  "PastePoint": {
    "X": 1283,
    "Y": 506
  },

  // 「兑换」按钮
  "ButtonPoint": {
    "X": 1171,
    "Y": 730
  },

  // 兑换成功弹出对话框的「确定」按钮
  "DialogPoint": {
    "X": 964,
    "Y": 756
  },

  // 错误提示
  // 在兑换出错时，「兑换」按钮上会有一条红色的提示，取靠边的红色
  "ErrorPoint": {
    "Color": 13588549,
    "X": 588,
    "Y": 586
  },
}
```
