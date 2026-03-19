-- Быстрая проверка роли admin
USE AutoWorkshopDB;
GO

SELECT Id, Login, Role FROM Users WHERE Login = 'admin';
GO

-- Если Role пустой или NULL - исправляем
UPDATE Users SET Role = 'Admin' WHERE Login = 'admin';
GO

-- Проверяем результат
SELECT Id, Login, Role FROM Users WHERE Login = 'admin';
GO
