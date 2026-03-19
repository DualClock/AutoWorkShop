-- ============================================
-- ПОЛНАЯ ПЕРЕЗАПИСЬ БАЗЫ ДАННЫХ
-- с правильными русскими символами
-- ============================================
-- ВНИМАНИЕ! Все текущие данные будут УДАЛЕНЫ
-- ============================================

USE AutoWorkshopDB;
GO

-- ============================================
-- ОТКЛЮЧЕНИЕ ПРОВЕРКИ ВНЕШНИХ КЛЮЧЕЙ
-- ============================================
EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT ALL";
GO

-- ============================================
-- 1. ОЧИСТКА ВСЕХ ТАБЛИЦ
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

-- ============================================
-- 2. ЗАПОЛНЕНИЕ Departments (Отделы)
-- ============================================
PRINT 'Заполнение Departments...';

SET IDENTITY_INSERT Departments ON;
INSERT INTO Departments (Id, Name) VALUES
(1, N'Администрация'),
(2, N'Сервисный отдел'),
(3, N'Отдел запчастей'),
(4, N'Бухгалтерия'),
(5, N'Склад');
SET IDENTITY_INSERT Departments OFF;
GO

-- ============================================
-- 3. ЗАПОЛНЕНИЕ Employees (Сотрудники)
-- ============================================
PRINT 'Заполнение Employees...';

SET IDENTITY_INSERT Employees ON;
INSERT INTO Employees (Id, FullName, Position, Phone, Email, DepartmentId) VALUES
(1, N'Иванов Иван Иванович', N'Директор', N'+7 (999) 001-01-01', N'ivanov@autoworkshop.ru', 1),
(2, N'Петров Пётр Петрович', N'Мастер-приёмщик', N'+7 (999) 002-02-02', N'petrov@autoworkshop.ru', 2),
(3, N'Сидоров Сидор Сидорович', N'Автослесарь', N'+7 (999) 003-03-03', N'sidorov@autoworkshop.ru', 2),
(4, N'Кузнецов Алексей Михайлович', N'Автослесарь', N'+7 (999) 004-04-04', N'kuznetsov@autoworkshop.ru', 2),
(5, N'Попов Дмитрий Александрович', N'Электрик', N'+7 (999) 005-05-05', N'popov@autoworkshop.ru', 2),
(6, N'Васильева Анна Сергеевна', N'Менеджер по запчастям', N'+7 (999) 006-06-06', N'vasilyeva@autoworkshop.ru', 3),
(7, N'Соколов Николай Владимирович', N'Кладовщик', N'+7 (999) 007-07-07', N'sokolov@autoworkshop.ru', 5),
(8, N'Михайлова Елена Игоревна', N'Бухгалтер', N'+7 (999) 008-08-08', N'mikhailova@autoworkshop.ru', 4),
(9, N'Козлов Андрей Викторович', N'Автослесарь', N'+7 (999) 009-09-09', N'kozlov@autoworkshop.ru', 2),
(10, N'Новикова Мария Павловна', N'Администратор', N'+7 (999) 010-10-10', N'novikova@autoworkshop.ru', 1);
SET IDENTITY_INSERT Employees OFF;
GO

-- ============================================
-- 4. ЗАПОЛНЕНИЕ Clients (Клиенты)
-- ============================================
PRINT 'Заполнение Clients...';

SET IDENTITY_INSERT Clients ON;
INSERT INTO Clients (Id, FullName, Phone, Email, Address, CreatedDate) VALUES
(1, N'ООО "АвтоТранс"', N'+7 (495) 101-01-01', N'info@avtotrans.ru', N'г. Москва, ул. Ленина, д. 1', '2025-01-15'),
(2, N'ИП Смирнов А.В.', N'+7 (999) 102-02-02', N'smirnov@mail.ru', N'г. Москва, ул. Мира, д. 10', '2025-02-20'),
(3, N'Козлов Виктор Петрович', N'+7 (999) 103-03-03', N'kozlov@yandex.ru', N'г. Химки, ул. Гагарина, д. 5', '2025-03-10'),
(4, N'Новикова Ольга Ивановна', N'+7 (999) 104-04-04', N'novikova@gmail.com', N'г. Мытищи, пр. Академика, д. 20', '2025-04-05'),
(5, N'АО "ГрузПеревозки"', N'+7 (495) 105-05-05', N'info@gruzper.ru', N'г. Москва, МКАД, д. 50', '2025-05-12'),
(6, N'Федоров Сергей Николаевич', N'+7 (999) 106-06-06', N'fedorov@mail.ru', N'г. Королёв, ул. Калинина, д. 15', '2025-06-18'),
(7, N'Павлова Татьяна Андреевна', N'+7 (999) 107-07-07', N'pavlova@yandex.ru', N'г. Балашиха, ш. Энтузиастов, д. 30', '2025-07-22'),
(8, N'ООО "Такси-Плюс"', N'+7 (495) 108-08-08', N'taxi-plus@yandex.ru', N'г. Москва, ул. Таксистов, д. 7', '2025-08-30'),
(9, N'ИП Морозов И.К.', N'+7 (999) 109-09-09', N'morozov@mail.ru', N'г. Подольск, ул. Ленина, д. 45', '2025-09-14'),
(10, N'ЗАО "АвтоЛайн"', N'+7 (495) 110-10-11', N'info@autoline.ru', N'г. Москва, Варшавское ш., д. 100', '2025-10-01');
SET IDENTITY_INSERT Clients OFF;
GO

-- ============================================
-- 5. ЗАПОЛНЕНИЕ Cars (Автомобили)
-- ============================================
PRINT 'Заполнение Cars...';

SET IDENTITY_INSERT Cars ON;
INSERT INTO Cars (Id, ClientId, Brand, Model, VIN, PlateNumber, Year) VALUES
(1, 1, N'ГАЗ', N'Газель', N'XTT123456789012345', N'А001АА799', 2020),
(2, 2, N'ВАЗ', N'2114', N'XTA211400012345678', N'В002ВВ777', 2015),
(3, 3, N'УАЗ', N'Патриот', N'XTT345678901234567', N'У003УУ199', 2021),
(4, 4, N'КАМАЗ', N'65115', N'XTM651150K1234567', N'К004КК750', 2019),
(5, 5, N'Ford', N'Transit', N'WF0LXXTTGL1234567', N'Ф005ФФ777', 2018),
(6, 6, N'ВАЗ', N'Гранта', N'XTA21900001234567', N'Г006ГГ199', 2022),
(7, 7, N'Hyundai', N'Solaris', N'XTEFC123456789012', N'Х007ХХ777', 2020),
(8, 8, N'Volkswagen', N'Transporter', N'WV1ZZZ7HZKE123456', N'Т008ТТ750', 2017),
(9, 9, N'ГАЗ', N'Соболь', N'XTT987654321098765', N'С009СС799', 2021),
(10, 10, N'ВАЗ', N'Веста', N'XTA21800009876543', N'В010ВВ777', 2023);
SET IDENTITY_INSERT Cars OFF;
GO

-- ============================================
-- 6. ЗАПОЛНЕНИЕ Parts (Запчасти)
-- ============================================
PRINT 'Заполнение Parts...';

SET IDENTITY_INSERT Parts ON;
INSERT INTO Parts (Id, Name, Article, Quantity, Price, MinQuantity) VALUES
(1, N'Моторное масло 5W-40', N'OIL-5W40', 50, 5000.00, 10),
(2, N'Фильтр масляный', N'FLT-OIL-01', 100, 800.00, 20),
(3, N'Фильтр воздушный', N'FLT-AIR-01', 80, 1200.00, 15),
(4, N'Фильтр салонный', N'FLT-CAB-01', 60, 1500.00, 10),
(5, N'Колодки тормозные передние', N'BRK-FRT-01', 40, 3500.00, 8),
(6, N'Колодки тормозные задние', N'BRK-RR-01', 40, 2500.00, 8),
(7, N'Свеча зажигания', N'SPK-01', 200, 600.00, 50),
(8, N'Аккумулятор 60Ач', N'BAT-60', 20, 7000.00, 5),
(9, N'Лампа H7', N'LMP-H7', 150, 300.00, 30),
(10, N'Лампа H4', N'LMP-H4', 150, 350.00, 30),
(11, N'Щетка стеклоочистителя', N'WPR-01', 100, 900.00, 20),
(12, N'Антифриз G12', N'ANT-G12', 60, 1800.00, 10),
(13, N'Тормозной диск', N'BRK-FL', 50, 4500.00, 10),
(14, N'Ремень ГРМ', N'BLT-TMG', 30, 4500.00, 5),
(15, N'Ролик натяжной', N'RLR-TNS', 30, 2000.00, 5),
(16, N'Насос водяной', N'PMP-WTR', 20, 5500.00, 5),
(17, N'Термостат', N'THRM-01', 25, 2200.00, 5),
(18, N'Датчик кислорода', N'SNS-O2', 15, 4000.00, 3),
(19, N'Датчик коленвала', N'SNS-CRK', 15, 2500.00, 3),
(20, N'Форсунка топливная', N'INJ-01', 10, 6000.00, 2),
(21, N'Катушка зажигания', N'COIL-01', 30, 3000.00, 5),
(22, N'Прокладка ГБЦ', N'GKT-HEAD', 10, 3500.00, 3),
(23, N'Сальник коленвала', N'SEL-CRK', 20, 1500.00, 5),
(24, N'Подшипник ступичный', N'BRG-HUB', 20, 4000.00, 5),
(25, N'Амортизатор передний', N'SHK-FRT', 15, 6500.00, 3),
(26, N'Амортизатор задний', N'SHK-RR', 15, 5500.00, 3),
(27, N'Рычаг подвески', N'ARM-01', 10, 4500.00, 3),
(28, N'Стабилизатор', N'SBL-01', 50, 800.00, 10),
(29, N'Шаровая опора', N'BALL-01', 40, 1200.00, 8),
(30, N'Рулевая тяга', N'TIE-ROD', 40, 1500.00, 8);
SET IDENTITY_INSERT Parts OFF;
GO

-- ============================================
-- 7. ЗАПОЛНЕНИЕ Orders (Заказы)
-- ============================================
PRINT 'Заполнение Orders...';

SET IDENTITY_INSERT Orders ON;
INSERT INTO Orders (Id, CarId, EmployeeId, Description, Status, TotalCost, CreatedDate, CompletedDate) VALUES
(1, 1, 2, N'Замена масла и фильтров', N'Выполнен', 8500.00, '2026-01-10 10:00:00', '2026-01-10 14:00:00'),
(2, 2, 3, N'Диагностика ходовой части', N'В работе', 2000.00, '2026-01-12 09:00:00', NULL),
(3, 3, 3, N'Замена тормозных колодок', N'Выполнен', 12000.00, '2026-01-15 11:00:00', '2026-01-15 16:00:00'),
(4, 4, 4, N'Ремонт двигателя', N'В работе', 45000.00, '2026-01-18 08:00:00', NULL),
(5, 5, 5, N'Замена аккумулятора', N'Выполнен', 9000.00, '2026-01-20 12:00:00', '2026-01-20 13:00:00'),
(6, 6, 2, N'Компьютерная диагностика', N'Ожидает', 3000.00, '2026-01-22 14:00:00', NULL),
(7, 7, 3, N'Замена ремня ГРМ', N'В работе', 18000.00, '2026-01-25 09:00:00', NULL),
(8, 8, 4, N'Регулировка развал-схождения', N'Выполнен', 4500.00, '2026-01-28 10:00:00', '2026-01-28 12:00:00'),
(9, 9, 3, N'Замена тормозных дисков', N'Выполнен', 15000.00, '2026-02-01 11:00:00', '2026-02-01 17:00:00'),
(10, 10, 5, N'Замена свечей зажигания', N'Ожидает', 5000.00, '2026-02-05 13:00:00', NULL);
SET IDENTITY_INSERT Orders OFF;
GO

-- ============================================
-- 8. ЗАПОЛНЕНИЕ Users (Пользователи)
-- ============================================
PRINT 'Заполнение Users...';

SET IDENTITY_INSERT Users ON;
INSERT INTO Users (Id, Login, PasswordHash, Role, EmployeeId, IsActive) VALUES
(1, N'admin', N'$2a$11$7EjXqH5K5K5K5K5K5K5K5.5K5K5K5K5K5K5K5K5K5K5K5K5K5', N'Администратор', 1, 1),
(2, N'master', N'$2a$11$7EjXqH5K5K5K5K5K5K5K5.5K5K5K5K5K5K5K5K5K5K5K5K5K5', N'Мастер', 2, 1),
(3, N'mechanic', N'$2a$11$7EjXqH5K5K5K5K5K5K5K5.5K5K5K5K5K5K5K5K5K5K5K5K5K5', N'Механик', 3, 1),
(4, N'accountant', N'$2a$11$7EjXqH5K5K5K5K5K5K5K5.5K5K5K5K5K5K5K5K5K5K5K5K5K5', N'Бухгалтер', 8, 1),
(5, N'warehouse', N'$2a$11$7EjXqH5K5K5K5K5K5K5K5.5K5K5K5K5K5K5K5K5K5K5K5K5K5', N'Кладовщик', 7, 1);
SET IDENTITY_INSERT Users OFF;
GO

-- ============================================
-- 9. ЗАПОЛНЕНИЕ Notifications (Уведомления)
-- ============================================
PRINT 'Заполнение Notifications...';

SET IDENTITY_INSERT Notifications ON;
INSERT INTO Notifications (Id, UserId, Message, IsRead, CreatedDate, Type) VALUES
(1, 1, N'Заказ №1 выполнен. Ожидает оплаты.', 0, '2026-01-10 14:00:00', N'Заказ'),
(2, 2, N'Новый заказ принят в работу.', 0, '2026-01-12 09:00:00', N'Заказ'),
(3, 6, N'Требуется пополнение склада запчастей.', 1, '2026-01-15 10:00:00', N'Склад'),
(4, 1, N'Истекает срок ТО для автомобиля клиента.', 0, '2026-01-18 08:00:00', N'Напоминание'),
(5, 5, N'Обновление прайс-листа запчастей.', 1, '2026-01-20 12:00:00', N'Системное'),
(6, 2, N'Клиент ждёт завершения ремонта.', 0, '2026-01-22 15:00:00', N'Заказ'),
(7, 3, N'Необходимо завершить диагностику.', 0, '2026-01-25 11:00:00', N'Задача'),
(8, 1, N'Отчёт за месяц готов к проверке.', 0, '2026-02-01 09:00:00', N'Отчёт');
SET IDENTITY_INSERT Notifications OFF;
GO

-- ============================================
-- 10. ЗАПОЛНЕНИЕ Settings (Настройки)
-- ============================================
PRINT 'Заполнение Settings...';

SET IDENTITY_INSERT Settings ON;
INSERT INTO Settings (Id, KeyName, Value) VALUES
(1, N'CompanyName', N'АвтоМастер'),
(2, N'Address', N'г. Москва, ул. Авторемонтная, д. 1'),
(3, N'Phone', N'+7 (495) 123-45-67'),
(4, N'Email', N'info@avtomaster.ru'),
(5, N'WorkingHours', N'Пн-Пт: 9:00-18:00, Сб: 10:00-15:00');
SET IDENTITY_INSERT Settings OFF;
GO

-- ============================================
-- ВКЛЮЧЕНИЕ ПРОВЕРКИ ВНЕШНИХ КЛЮЧЕЙ
-- ============================================
EXEC sp_MSforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL";
GO

-- ============================================
-- СБРОС СЧЁТЧИКОВ IDENTITY
-- ============================================
PRINT 'Сброс счётчиков...';

DBCC CHECKIDENT ('Departments', RESEED, 5);
DBCC CHECKIDENT ('Employees', RESEED, 10);
DBCC CHECKIDENT ('Clients', RESEED, 10);
DBCC CHECKIDENT ('Cars', RESEED, 10);
DBCC CHECKIDENT ('Parts', RESEED, 30);
DBCC CHECKIDENT ('Orders', RESEED, 10);
DBCC CHECKIDENT ('Users', RESEED, 5);
DBCC CHECKIDENT ('Notifications', RESEED, 8);
DBCC CHECKIDENT ('Settings', RESEED, 5);
GO

-- ============================================
-- ПРОВЕРКА РЕЗУЛЬТАТОВ
-- ============================================
PRINT '';
PRINT '========================================';
PRINT 'ПРОВЕРКА РЕЗУЛЬТАТОВ';
PRINT '========================================';

PRINT '';
PRINT '--- Departments ---';
SELECT Id, Name FROM Departments;

PRINT '';
PRINT '--- Employees ---';
SELECT Id, FullName, Position FROM Employees;

PRINT '';
PRINT '--- Clients ---';
SELECT Id, FullName FROM Clients;

PRINT '';
PRINT '--- Cars ---';
SELECT Id, Brand, Model, PlateNumber FROM Cars;

PRINT '';
PRINT '--- Parts (первые 10) ---';
SELECT TOP 10 Id, Name, Article FROM Parts;

PRINT '';
PRINT '--- Orders ---';
SELECT Id, Description, Status FROM Orders;

PRINT '';
PRINT '--- Users ---';
SELECT Id, Login, Role FROM Users;

PRINT '';
PRINT '--- Notifications ---';
SELECT Id, Message FROM Notifications;

PRINT '';
PRINT '--- Settings ---';
SELECT Id, KeyName, Value FROM Settings;

PRINT '';
PRINT '========================================';
PRINT 'ОБНОВЛЕНИЕ ЗАВЕРШЕНО УСПЕШНО!';
PRINT '========================================';
GO
