set isPublish=1

set currentPath=%~dp0


rem ��ȡ��ǰ���ں�ʱ��
for /f "tokens=2 delims==" %%a in ('wmic path win32_operatingsystem get LocalDateTime /value') do (
  set t=%%a
)
set Now=%t:~0,4%-%t:~4,2%-%t:~6,2%-%t:~8,2%-%t:~10,2%


rem ����publish�ļ��е�����

set publishDir=BhArinc %Now%
cd /d %currentPath%

rem �����Ŀ��������dotnet�汾�����ܴ���4.5
set versionIsBad=false
for /r %%f in (*.csproj) do (
	for /f "usebackq  tokens=1-3* delims=<>" %%l in ("%%f") do (	
		if %%m==TargetFrameworkVersion (
			if /I %%n GTR v4.5 	(
				set versionIsBad=true
				(Echo %%n )>>PublishLog.log
				(Echo %%f )>>PublishLog.log
			)
		)		
	)
)
if %versionIsBad%==true (
color 0c
 echo "publish�쳣������"
 echo "��⵽����Ŀ��dotNet�汾���ø���4.5�����޸�����Ϊ4.5�����µİ汾"
 echo "����鿴����Ŀ¼��PublishLog.log��"
color
 goto end
)

rem �������ģ��
call "buildRelease.bat" 1

xcopy /e /y "%currentPath%bin\Release" "%currentPath%%publishDir%\"



cd /d "%currentPath%..\Tools\Fill_Symbol\"

call "buildRelease.bat" 1

xcopy /e /y "%currentPath%..\Tools\Fill_Symbol\bin\Release" "%currentPath%%publishDir%\Tools\"



cd /d "%currentPath%..\Tools\BoardFilesLoader\"

call "%buildRelease.bat" 1

xcopy /e /y "%currentPath%..\Tools\BoardFilesLoader\bin\Release" "%currentPath%%publishDir%\Tools\"



cd /d "%currentPath%..\Tools\PosConfig\"

call "buildRelease.bat" 1
xcopy /e /y "%currentPath%..\Tools\PosConfig\bin\Release" "%currentPath%%publishDir%\Tools\"



cd /d "%currentPath%..\Tools\MemFileFormatTool\"

call "buildRelease.bat" 1
xcopy /e /y "%currentPath%..\Tools\MemFileFormatTool\bin\Release" "%currentPath%%publishDir%\Tools\"


rem ����innoPublish�ű����ѳ������ɰ�װ�ļ�
if not exist "%ProgramFiles(x86)%\Inno Setup 5\ISCC.exe" goto end

cd /d "%currentPath%"
rd /s/q Tmp 
xcopy /e /y "%currentPath%%publishDir%" "%currentPath%Tmp\"
"%ProgramFiles(x86)%\Inno Setup 5\ISCC.exe" InnoPublish.iss
rename "%currentPath%Pxs.exe" "%publishDir%.exe"
rd /s/q Tmp

:end
pause