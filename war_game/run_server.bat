@echo off
if exist "%~dp0server" (
    cd /d "%~dp0server"
) else (
    cd /d "%~dp0..\server"
)
npm start