dim arg, args, desktop, path, WshShell, fso

'CustomActionDataを分解
arg = Session.Property("CustomActionData")
args = Split(arg, ":::")

'デスクトップのパスを取得
set WshShell = CreateObject("WScript.Shell")
if args(1)="1" then
    desktop = WshShell.SpecialFolders("AllUsersDesktop")
else
    desktop = WshShell.SpecialFolders("Desktop")
end if
'削除するファイルのパス
path = desktop & "\" & args(0) & ".lnk"
'ショートカットを削除
set fso = CreateObject("Scripting.FileSystemObject")
if (fso.FileExists(path)) then
    On Error Resume Next
    fso.DeleteFile(path)
end if