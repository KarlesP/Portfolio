#$Usr = Read-Host -Prompt "Provide the name of the user: "
#Enable Remote PS execution

function Show-Menu
{ 
    param (
        [string]$Title = 'Menu'
    )
    Clear-Host
    Write-Host "================ $Title ================"
    
    Write-Host "1: Press '1' to check username based on email"
    Write-Host "2: Press '2' to reset new Passord."
    Write-Host "3: Press '3' to check for locked account."
    Write-Host "4: Press '4' to lock an account."
    Write-Host "5: Press '5' to unlock an account."    
    Write-Host "Q: Press 'Q' to quit."
}
Do {
Show-Menu

$choice = Read-Host -Prompt "What should we do? "

If ($choice -eq 1) {
"Please provide an email.."
$mailnn = Read-Host -Prompt "Provide the mail nickname of the user (i.e nickname@xxx.xxx)"
$search=[adsisearcher]"(mailnickname=$mailnn)"
"Username"
$search.FindOne().Properties.cn
"Name"
$search.FindOne().Properties.displayname
"Phone"
$search.FindOne().Properties.telephonenumber
"Email"
$search.FindOne().Properties.userprincipalname
}

ElseIf ($choice -eq 2) {

"Resetting Password"
$Usr = Read-Host -Prompt "Provide the name of the user"
$Pswrd = Read-Host -Prompt "Provide the new password of the user"
NET USER $Usr $Pswrd /DOMAIN

}
ElseIf ($choice -eq 3){
"Checking Active status"
$Usr = Read-Host -Prompt "Provide the name of the user"
NET USER $Usr /DOMAIN | FIND /I "Account active"
}
ElseIf ($choice -eq 4){
"Locking Account"
$Usr = Read-Host -Prompt "Provide the name of the user"
NET USER `loginname` /DOMAIN /ACTIVE:LOCKED
}
ElseIf ($choice -eq 5){
"Unlocking Account"
$Usr = Read-Host -Prompt "Provide the name of the user"
NET USER `loginname` /DOMAIN /ACTIVE:YES
}
ElseIf ($choice -eq 'q' -or $choice -eq 'Q') {
exit}

$exitBig=Read-Host -Prompt "Something else? "
}
Until ($exitBig -eq "no" -or $exitBig -eq "n" -or $exitBig -eq "No" -or $exitBig -eq "N")