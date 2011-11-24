set msbuild=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe
set PointVersion=1.0.1

set outpathprop=/property:OutputPath=release\%PointVersion%\2.0
%msbuild% /property:Configuration=Debug /property:TargetFrameworkVersion=v3.5 %outpathprop%\Debug flog.csproj
%msbuild% /property:Configuration=Release /property:TargetFrameworkVersion=v3.5 %outpathprop%\Release flog.csproj

set outpathprop=/property:OutputPath=release\%PointVersion%\4.0
%msbuild% /property:Configuration=Debug /property:TargetFrameworkVersion=v4.0 %outpathprop%\Debug flog.csproj
%msbuild% /property:Configuration=Release /property:TargetFrameworkVersion=v4.0 %outpathprop%\Release flog.csproj
