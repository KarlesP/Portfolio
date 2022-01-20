$Machine = Read-Host -Prompt "Name of the Machine: "
#Enable Remote PS execution
Invoke-WmiMethod -ComputerName $Machine -Namespace root\cimv2 -Class Win32_Process -Name Create -Credential $cred -Impersonation 3 -EnableAllPrivileges -ArgumentList "powershell Start-Process powershell -WindowStyle Minimized -Verb runAs -ArgumentList 'Enable-PSRemoting –force'"
function Show-Menu
{
    param (
        [string]$Title = 'Menu'
    )
    Clear-Host
    Write-Host "================ $Title ================"
    
    Write-Host "1: Press '1' to execute a custom command remotely."
    Write-Host "2: Press '2' to install a network printer."
    Write-Host "3: Press '3' to access 'C:\Users' directory remotely."
    Write-Host "4: Press '4' to copy a file from the remote machine."
    Write-Host "5: Press '5' to copy a file to the remote machine."
    Write-Host "6: Press '6' to get logged in user(s)."
    Write-Host "7: Press '7' to sign-out every user from the current machine."
    Write-Host "8: Press '8' to restart the current machine."
    Write-Host "9: Press '9' to shutdown the current machine."
    Write-Host "Q: Press 'Q' to quit."
}
Do {
Show-Menu

$choice = Read-Host -Prompt "What should we do? "

If ($choice -eq 1) {
Do {
    $command = read-host "Command to run"
    Invoke-Command -ComputerName $Machine -ScriptBlock { invoke-expression $using:command }

    $exit=Read-Host -Prompt "Should I execute another command? "
    }
Until ($exit -eq "no" -or $exit -eq "n" -or $exit -eq "No" -or $exit -eq "N")

}
ElseIf ($choice -eq 2) {
"Adding Printer..."
$pathToPrinter = read-host "Please provide the printer (i.e \\printServer\printerName)"
Invoke-Command -ComputerName $Machine -ScriptBlock {Add-Printer -ConnectionName $pathToPrinter}
}
ElseIf ($choice -eq 3){
"Accessing C:\Users"
Invoke-Command –ComputerName $Machine -ScriptBlock {Get-ChildItem C:\Users}
}
ElseIf ($choice -eq 4){
New-Item -ItemType Directory -Force -Path C:\Users\Public\$Machine
$file = read-host "What should I copy to 'C:\Users\Public' ?"
$client = New-PSSession $Machine
Copy-Item -FromSession $client $file -Destination C:\Users\Public\$Machine\
}

ElseIf ($choice -eq 5){
New-Item -ItemType Directory -Force -Path C:\Users\Public\$Machine
$fileLocal = read-host "What should I copy from your machine ?"
$fileRemote = read-host "Where should I copy it to?"
$client = New-PSSession $Machine
Copy-Item $fileLocal -ToSession $client $fileRemote}


ElseIf ($choice -eq 6){
"The current logged in user is.."
Get-WmiObject -Class win32_computersystem -ComputerName $Machine | select username
}
ElseIf ($choice -eq 7) {
"Signing out users..."
Invoke-Command -ComputerName $Machine -ScriptBlock { logoff * }
}
ElseIf ($choice -eq 8) {
"Restarting Machine..."
Restart-Computer -ComputerName $Machine
}
ElseIf ($choice -eq 9) {
"Shuting down Machine..."
 Stop-Computer -ComputerName $Machine
}
ElseIf ($choice -eq 'q' -or $choice -eq 'Q') {
exit}

$exitBig=Read-Host -Prompt "Something else? "
}
Until ($exitBig -eq "no" -or $exitBig -eq "n" -or $exitBig -eq "No" -or $exitBig -eq "N")