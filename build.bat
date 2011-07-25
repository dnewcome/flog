:: assuming 4.0 compiler is available
set pointversion=1.0.0

:: set msbuild=C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe
set msbuild=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe

set projfile=flog.csproj

:: note that we MUST specify /t:rebuild otherwise just changing the target framework isn't enough to 
:: force uptodate logic to rebuild the project. We'll end up with 2 identical builds of whatever is first
%msbuild% %projfile% /p:Configuration=Debug /p:TargetFrameworkVersion=v4.0 /p:PointVersion=%pointversion% /t:rebuild
%msbuild% %projfile% /p:Configuration=Release /p:TargetFrameworkVersion=v4.0 /p:PointVersion=%pointversion% /t:rebuild
 
%msbuild% %projfile% /p:Configuration=Debug /p:TargetFrameworkVersion=v2.0 /p:PointVersion=%pointversion% /t:rebuild
%msbuild% %projfile% /p:Configuration=Release /p:TargetFrameworkVersion=v2.0 /p:PointVersion=%pointversion% /t:rebuild
