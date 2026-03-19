-- ДИАГНОСТИКА: Проверка текущего состояния
-- Выполните и покажите результат

USE AutoWorkshopDB;
GO

-- 1. Какая структура таблицы Users?
PRINT '1. Структура таблицы Users:';
SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users';
GO

-- 2. Какие пользователи есть и их роли?
PRINT '2. Пользователи:';
SELECT Id, Login, Role, IsActive FROM Users;
GO

-- 3. Есть ли поле LastLogin?
PRINT '3. Проверка поля LastLogin:';
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'LastLogin')
    PRINT 'LastLogin: СУЩЕСТВУЕТ';
ELSE
    PRINT 'LastLogin: НЕ СУЩЕСТВУЕТ';
GO
