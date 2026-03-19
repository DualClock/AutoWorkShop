/*
================================================================================
    AutoWorkshopDB - СКРИПТ ПРОВЕРКИ БАЗЫ ДАННЫХ
    Система управления автосервисом "АвтоМаст ИС"
    
    Выполните этот скрипт для проверки состояния БД
================================================================================
*/

USE AutoWorkshopDB;
GO

PRINT '================================================================================';
PRINT '    ПРОВЕРКА БАЗЫ ДАННЫХ AutoWorkshopDB';
PRINT '================================================================================';
PRINT '';

-- ============================================
-- 1. ПРОВЕРКА ТАБЛИЦ
-- ============================================
PRINT '1. ТАБЛИЦЫ В БАЗЕ ДАННЫХ:';
PRINT '-------------------------';
SELECT 
    TABLE_NAME AS [Таблица],
    TABLE_CATALOG AS [База данных]
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE' 
ORDER BY TABLE_NAME;
GO

-- ============================================
-- 2. КОЛИЧЕСТВО ЗАПИСЕЙ В ТАБЛИЦАХ
-- ============================================
PRINT '';
PRINT '2. КОЛИЧЕСТВО ЗАПИСЕЙ:';
PRINT '----------------------';
SELECT 
    'Departments' AS [Таблица], COUNT(*) AS [Записей] FROM Departments
UNION ALL SELECT 'Employees', COUNT(*) FROM Employees
UNION ALL SELECT 'Clients', COUNT(*) FROM Clients
UNION ALL SELECT 'Cars', COUNT(*) FROM Cars
UNION ALL SELECT 'Parts', COUNT(*) FROM Parts
UNION ALL SELECT 'Orders', COUNT(*) FROM Orders
UNION ALL SELECT 'OrderParts', COUNT(*) FROM OrderParts
UNION ALL SELECT 'Receipts', COUNT(*) FROM Receipts
UNION ALL SELECT 'Users', COUNT(*) FROM Users
UNION ALL SELECT 'Notifications', COUNT(*) FROM Notifications
UNION ALL SELECT 'Settings', COUNT(*) FROM Settings;
GO

-- ============================================
-- 3. ПРОСТРАНСТВО БАЗЫ ДАННЫХ
-- ============================================
PRINT '';
PRINT '3. РАЗМЕР БАЗЫ ДАННЫХ:';
PRINT '----------------------';
SELECT 
    DB_NAME() AS [База данных],
    SUM(size * 8 / 1024) AS [Размер (МБ)]
FROM sys.database_files
WHERE type_desc = 'ROWS';
GO

-- ============================================
-- 4. ПОЛЬЗОВАТЕЛИ СИСТЕМЫ
-- ============================================
PRINT '';
PRINT '4. ПОЛЬЗОВАТЕЛИ СИСТЕМЫ:';
PRINT '------------------------';
SELECT 
    Id,
    Login AS [Логин],
    Role AS [Роль],
    CASE WHEN IsActive = 1 THEN 'Да' ELSE 'Нет' END AS [Активен],
    LastLogin AS [Последний вход]
FROM Users
ORDER BY Id;
GO

-- ============================================
-- 5. ПОСЛЕДНИЕ ЗАКАЗЫ
-- ============================================
PRINT '';
PRINT '5. ПОСЛЕДНИЕ 10 ЗАКАЗОВ:';
PRINT '------------------------';
SELECT TOP 10
    O.Id AS [№],
    C.Brand + ' ' + C.Model AS [Автомобиль],
    C.PlateNumber AS [Госномер],
    O.Description AS [Описание],
    O.Status AS [Статус],
    O.TotalCost AS [Сумма],
    O.CreatedDate AS [Дата]
FROM Orders O
JOIN Cars C ON O.CarId = C.Id
ORDER BY O.CreatedDate DESC;
GO

-- ============================================
-- 6. СТАТУСЫ ЗАКАЗОВ
-- ============================================
PRINT '';
PRINT '6. СТАТИСТИКА ПО СТАТУСАМ ЗАКАЗОВ:';
PRINT '-----------------------------------';
SELECT 
    Status AS [Статус],
    COUNT(*) AS [Количество],
    SUM(TotalCost) AS [Общая сумма]
FROM Orders
GROUP BY Status;
GO

-- ============================================
-- 7. ЗАПЧАСТИ С НИЗКИМ ОСТАТКОМ
-- ============================================
PRINT '';
PRINT '7. ЗАПЧАСТИ С НИЗКИМ ОСТАТКОМ:';
PRINT '------------------------------';
SELECT 
    Name AS [Название],
    Article AS [Артикул],
    Quantity AS [Остаток],
    MinQuantity AS [Минимум]
FROM Parts
WHERE Quantity <= MinQuantity
ORDER BY Quantity;
GO

-- ============================================
-- 8. НАСТРОЙКИ СИСТЕМЫ
-- ============================================
PRINT '';
PRINT '8. НАСТРОЙКИ СИСТЕМЫ:';
PRINT '--------------------';
SELECT 
    KeyName AS [Параметр],
    Value AS [Значение]
FROM Settings
ORDER BY KeyName;
GO

-- ============================================
-- 9. НЕПРОЧИТАННЫЕ УВЕДОМЛЕНИЯ
-- ============================================
PRINT '';
PRINT '9. НЕПРОЧИТАННЫЕ УВЕДОМЛЕНИЯ:';
PRINT '-----------------------------';
SELECT 
    U.Login AS [Пользователь],
    N.Message AS [Сообщение],
    N.Type AS [Тип],
    N.CreatedDate AS [Дата]
FROM Notifications N
JOIN Users U ON N.UserId = U.Id
WHERE N.IsRead = 0
ORDER BY N.CreatedDate DESC;
GO

PRINT '';
PRINT '================================================================================';
PRINT '    ПРОВЕРКА ЗАВЕРШЕНА';
PRINT '================================================================================';
GO
