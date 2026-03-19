-- Скрипт для исправления кодировки в базе данных AutoWorkshopDB
-- Выполните этот скрипт в SQL Server Management Studio

USE AutoWorkshopDB;
GO

-- Проверка текущей кодировки базы данных
SELECT name, collation_name 
FROM sys.databases 
WHERE name = 'AutoWorkshopDB';
GO

-- Если collation не Cyrillic_General_CI_AS, измените его
-- ALTER DATABASE AutoWorkshopDB COLLATE Cyrillic_General_CI_AS;
-- GO

-- Проверка данных в таблице Parts
SELECT Id, Name, Article, Quantity, Price 
FROM Parts;
GO

-- Если данные отображаются как ??????, их нужно обновить
-- Пример обновления (замените значения на правильные):
/*
UPDATE Parts 
SET Name = N'Моторное масло 5W-40'
WHERE Id = 1;
*/

-- Для массового исправления нужно:
-- 1. Удалить существующие данные: DELETE FROM Parts;
-- 2. Перезапустить приложение и добавить данные заново
-- ИЛИ
-- 3. Обновить данные вручную через UPDATE

GO
