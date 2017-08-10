@Setlocal Enabledelayedexpansion
set currentPath=%~dp0
set buildType=Release

set msbuild="C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSbuild.exe"


for %%f in (*.sln) do %msbuild% "%%f" /t:Rebuild /p:Configuration=%buildType%


rem 获取当前日期和时间
for /f "tokens=2 delims==" %%a in ('wmic path win32_operatingsystem get LocalDateTime /value') do (
  set t=%%a
)
set Now=%t:~0,4%-%t:~4,2%-%t:~6,2%-%t:~8,2%-%t:~10,2%

rem 把当前日期和时间写如Version.cfg文件中。
if %buildType% == Debug (
set versionFile="%currentPath%bin\Debug\Config\Version.cfg"

) else (
set versionFile="%currentPath%bin\Release\Config\Version.cfg"
)

set TempVerFile=Version.cfg
if exist %TempVerFile% del /q /f %TempVerFile%

For /f "usebackq tokens=*" %%i in (%versionFile%) do (
	For /f "tokens=1* delims==" %%j in ("%%i") do (
		If "%%j"=="<BuildTime Time" (
				Set rowStr=  %%j="%Now%" /^>
			) Else (
				Set rowStr=%%i
			)
		(Echo !rowStr!)>>%TempVerFile%
	) 
)

copy /y  %TempVerFile% %versionFile%
del /q /f %TempVerFile%


@echo ********************************************************

@echo 删除无用的文件

@echo ********************************************************

@echo off 


cd /d bin\Release

del /q /s *.pdb

del /q /s *.dll.config


if ERRORLEVEL 1 goto pause

if [%1]==[1] goto end



:pause

echo %ERRORLEVEL%

pause

:end