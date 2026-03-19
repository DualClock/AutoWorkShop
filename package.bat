@echo off
chcp 65001 >nul
echo ================================================================================
echo     УПАКОВКА ПРОЕКТА AutoWorkShop
echo     Система управления автосервисом "АвтоМаст ИС"
echo ================================================================================
echo.

set VERSION=1.0
set DATE=2026-03-20
set PACKAGE_NAME=AutoWorkShop_v%VERSION%_%DATE%

echo [1/4] Создание папки для упаковки...
if exist "%PACKAGE_NAME%" (
    rmdir /s /q "%PACKAGE_NAME%"
)
mkdir "%PACKAGE_NAME%"

echo [2/4] Копирование файлов публикации...
xcopy /E /I /Y Publish "%PACKAGE_NAME%\Program"
mkdir "%PACKAGE_NAME%\Program\Deploy"
copy "Deploy\*.md" "%PACKAGE_NAME%\Program\Deploy\" >nul 2>&1

echo [3/4] Копирование документации...
copy "README.md" "%PACKAGE_NAME%\" >nul 2>&1
copy "CHECKLIST.md" "%PACKAGE_NAME%\" >nul 2>&1
copy "01_CreateDatabase_Full.sql" "%PACKAGE_NAME%\" >nul 2>&1
copy "02_DatabaseCheck.sql" "%PACKAGE_NAME%\" >nul 2>&1
copy "03_DatabaseReset.sql" "%PACKAGE_NAME%\" >nul 2>&1

echo [4/4] Создание итогового README...
(
echo ================================================================================
echo     AutoWorkShop v%VERSION% от %DATE%
echo     Система управления автосервисом "АвтоМаст ИС"
echo ================================================================================
echo.
echo СОСТАВ КОМПЛЕКТА:
echo -----------------
echo 1. Program\              - Папка с приложением для установки
echo 2. 01_CreateDatabase_Full.sql - Скрипт создания БД
echo 3. 02_DatabaseCheck.sql  - Скрипт проверки БД
echo 4. 03_DatabaseReset.sql  - Скрипт сброса БД
echo 5. README.md             - Описание проекта
echo 6. CHECKLIST.md          - Чек-лист проверки
echo.
echo ТРЕБОВАНИЯ К СИСТЕМЕ:
echo ---------------------
echo - Windows 10/11 ^(^64-bit^)
echo - .NET 10.0 Runtime
echo - SQL Server 2016+ или SQL Server Express
echo.
echo БЫСТРЫЙ СТАРТ:
echo --------------
echo 1. Установите SQL Server Express
echo 2. Выполните скрипт 01_CreateDatabase_Full.sql
echo 3. Скопируйте содержимое Program\ в C:\AutoWorkShop\
echo 4. Настройте appsettings.json при необходимости
echo 5. Запустите AutoWorkShop.exe
echo.
echo УЧЁТНЫЕ ДАННЫЕ ПО УМОЛЧАНИЮ:
echo ----------------------------
echo Логин      Пароль       Роль
echo admin      admin123     Администратор
echo master     master123    Мастер
echo mechanic   mechanic123  Механик
echo accountant accountant123 Бухгалтер
echo warehouse  warehouse123 Кладовщик
echo.
echo ⚠️ СМЕНите пароли после первого входа!
echo.
echo ПОДРОБНАЯ ИНСТРУКЦИЯ:
echo ---------------------
echo См. Program\README_ДЛЯ_ЗАКАЗЧИКА.txt
echo или Deploy\ИНСТРУКЦИЯ_ПО_УСТАНОВКЕ.md
echo.
echo ================================================================================
echo     © 2026 АвтоМаст ИС. Все права защищены.
echo ================================================================================
) > "%PACKAGE_NAME%\НАЧНИТЕ_ЗДЕСЬ.txt"

echo.
echo ================================================================================
echo     УПАКОВКА ЗАВЕРШЕНА!
echo ================================================================================
echo.
echo Создана папка: %PACKAGE_NAME%
echo.
echo Структура:
echo   %PACKAGE_NAME%\
echo   ├── Program\              ^(приложение для установки^)
echo   ├── 01_CreateDatabase_Full.sql
echo   ├── 02_DatabaseCheck.sql
echo   ├── 03_DatabaseReset.sql
echo   ├── README.md
echo   ├── CHECKLIST.md
echo   └── НАЧНИТЕ_ЗДЕСЬ.txt
echo.
echo Для создания архива:
echo   1. Щёлкните правой кнопкой на папке %PACKAGE_NAME%
echo   2. Выберите "Отправить" ^> "Сжатая ZIP-папка"
echo   3. Или используйте 7-Zip / WinRAR
echo.
echo ================================================================================
pause
