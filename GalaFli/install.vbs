dim arg, args, desktop, WshShell, WshShortcut

'CustomActionDataを分解
arg = Session.Property("CustomActionData")
args = Split(arg, ":::")

if args(2)="1" then
    set WshShell = CreateObject("WScript.Shell")
    'デスクトップのパスを取得
    if args(3)="1" then
        desktop = WshShell.SpecialFolders("AllUsersDesktop")
    else
        desktop = WshShell.SpecialFolders("Desktop")
    end if
    'ショートカットを作成
    On Error Resume Next
    set WshShortcut = _
        WshShell.CreateShortcut(desktop & "\" & args(1) & ".lnk")
    WshShortcut.TargetPath = args(0)
    WshShortcut.IconLocation = "C:\Users\s212127.TSITCL\source\repos\test_2\test_2\bin\Debug\icon.ico"
    WshShortcut.Save
end if