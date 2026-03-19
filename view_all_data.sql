-- ============================================
-- ШАГ 1: Сначала выполните этот скрипт для просмотра ВСЕХ данных
-- Скопируйте результат и отправьте разработчику
-- ИЛИ используйте ШАГ 2 для полной перезаписи
-- ============================================

USE AutoWorkshopDB;
GO

PRINT '=== ТЕКУЩИЕ ДАННЫЕ В ТАБЛИЦАХ ===';
PRINT '';

PRINT '--- Departments (Отделы) ---';
SELECT Id, Name AS 'Название' FROM Departments ORDER BY Id;

PRINT '';
PRINT '--- Employees (Сотрудники) ---';
SELECT Id, FullName AS 'ФИО', Position AS 'Должность', Phone AS 'Телефон', Email AS 'Email', DepartmentId AS 'Отдел' 
FROM Employees ORDER BY Id;

PRINT '';
PRINT '--- Clients (Клиенты) ---';
SELECT Id, FullName AS 'ФИО/Название', Phone AS 'Телефон', Email AS 'Email', Address AS 'Адрес', CreatedDate AS 'Дата создания' 
FROM Clients ORDER BY Id;

PRINT '';
PRINT '--- Cars (Автомобили) ---';
SELECT Id, Brand AS 'Марка', Model AS 'Модель', VIN, PlateNumber AS 'Госномер', Year AS 'Год', ClientId AS 'Владелец' 
FROM Cars ORDER BY Id;

PRINT '';
PRINT '--- Parts (Запчасти) ---';
SELECT Id, Name AS 'Название', Article AS 'Артикул', Quantity AS 'Кол-во', Price AS 'Цена', MinQuantity AS 'Мин.кол-во' 
FROM Parts ORDER BY Id;

PRINT '';
PRINT '--- Orders (Заказы) ---';
SELECT Id, CarId AS 'Авто', EmployeeId AS 'Сотрудник', Description AS 'Описание', Status AS 'Статус', TotalCost AS 'Сумма', CreatedDate AS 'Дата создания' 
FROM Orders ORDER BY Id;

PRINT '';
PRINT '--- Users (Пользователи) ---';
SELECT Id, Login, Role AS 'Роль', EmployeeId AS 'Сотрудник', IsActive AS 'Активен' 
FROM Users ORDER BY Id;

PRINT '';
PRINT '--- Notifications (Уведомления) ---';
SELECT Id, UserId AS 'Пользователь', Message AS 'Сообщение', IsRead AS 'Прочитано', CreatedDate AS 'Дата', Type AS 'Тип' 
FROM Notifications ORDER BY Id;

PRINT '';
PRINT '--- Settings (Настройки) ---';
SELECT Id, KeyName AS 'Ключ', Value AS 'Значение' 
FROM Settings ORDER BY Id;

GO
