/*
================================================================================
    AutoWorkshopDB - Скрипт обновления базы данных
    Для существующих установок
    
    Выполните этот скрипт в SQL Server Management Studio
================================================================================
*/

USE AutoWorkshopDB;
GO

PRINT 'Начало обновления базы данных...';
GO

-- Добавляем поле LastLogin если не существует
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'LastLogin')
BEGIN
    ALTER TABLE Users ADD LastLogin DATETIME2 NULL;
    PRINT '✓ Добавлено поле LastLogin в таблицу Users';
END
GO

-- Обновляем пустые роли
UPDATE Users SET Role = 'Admin' WHERE Role = '' OR Role IS NULL;
PRINT '✓ Обновлены пустые роли';
GO

PRINT 'Обновление завершено!';
GO
