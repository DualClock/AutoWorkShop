@echo off
chcp 65001 >nul
echo ================================================================================
echo     СКРИПТ ПУБЛИКАЦИИ AutoWorkShop
echo     Система управления автосервисом "АвтоМаст ИС"
echo ================================================================================
echo.

echo [1/3] Очистка папки публикации...
if exist "Publish" (
    rmdir /s /q Publish
)
mkdir Publish
echo.

echo [2/3] Публикация проекта...
dotnet publish --configuration Release -r win-x64 --self-contained false -o Publish

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo ================================================================================
    echo     ОШИБКА ПУБЛИКАЦИИ!
    echo ================================================================================
    exit /b 1
)
echo.

echo [3/3] Копирование дополнительных файлов...
copy "README.md" "Publish\" >nul 2>&1
copy "Deploy\*" "Publish\Deploy\" >nul 2>&1
if not exist "Publish\Deploy" mkdir "Publish\Deploy"
copy "Deploy\*.sql" "Publish\Deploy\" >nul 2>&1
copy "01_CreateDatabase_Full.sql" "Publish\" >nul 2>&1
copy "02_DatabaseCheck.sql" "Publish\" >nul 2>&1
copy "03_DatabaseReset.sql" "Publish\" >nul 2>&1

echo.
echo ================================================================================
echo     ПУБЛИКАЦИЯ ЗАВЕРШЕНА УСПЕШНО!
echo ================================================================================
echo.
echo Файлы опубликованы в папку: Publish
echo.
echo Состав дистрибутива:
echo   - AutoWorkShop.exe (исполняемый файл)
echo   - appsettings.json (файл конфигурации)
echo   - DLL файлы (.NET и зависимости)
echo   - 01_CreateDatabase_Full.sql (скрипт создания БД)
echo   - 02_DatabaseCheck.sql (скрипт проверки БД)
echo   - 03_DatabaseReset.sql (скрипт сброса БД)
echo   - Deploy\ИНСТРУКЦИЯ_ПО_УСТАНОВКЕ.md
echo   - README.md
echo.
echo СЛЕДУЮЩИЕ ШАГИ:
echo   1. Установите SQL Server Express
echo   2. Выполните скрипт 01_CreateDatabase_Full.sql
echo   3. Настройте appsettings.json при необходимости
echo   4. Запустите AutoWorkShop.exe
echo.
echo ================================================================================
pause
