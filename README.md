# GalaFli
**Galapagos × Flick**
## 概要
テンキーだけでキーボード入力ができるようになるWindows常駐ソフト。<br>
ガラケー入力のように限られたキー数で、フリック入力のように十字の操作でキーボード全て
の入力ができるようになります。
## 使い方
USB接続のテンキーを用意して接続し、GalaFliを起動してください。
## 設定
* 初回起動時、ドライバがインストールされていない場合、自動でインストールされます。<br>
ドライバインストール後は再起動が必要となります。<br><br>
* 使用するUSB接続のテンキーを設定する必要があります。<br>
設定画面から、使用するテンキーのハードウェアIDを選択してください。
## 導入方法
Releases内のInstaller(GalaFli-Installer.msi)を使用してください。
## カスタム設定方法
all_data.jsonを下記のように記述してください。<br><br>
` "name" : "custom_basis_1" `<br>
` "keys" : [] `
* ` "action":"send" `<br>
* ` "value": null `<br>
分割打ち：「や」を入力する場合、` [["y"]["a"]] `を格納してください。<br>
同時打ち：「！」を入力する場合、` [["shift","1"]] `を格納してください。<br>
※格納するデータは下記のdictionaryを参照してください。

* ` "text":"" ` <br>
表示するテキストを格納してください。<br>
※4byteを超えるとボタンが崩れます。<br><br>

```csharp:data
{"leftclick",0x01},{"rightclick",0x02},{"ctrlpause",0x03},{"middleclick",0x04},{"bs",0x08},{"tab",0x09},{"enter",0x0D},
{"shift",0x10},{"ctrl",0x11},{"alt",0x12},{"pause",0x13},{"shiftcapslock",0x14},{"althankaku/zenkaku",0x19},{"esc",0x1B},{"conversion",0x1C},{"noconversion",0x1D},
{"space",0x20},{"pageup",0x21},{"pagedown",0x22},{"end",0x23},{"home",0x24},{"leftarrow",0x25},{"uparrow",0x26},{"rightarrow",0x27},{"down",0x28},{"printscreen",0x2C},{"insert",0x2D},{"delete",0x2E},
{"0",0x30},{"1",0x31},{"2",0x32},{"3",0x33},{"4",0x34},{"5",0x35},{"6",0x36},{"7",0x37},{"8",0x38},{"9",0x39},
{"a",0x41},{"b",0x42},{"c",0x43},{"d",0x44},{"e",0x45},{"f",0x46},{"g",0x47},{"h",0x48},{"i",0x49},{"j",0x4A},{"k",0x4B},{"l",0x4C},{"m",0x4D},{"n",0x4E},{"o",0x4F},
{"p",0x50},{"q",0x51},{"r",0x52},{"s",0x53},{"t",0x54},{"u",0x55},{"v",0x56},{"w",0x57},{"x",0x58},{"y",0x59},{"z",0x5A},{"leftwin",0x5B},{"rightwin",0x5C},{"app",0x5D},
{"F1",0x70},{"F2",0x71},{"F3",0x72},{"F4",0x73},{"F5",0x74},{"F6",0x75},{"F7",0x76},{"F8",0x77},{"F9",0x78},{"F10",0x79},{"F11",0x7A},{"F12",0x7B},{"F13",0x7C},{"F14",0x7D},{"F15",0x7E},{"F16",0x7F},
{"F17",0x80},{"F18",0x81},{"F19",0x82},{"F20",0x83},{"F21",0x84},{"F22",0x85},{"F23",0x86},{"F24",0x87},
{"numlock",0x90},{"scrolllock",0x91},
{"leftshift",0xA0},{"rightshift",0xA1},{"leftctrl",0xA2},{"rightctrl",0xA3},{"leftalt",0xA4},{"rightalt",0xA5},{"soundmute",0xAD},{"sounddown",0xAE},{"soundup",0xAF},
{":*",0xBA},{";+",0xBB},{",<",0xBC},{"-=",0xBD},{".>",0xBE},{"/?",0xBF},
{"@`",0xC0},
{"[{",0xDB},{"\\|",0xDC},{"]}",0xDD},{"^~",0xDE},
{"\\_",0xE2},
{"capslock",0xF0},{"hiragana",0xF2 },{"hankaku/zenkaku",0xF3 },{"althiragana",0xF5}
```

## 動作環境
Windows 10 以降。 <br><br><br><br>

## About
Windows resident software that allows keyboard input using only the numeric keypad.<br>
The keyboard can be used with a limited number of keys, as in Galapagos input, and all keyboard input can be done with a cross operation, as in flick input.
## How to use
Prepare and connect a USB-connected numeric keypad and start GalaFli.
## Setting
* If the driver is not installed at first startup, it will be installed automatically.<br>
A reboot is required after installing the driver.<br><br>
* The USB-connected numeric keypad to be used must be configured.<br>
From the settings screen, select the hardware ID of the numeric keypad to be used.
## Introduction Method
Installer (GalaFli-Installer.msi) in Releases.
## Operating Environment
Windows 10 or later.

<br><br>
© 2023 yonrise
