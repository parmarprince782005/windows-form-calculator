@echo off
set "CSC=%windir%\Microsoft.NET\Framework\v4.0.30319\csc.exe"

if exist "%CSC%" (
    echo Compiling...
    "%CSC%" /target:winexe /debug+ /out:Calculator.exe Form1.cs
    if %errorlevel% neq 0 (
        echo Compilation failed.
        exit /b 1
    ) else (
        echo Compilation succeeded!
    )
) else (
    echo csc.exe not found at %CSC%.
    exit /b 1
)
