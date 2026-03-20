/*
================================================================================
    AutoWorkshopDB - ИСПРАВЛЕНИЕ ОШИБКИ PasswordHash
    Система управления автосервисом "АвтоМаст ИС"
    
    Этот скрипт исправляет ошибку с именем поля PasswordHash -> Password
================================================================================
*/

USE AutoWorkshopDB;
GO

PRINT '================================================================================';
PRINT '    ИСПРАВЛЕНИЕ ОШИБКИ PASSWORDHASH';
PRINT '================================================================================';
PRINT '';

-- ============================================
-- ПРОВЕРКА СУЩЕСТВОВАНИЯ ПОЛЯ PasswordHash
-- ============================================
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'PasswordHash')
BEGIN
    PRINT 'Обнаружено старое поле PasswordHash...';
    
    -- Проверяем, есть ли уже поле Password
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'Password')
    BEGIN
        PRINT 'Добавление поля Password...';
        ALTER TABLE Users ADD Password NVARCHAR(255) NOT NULL DEFAULT '';
    END
    
    -- Копируем данные из PasswordHash в Password
    PRINT 'Копирование данных из PasswordHash в Password...';
    UPDATE Users SET Password = PasswordHash WHERE Password = '';
    
    -- Удаляем старое поле PasswordHash
    PRINT 'Удаление старого поля PasswordHash...';
    ALTER TABLE Users DROP COLUMN PasswordHash;
    
    PRINT '';
    PRINT '✓ Поле PasswordHash успешно заменено на Password';
END
ELSE
BEGIN
    PRINT '✓ Поле PasswordHash не обнаружено - база данных уже обновлена';
    
    -- Проверяем, есть ли поле Password
    IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'Password')
    BEGIN
        PRINT '✓ Поле Password существует';
    END
    ELSE
    BEGIN
        PRINT '⚠ ВНИМАНИЕ: Поле Password отсутствует!';
        PRINT 'Добавление поля Password...';
        ALTER TABLE Users ADD Password NVARCHAR(255) NOT NULL DEFAULT '';
        PRINT '✓ Поле Password добавлено';
    END
END
GO

PRINT '';
PRINT '================================================================================';
PRINT '    ИСПРАВЛЕНИЕ ЗАВЕРШЕНО';
PRINT '================================================================================';
PRINT '';
PRINT 'Теперь вы можете войти в приложение AutoWorkShop';
PRINT '================================================================================';
GO
