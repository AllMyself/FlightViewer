set isPublish=1

set currentPath=%~dp0



cd /d %currentPath%
call "Clean.bat" 1


rd /s /q "Publish"
 


cd /d "%currentPath%..\Tools\"

for /f "delims=" %%d in ('dir /ad/b') do (
cd /d "%currentPath%..\Tools\%%d\"
call "Clean.bat" 1
)


pause