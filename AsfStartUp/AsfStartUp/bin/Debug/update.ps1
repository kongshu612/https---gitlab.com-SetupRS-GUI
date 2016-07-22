param(
[Parameter(Mandatory=$false)]
[bool]$restart=$true
)
Function Log-Message
{
	param(
	[Parameter(Mandatory=$true)]
	[string]$message
	)
	Add-Content c:\programdata\ASFStartUp\Update.txt $message
	#Add-Content c:\programdata\ASFStartUp\Update.txt "\r\n"
}
if(Test-Path c:\programdata\ASFStartUp\Update.txt)
{
	remove-item -path c:\programdata\ASFStartUp\Update.txt -force
}
new-Item -type File -path c:\programdata\ASFStartUp\Update.txt
Log-Message "Start to update packets"
$Location = (Get-Location).Path
Log-Message "Get Current Path: $Location"
Log-Message "Close the ASFStartUp Process"
$count = 5
while($count -gt 1)
{
	if((gps | ?{$_.name -eq "AsfStartUp"}) -eq $null)
	{
		Log-Message "the AsfStartUp process has been stopped"
		break
	}
	sleep 5
	$count--
	Log-Message "Wait for the process exit"
}
try
{
gps -name ASFStartUp | stop-process -Force
sleep 5
}
catch
{}
Log-Message "Test if Both update Packets and Current Packets exist"
if(-not (Test-Path .\ASFStartUpNew) -or -not (Test-Path .\ASFStartUp))
{
	Log-Message "Do Not Find the both packets. Update Failed"
	return;
}
Log-Message "Try to rename the Current Packets"
try
{
	rename-item -Path .\ASFStartUp -NewName ASFStartUp-del -Force
}
catch
{
	Log-Message "Rename the Current Packets Failed"
	return;
}
Log-Message "Try to replace the name of update Packets"
try
{
	rename-item -Path .\ASFStartUpNew -NewName ASFStartUp -Force
}
catch
{
	Log-Message "Rename the Update Packets Failed"
	return;
}
if($restart)
{
	Log-Message "Start the ASFStartUp again"
	& .\ASFStartUp\AsfStartUp.exe
}
Log-Message "Delete the deprecated Packets"
try
{
	Remove-item -path .\ASFStartUp-del -recurse -Force
}
catch
{
	Log-Message "Remove the deprecatedPackets failed"
}
return;