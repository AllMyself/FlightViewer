set isPublish=1

set currentPath=%~dp0


rem 获取当前日期和时间
for /f "tokens=2 delims==" %%a in ('wmic path win32_operatingsystem get LocalDateTime /value') do (
  set t=%%a
)
set Now=%t:~0,4%-%t:~4,2%-%t:~6,2%-%t:~8,2%-%t:~10,2%


rem 设置publish文件夹的名字

set publishDir=BhArinc %Now%
cd /d %currentPath%

rem 检测项目所依赖的dotnet版本，不能大于4.5
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
 echo "publish异常结束！"
 echo "检测到有项目的dotNet版本设置高于4.5，请修改它们为4.5及以下的版本"
 echo "详情查看，本目录下PublishLog.log！"
color
 goto end
)

rem 编译各个模块
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


rem 调用innoPublish脚本，把程序打包成安装文件
if not exist "%ProgramFiles(x86)%\Inno Setup 5\ISCC.exe" goto end

cd /d "%currentPath%"
rd /s/q Tmp 
xcopy /e /y "%currentPath%%publishDir%" "%currentPath%Tmp\"
"%ProgramFiles(x86)%\Inno Setup 5\ISCC.exe" InnoPublish.iss
rename "%currentPath%Pxs.exe" "%publishDir%.exe"
rd /s/q Tmp

:end
pause