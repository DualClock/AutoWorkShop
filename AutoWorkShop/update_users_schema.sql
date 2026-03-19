-- Скрипт обновления базы данных для добавления поля LastLogin и настройки ролей
-- 1. Откройте SQL Server Management Studio
-- 2. Подключитесь к базе данных AutoWorkshopDB
-- 3. Выполните этот скрипт (F5)

USE AutoWorkshopDB;
GO

-- 1. Добавляем поле LastLogin в таблицу Users, если оно ещё не существует
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'LastLogin')
BEGIN
    ALTER TABLE Users ADD LastLogin DATETIME2 NULL;
    PRINT '✓ Добавлено поле LastLogin в таблицу Users';
END
ELSE
BEGIN
    PRINT '✓ Поле LastLogin уже существует';
END
GO

-- 2. Проверяем текущих пользователей
PRINT '';
PRINT '=== Текущие пользователи ===';
SELECT Id, Login, Role, IsActive, LastLogin FROM Users;
GO

-- 3. Обновляем пустые роли до 'Admin' (для существующих пользователей)
UPDATE Users SET Role = 'Admin' WHERE Role = '' OR Role IS NULL;
PRINT '✓ Обновлены пустые роли до Admin';
GO

-- 4. Показываем обновлённых пользователей
PRINT '';
PRINT '=== Обновлённые пользователи ===';
SELECT Id, Login, Role, IsActive, LastLogin FROM Users;
GO

PRINT '';
PRINT '=== Готово! ===';
PRINT 'Теперь закройте приложение и запустите снова.';
PRINT 'Войдите под admin и перейдите во вкладку "Пользователи".';
GO
