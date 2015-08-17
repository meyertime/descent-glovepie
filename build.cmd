@echo off

"%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\msbuild" "%~dp0build\Build.proj" /m /nr:false %*