; �ű��� Inno Setup �ű��� ���ɣ�
; �йش��� Inno Setup �ű��ļ�����ϸ��������İ����ĵ���

#define MyAppName "PXS"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "�ɶ����Ƽ����޹�˾"
#define MyAppURL "http://www.binhong-tech.com/"
#define MyAppExeName "BhArinc.exe"
#define ScriptCurDir ".\"
#define MyOutputBaseFilename "BhArinc"

[Setup]
; ע: AppId��ֵΪ������ʶ��Ӧ�ó���
; ��ҪΪ������װ����ʹ����ͬ��AppIdֵ��
; (�����µ�GUID����� ����|��IDE������GUID��)
AppId={{66EF5DF6-1DCC-4A52-B7A1-595F686F6BA3}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile={#ScriptCurDir}..\�û����Э��.txt
OutputDir={#ScriptCurDir}
OutputBaseFilename={#MyOutputBaseFilename}
;SetupIconFile={#ScriptCurDir}PhoenixBootstrap\PXILOGO.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "chinesesimp"; MessagesFile: "compiler:Default.isl"
Name: "english"; MessagesFile: "compiler:Languages\English.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "{#ScriptCurDir}Tmp\*"; DestDir: "{app}"; Flags: ignoreversion
;Source: "{#ScriptCurDir}Tmp\Tools\*"; DestDir: "{app}\Tools\"; Flags: ignoreversion
;Source: "{#ScriptCurDir}Tmp\Config\*"; DestDir: "{app}\Config\"; Flags: ignoreversion
;Source: "{#ScriptCurDir}Tmp\Config\LangConfig\*"; DestDir: "{app}\Config\LangConfig\"; Flags: ignoreversion
;Source: "{#ScriptCurDir}Tmp\RefDll\*"; DestDir: "{app}\RefDll\"; Flags: ignoreversion
;Source: "{#ScriptCurDir}Tmp\RefDll\tools\*"; DestDir: "{app}\RefDll\tools\"; Flags: ignoreversion
;Source: "{#ScriptCurDir}Tmp\RefDll\tools\gcc\*"; DestDir: "{app}\RefDll\tools\gcc\"; Flags: ignoreversion
;Source: "{#ScriptCurDir}Tmp\RefDll\tools\lib\gcc-lib\mingw32\3.2.3\*"; DestDir: "{app}\RefDll\tools\lib\gcc-lib\mingw32\3.2.3\"; Flags: ignoreversion
; ע��: ��Ҫ���κι���ϵͳ�ļ���ʹ�á�Flags: ignoreversion��

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}";

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
[Code]
//����Ƿ�����ض��汾��.net framework
function IsDotNetDetected(version: string; service:cardinal): boolean;
// Indicates whether the specified version and service pack of the .NET Framework is installed.
//
// version -- Specify one of these strings for the required .NET Framework version:
//    'v1.1.4322'     .NET Framework 1.1
//    'v2.0.50727'    .NET Framework 2.0
//    'v3.0'          .NET Framework 3.0
//    'v3.5'          .NET Framework 3.5
//    'v4\Client'     .NET Framework 4.0 Client Profile
//    'v4\Full'       .NET Framework 4.0 Full Installation
//    'v4.5'          .NET Framework 4.5
//
// service -- Specify any non-negative integer for the required service pack level:
//    0               No service packs required
//    1, 2, etc.      Service pack 1, 2, etc. required
var
    key: string;
    install: cardinal;
    success: boolean;
begin 
    Result := false;
    // .NET 4.0���ϰ汾���      
    key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full';
    success := RegQueryDWordValue(HKLM, key, 'Install', install );
    Result := success and (install <> 0);

    // .NET 4.0���°汾��� 
    if not result then begin
      key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\' + version;
      success := RegQueryDWordValue(HKLM, key, 'Install', install);
      Result := success and  (install <> 0);
    end
end;


function InitializeSetup(): Boolean;
var 
  dotNetVersion:string;
begin
    Result := true;
  
    dotNetVersion := 'v4.0'     
    if not IsDotNetDetected(dotNetVersion,0) then begin      
       MsgBox('���Ȱ�װ .NET Framework 4.0��.NET Framework 4.0���ϵ����л�����', mbInformation, MB_OK);
      Result := false;
    end;  
end;

