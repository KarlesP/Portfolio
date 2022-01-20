$username = "username" 
$password = "password"
$credentials = New-Object System.Management.Automation.PSCredential -ArgumentList @($username,(ConvertTo-SecureString -String $password -AsPlainText -Force))

function Show-Menu
{
    param (
        [string]$Title = 'Menu'
    )
    Clear-Host
    Write-Host "================ $Title ================"
    
    Write-Host "1: Press '1' in case you want to see every available printer for installation."
    Write-Host "2: Press '2' in case you want to execute a command remotely to a PC."
    Write-Host "3: Press '3' in case you want to manage a user account."

    Write-Host "999: Press '999' in case you want to access MMC."

    Write-Host "Q: Press 'Q' to quit."
}
Do{
Show-Menu
$choice = Read-Host -Prompt "What would you like to do today ?"

If ($choice -eq 1 {

"So, here are the available printers for installation"
"WebServer1: "
"==========================================================================================================="
Get-Printer -computername WebServer1
"-"
"WebServer2: "
"==========================================================================================================="
Get-Printer -computername WebServer2
"-"
"==========================================================================================================="

pause
}
ElseIf ($choice -eq 2) {
"So, you chose to execute commands remotely, very well"
Start-process powershell -WindowStyle Maximized -Argument "ExecuteCommandRemotely.ps1" -Credential ($credentials)
}
ElseIf ($choice -eq 3) {
"So, you chose to manage a user, very well"
Start-process powershell -WindowStyle Maximized -Argument "UserManage.ps1" -Credential ($credentials)
}

ElseIf ($choice -eq 999) {
"So, you chose to open Mmc, very well"
Start-process powershell -WindowStyle Minimized -Argument "path\to\mmc.exe" -Credential ($credentials)
}
ElseIf ($choice -eq 'q' -or $choice -eq 'Q') {exit}
$exitBig=Read-Host -Prompt "Something else? "
}Until($exitBig -eq "no" -or $exitBig -eq "n" -or $exitBig -eq "No" -or $exitBig -eq "N")

