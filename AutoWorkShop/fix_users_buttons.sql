-- Скрипт для проверки и исправления проблемы с кнопками
-- Выполните в SQL Server Management Studio

USE AutoWorkshopDB;
GO

-- 1. Проверяем структуру таблицы Users
PRINT '=== Структура таблицы Users ===';
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Users'
ORDER BY ORDINAL_POSITION;
GO

-- 2. Проверяем всех пользователей и их роли (без LastLogin пока что)
PRINT '';
PRINT '=== Все пользователи ===';
SELECT Id, Login, Role, IsActive FROM Users;
GO

-- 3. Добавляем поле LastLogin ТОЛЬКО если его нет
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'LastLogin')
BEGIN
    ALTER TABLE Users ADD LastLogin DATETIME2 NULL;
    PRINT '✓ Добавлено поле LastLogin';
END
ELSE
BEGIN
    PRINT '✓ Поле LastLogin уже существует';
END
GO

-- 4. Исправляем проблему: устанавливаем роль 'Admin' для пользователя admin
PRINT '';
PRINT '=== Исправление ролей ===';

-- Если у пользователя admin пустая роль или NULL - устанавливаем 'Admin'
UPDATE Users 
SET Role = 'Admin' 
WHERE Login = 'admin' AND (Role = '' OR Role IS NULL);

PRINT '✓ Роль Admin установлена для пользователя admin';
GO

-- 5. Финальная проверка с LastLogin
PRINT '';
PRINT '=== Итоговый результат ===';
SELECT Id, Login, Role, IsActive, LastLogin FROM Users;
GO

PRINT '';
PRINT '=== ИНСТРУКЦИЯ ===';
PRINT '1. Закройте приложение AutoWorkShop';
PRINT '2. Запустите приложение снова';
PRINT '3. Войдите под admin / admin123';
PRINT '4. Перейдите во вкладку "Пользователи"';
PRINT '5. Кнопки должны быть видны!';
GO
