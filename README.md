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
## カスタム設定(JSONの書き方)
name     :  表示するページの名前(あ行であればkana_row_a)<br>
basis    :  default状態であるかの値(kana_basisであればtrue、kana_row_aであればfalse)<br>
keyname  :  値を変更しないでください<br>
action   :  "send"→送信、"transition"→遷移
value    :  入力する時にshiftを使うものには、値に"shift"を入力する必要があります。<br>
            あ、aを送信するなら[["a"]]、Aを送信するなら[["shift","a"]]、かを送信するならば[["k"],["a"]]、!を送信するならば[["shift","1"]]になります<br>
text     :  表示するテキストを設定します→全角の！を表示するならば、value:[["shift","1"]]、text:"！(全角)"で設定してください
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
