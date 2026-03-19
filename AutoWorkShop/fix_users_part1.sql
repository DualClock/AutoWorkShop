-- ЧАСТЬ 1: Добавляем поле LastLogin
-- Выполните этот скрипт ПЕРВЫМ

USE AutoWorkshopDB;
GO

-- Добавляем поле LastLogin только если его нет
IF NOT EXISTS (
    SELECT * FROM sys.columns 
    WHERE object_id = OBJECT_ID('Users') 
    AND name = 'LastLogin'
)
BEGIN
    ALTER TABLE Users ADD LastLogin DATETIME2 NULL;
    PRINT 'Добавлено поле LastLogin';
END
ELSE
BEGIN
    PRINT 'Поле LastLogin уже существует';
END
GO
