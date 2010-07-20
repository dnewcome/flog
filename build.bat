mkdir debug
csc /out:debug/Flog.dll /debug:full /target:library flog.cs

mkdir release
csc /out:release/Flog.dll /target:library flog.cs
