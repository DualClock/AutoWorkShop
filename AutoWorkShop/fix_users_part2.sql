-- ЧАСТЬ 2: Исправляем роли
-- Выполните этот скрипт ВТОРЫМ (после part1)

USE AutoWorkshopDB;
GO

-- Показываем текущих пользователей
SELECT Id, Login, Role, IsActive FROM Users;
GO

-- Исправляем роль admin
UPDATE Users 
SET Role = 'Admin' 
WHERE Login = 'admin' AND (Role = '' OR Role IS NULL);

PRINT 'Роль Admin установлена для пользователя admin';
GO

-- Показываем результат
SELECT Id, Login, Role, IsActive, LastLogin FROM Users;
GO
