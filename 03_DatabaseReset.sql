/*
================================================================================
    AutoWorkshopDB - СКРИПТ СБРОСА БАЗЫ ДАННЫХ
    Система управления автосервисом "АвтоМаст ИС"
    
    ВНИМАНИЕ! Этот скрипт полностью удаляет все данные из таблиц!
    Используйте перед повторным заполнением или удалением БД
================================================================================
*/

USE AutoWorkshopDB;
GO

PRINT '================================================================================';
PRINT '    СБРОС БАЗЫ ДАННЫХ';
PRINT '================================================================================';
PRINT '';
PRINT 'ВНИМАНИЕ! Все данные будут УДАЛЕНЫ!';
PRINT '';

-- ============================================
-- ОТКЛЮЧЕНИЕ ПРОВЕРКИ ВНЕШНИХ КЛЮЧЕЙ
-- ============================================
EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT ALL";
GO

-- ============================================
-- ОЧИСТКА ВСЕХ ТАБЛИЦ
-- ============================================
PRINT 'Очистка таблиц...';

DELETE FROM Notifications;
DELETE FROM Receipts;
DELETE FROM OrderParts;
DELETE FROM Orders;
DELETE FROM Users;
DELETE FROM Cars;
DELETE FROM Parts;
DELETE FROM Employees;
DELETE FROM Clients;
DELETE FROM Departments;
DELETE FROM Settings;
GO

PRINT 'Все таблицы очищены';
GO

-- ============================================
-- СБРОС СЧЁТЧИКОВ IDENTITY
-- ============================================
PRINT 'Сброс счётчиков...';

DBCC CHECKIDENT ('Departments', RESEED, 0);
DBCC CHECKIDENT ('Employees', RESEED, 0);
DBCC CHECKIDENT ('Clients', RESEED, 0);
DBCC CHECKIDENT ('Cars', RESEED, 0);
DBCC CHECKIDENT ('Parts', RESEED, 0);
DBCC CHECKIDENT ('Orders', RESEED, 0);
DBCC CHECKIDENT ('OrderParts', RESEED, 0);
DBCC CHECKIDENT ('Receipts', RESEED, 0);
DBCC CHECKIDENT ('Users', RESEED, 0);
DBCC CHECKIDENT ('Notifications', RESEED, 0);
DBCC CHECKIDENT ('Settings', RESEED, 0);
GO

-- ============================================
-- ВКЛЮЧЕНИЕ ПРОВЕРКИ ВНЕШНИХ КЛЮЧЕЙ
-- ============================================
EXEC sp_MSforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL";
GO

PRINT '';
PRINT '================================================================================';
PRINT '    БАЗА ДАННЫХ ПОЛНОСТЬЮ СБРОШЕНА';
PRINT '================================================================================';
PRINT '';
PRINT 'Теперь вы можете выполнить скрипт 01_CreateDatabase_Full.sql';
PRINT 'для создания структуры и заполнения данными';
PRINT '================================================================================';
GO
