/*
================================================================================
    Проверка установки базы данных
================================================================================
*/

USE AutoWorkshopDB;
GO

PRINT '=== ПРОВЕРКА БАЗЫ ДАННЫХ ===';
PRINT '';

-- Проверка таблиц
PRINT 'Таблицы:';
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME;
GO

-- Проверка количества записей
PRINT '';
PRINT 'Количество записей:';
SELECT 'Departments' as TableName, COUNT(*) as Count FROM Departments
UNION ALL
SELECT 'Employees', COUNT(*) FROM Employees
UNION ALL
SELECT 'Clients', COUNT(*) FROM Clients
UNION ALL
SELECT 'Cars', COUNT(*) FROM Cars
UNION ALL
SELECT 'Parts', COUNT(*) FROM Parts
UNION ALL
SELECT 'Orders', COUNT(*) FROM Orders
UNION ALL
SELECT 'Users', COUNT(*) FROM Users;
GO

-- Проверка пользователей
PRINT '';
PRINT 'Пользователи:';
SELECT Id, Login, Role, IsActive, LastLogin FROM Users;
GO

PRINT '';
PRINT '=== ПРОВЕРКА ЗАВЕРШЕНА ===';
GO
