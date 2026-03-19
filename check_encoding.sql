-- Проверка кодировки базы данных
USE AutoWorkshopDB;
GO

-- Проверка collation базы данных
SELECT 
    DB_NAME() AS DatabaseName,
    DATABASEPROPERTYEX(DB_NAME(), 'Collation') AS DatabaseCollation;
GO

-- Проверка collation таблиц
SELECT 
    TABLE_NAME,
    TABLE_CATALOG,
    TABLE_SCHEMA
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
GO

-- Проверка collation столбцов с текстом
SELECT 
    TABLE_NAME,
    COLUMN_NAME,
    DATA_TYPE,
    COLLATION_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE DATA_TYPE IN ('nvarchar', 'varchar', 'text', 'ntext')
ORDER BY TABLE_NAME, COLUMN_NAME;
GO

-- Проверка данных в таблице Parts
SELECT TOP 5 Id, Name, Article FROM Parts;
GO

-- Проверка данных в таблице Employees
SELECT TOP 5 Id, FullName, Position FROM Employees;
GO
