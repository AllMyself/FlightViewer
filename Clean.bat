cd /d %~dp0 


set msbuild="C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSbuild.exe"

for %%f in (*.sln) do %msbuild% "%%f" /t:Clean



for /f "delims=" %%f in ('dir *.suo /ah/b/s') do del /f /q /ah "%%f"

for /f "delims=" %%f in ('dir *.suo /a-h/b/s') do del /f /q /a-h "%%f"



for /d /r %%d in (*obj) do (

if exist "%%d"  rd /s /q "%%d"

)


for /d /r %%d in (*bin) do (

if exist "%%d" rd /s /q "%%d"

)


if exist "TestResults"  rd /s /q "TestResults"


if exist ".vs"  rd /s /q ".vs"


if %ERRORLEVEL% neq 0 goto pause

if [%1]==[1] goto end



:pause

echo %ERRORLEVEL%

pause

:end




