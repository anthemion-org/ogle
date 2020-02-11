; Ogle.iss
; ========
; Copyright ©2011 Anthemion Industries

[Setup]
AppName=Ogle
AppVersion=0.2.2
DefaultDirName={pf}\Ogle
DefaultGroupName=Ogle
; Oldest supported Windows version is Vista:
MinVersion=6.0.6000
UninstallDisplayIcon={app}\Ogle.exe
Compression=lzma
SolidCompression=true
OutputBaseFilename=ogle_setup
AppCopyright=Copyright 2011 Jeremy Kelly
AppVerName=Ogle 0.2.2
AppSupportURL=https://www.anthemion.org/ogle.html
UninstallDisplayName=Ogle 0.2.2
SetupIconFile=..\Ogle\Ogle.ico
AppPublisher=Jeremy Kelly
AppPublisherURL=https://www.anthemion.org

[Dirs]
Name: {app}\Sound
Name: {userappdata}\Ogle\Lexicon

[Icons]
Name: {group}\Ogle; Filename: {app}\Ogle.exe; WorkingDir: {app}
Name: {group}\Uninstall Ogle; Filename: {uninstallexe}
Name: {group}\Ogle Help; Filename: https://www.anthemion.org/ogle_help/index.html
Name: {commondesktop}\Ogle; Filename: {app}\Ogle.exe; Tasks: IconDesk\All
Name: {userdesktop}\Ogle; Filename: {app}\Ogle.exe; Tasks: IconDesk\Curr

[Tasks]
Name: IconDesk; Description: Create a &desktop icon; GroupDescription: Additional icons:; Flags: unchecked
Name: IconDesk\All; Description: For all users; GroupDescription: Additional icons:; Flags: exclusive unchecked
Name: IconDesk\Curr; Description: For the current user only; GroupDescription: Additional icons:; Flags: exclusive unchecked

[Files]
; This Microsoft executable downloads and installs the framework:
Source: InstallDotNET4.5.2.exe; DestDir: {tmp}; Flags: DeleteAfterInstall; Check: CkInstallDotNET

Source: ..\Ogle\bin\Release\Ogle.exe; DestDir: {app}
Source: ..\Ogle\bin\Release\GNU General Public License.txt; DestDir: {app}
Source: ..\Ogle\bin\Release\BtnTime.dll; DestDir: {app}
Source: ..\Ogle\bin\Release\Line.dll; DestDir: {app}
Source: ..\Ogle\bin\Release\Microsoft.DirectX.DirectSound.dll; DestDir: {app}
Source: ..\Ogle\bin\Release\Microsoft.DirectX.DirectSound.xml; DestDir: {app}
Source: ..\Ogle\bin\Release\Microsoft.DirectX.dll; DestDir: {app}
Source: ..\Ogle\bin\Release\Microsoft.DirectX.xml; DestDir: {app}
Source: ..\Ogle\bin\Release\SCOWL License.txt; DestDir: {app}

Source: ..\Ogle\bin\Release\Sound\EntInval.wav; DestDir: {app}\Sound\
Source: ..\Ogle\bin\Release\Sound\EntVal.wav; DestDir: {app}\Sound\
Source: ..\Ogle\bin\Release\Sound\MouseOver.wav; DestDir: {app}\Sound\
Source: ..\Ogle\bin\Release\Sound\SelDie.wav; DestDir: {app}\Sound\
Source: ..\Ogle\bin\Release\Sound\Tick.wav; DestDir: {app}\Sound\
Source: ..\Ogle\bin\Release\Sound\TickLast.wav; DestDir: {app}\Sound\
Source: ..\Ogle\bin\Release\Sound\TickLow.wav; DestDir: {app}\Sound\
Source: ..\Ogle\bin\Release\Sound\UnselDie.wav; DestDir: {app}\Sound\

Source: ..\Lexicon\Am.txt; DestDir: {userappdata}\Ogle\Lexicon\
Source: ..\Lexicon\Brit.txt; DestDir: {userappdata}\Ogle\Lexicon\
Source: ..\Lexicon\Can.txt; DestDir: {userappdata}\Ogle\Lexicon\
Source: ..\Lexicon\Eng.txt; DestDir: {userappdata}\Ogle\Lexicon\

[ThirdParty]
CompileLogMethod=append

[Code]
// Indicates whether the specified version and service pack of the .NET
// Framework is installed.
//
// version -- Specify one of these strings for the required .NET Framework
// version:
//	'v1.1'		  .NET Framework 1.1
//	'v2.0'		  .NET Framework 2.0
//	'v3.0'		  .NET Framework 3.0
//	'v3.5'		  .NET Framework 3.5
//	'v4\Client'	 .NET Framework 4.0 Client Profile
//	'v4\Full'	   .NET Framework 4.0 Full Installation
//	'v4.5'		  .NET Framework 4.5
//	'v4.5.1'		.NET Framework 4.5.1
//	'v4.5.2'		.NET Framework 4.5.2
//	'v4.6'		  .NET Framework 4.6
//	'v4.6.1'		.NET Framework 4.6.1
//	'v4.6.2'		.NET Framework 4.6.2
//	'v4.7'		  .NET Framework 4.7
//
// service -- Specify any non-negative integer for the required service pack
// level:
//	0			   No service packs required
//	1, 2, etc.	  Service pack 1, 2, etc. required
//
function IsDotNetDetected(version: string; service: cardinal): boolean;
var
	key, versionKey: string;
	install, release, serviceCount, versionRelease: cardinal;
	success: boolean;
begin
	versionKey := version;
	versionRelease := 0;

	// .NET 1.1 and 2.0 embed release number in version key
	if version = 'v1.1' then begin
		versionKey := 'v1.1.4322';
	end else if version = 'v2.0' then begin
		versionKey := 'v2.0.50727';
	end

	// .NET 4.5 and newer install as update to .NET 4.0 Full
	else if Pos('v4.', version) = 1 then begin
		versionKey := 'v4\Full';
		case version of
		  'v4.5':   versionRelease := 378389;
		  'v4.5.1': versionRelease := 378675; // 378758 on Windows 8 and older
		  'v4.5.2': versionRelease := 379893;
		  'v4.6':   versionRelease := 393295; // 393297 on Windows 8.1 and older
		  'v4.6.1': versionRelease := 394254; // 394271 before Win10 November Update
		  'v4.6.2': versionRelease := 394802; // 394806 before Win10 Anniversary Update
		  'v4.7':   versionRelease := 460798; // 460805 before Win10 Creators Update
		end;
	end;

	// installation key group for all .NET versions
	key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\' + versionKey;

	// .NET 3.0 uses value InstallSuccess in subkey Setup
	if Pos('v3.0', version) = 1 then begin
		success := RegQueryDWordValue(HKLM, key + '\Setup', 'InstallSuccess', install);
	end else begin
		success := RegQueryDWordValue(HKLM, key, 'Install', install);
	end;

	// .NET 4.0 and newer use value Servicing instead of SP
	if Pos('v4', version) = 1 then begin
		success := success and RegQueryDWordValue(HKLM, key, 'Servicing', serviceCount);
	end else begin
		success := success and RegQueryDWordValue(HKLM, key, 'SP', serviceCount);
	end;

	// .NET 4.5 and newer use additional value Release
	if versionRelease > 0 then begin
		success := success and RegQueryDWordValue(HKLM, key, 'Release', release);
		success := success and (release >= versionRelease);
	end;

	result := success and (install = 1) and (serviceCount >= service);
end;

// Returns 'true' if .NET 4.5.2 is not already installed:
function CkInstallDotNET(): boolean;
begin
  Result := IsDotNetDetected('v4.5.2', 0);
end;
