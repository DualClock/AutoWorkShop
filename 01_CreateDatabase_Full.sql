/*
================================================================================
    AutoWorkshopDB - ПОЛНЫЙ СКРИПТ СОЗДАНИЯ И ЗАПОЛНЕНИЯ БАЗЫ ДАННЫХ
    Система управления автосервисом "АвтоМаст ИС"
    Версия: 1.0
    
    Выполните этот скрипт в SQL Server Management Studio
================================================================================
*/

USE master;
GO

-- ============================================
-- ПОДГОТОВКА: Удаляем базу данных если существует
-- ============================================
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'AutoWorkshopDB')
BEGIN
    ALTER DATABASE AutoWorkshopDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE AutoWorkshopDB;
    PRINT 'База данных AutoWorkshopDB удалена';
END
GO

-- ============================================
-- СОЗДАНИЕ БАЗЫ ДАННЫХ
-- ============================================
CREATE DATABASE AutoWorkshopDB;
GO

USE AutoWorkshopDB;
GO

PRINT 'База данных AutoWorkshopDB создана';
PRINT '';
PRINT '========================================';
PRINT 'СОЗДАНИЕ ТАБЛИЦ';
PRINT '========================================';
GO

-- ============================================
-- ТАБЛИЦА: Отделы
-- ============================================
CREATE TABLE Departments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);
GO

-- ============================================
-- ТАБЛИЦА: Сотрудники
-- ============================================
CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Position NVARCHAR(50),
    Phone NVARCHAR(20),
    Email NVARCHAR(50),
    DepartmentId INT,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id) ON DELETE SET NULL
);
GO

-- ============================================
-- ТАБЛИЦА: Клиенты
-- ============================================
CREATE TABLE Clients (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Email NVARCHAR(50),
    Address NVARCHAR(255),
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- ============================================
-- ТАБЛИЦА: Автомобили
-- ============================================
CREATE TABLE Cars (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClientId INT NOT NULL,
    Brand NVARCHAR(50) NOT NULL,
    Model NVARCHAR(50) NOT NULL,
    Year INT,
    VIN NVARCHAR(17),
    PlateNumber NVARCHAR(20),
    FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE
);
GO

-- ============================================
-- ТАБЛИЦА: Запчасти
-- ============================================
CREATE TABLE Parts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Article NVARCHAR(50),
    Price DECIMAL(18,2) NOT NULL DEFAULT 0,
    Quantity INT NOT NULL DEFAULT 0,
    MinQuantity INT NOT NULL DEFAULT 5
);
GO

-- ============================================
-- ТАБЛИЦА: Заказы
-- ============================================
CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CarId INT NOT NULL,
    EmployeeId INT,
    Description NVARCHAR(500),
    Status NVARCHAR(20) NOT NULL DEFAULT 'New',
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    CompletedDate DATETIME2,
    TotalCost DECIMAL(18,2) DEFAULT 0,
    FOREIGN KEY (CarId) REFERENCES Cars(Id) ON DELETE CASCADE,
    FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) ON DELETE SET NULL
);
GO

-- ============================================
-- ТАБЛИЦА: Запчасти в заказе
-- ============================================
CREATE TABLE OrderParts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    PartId INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    Price DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    FOREIGN KEY (PartId) REFERENCES Parts(Id) ON DELETE CASCADE
);
GO

-- ============================================
-- ТАБЛИЦА: Пользователи
-- ============================================
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Login NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL,
    EmployeeId INT,
    IsActive BIT NOT NULL DEFAULT 1,
    LastLogin DATETIME2,
    FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) ON DELETE SET NULL
);
GO

-- ============================================
-- ТАБЛИЦА: Квитанции
-- ============================================
CREATE TABLE Receipts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    PaymentDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    PaymentMethod NVARCHAR(20),
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE
);
GO

-- ============================================
-- ТАБЛИЦА: Уведомления
-- ============================================
CREATE TABLE Notifications (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    Message NVARCHAR(500) NOT NULL,
    IsRead BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    Type NVARCHAR(20),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);
GO

-- ============================================
-- ТАБЛИЦА: Настройки
-- ============================================
CREATE TABLE Settings (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    KeyName NVARCHAR(50) NOT NULL UNIQUE,
    Value NVARCHAR(255),
    Description NVARCHAR(255)
);
GO

PRINT 'Все таблицы созданы';
PRINT '';
PRINT '========================================';
PRINT 'СОЗДАНИЕ ИНДЕКСОВ';
PRINT '========================================';
GO

-- Индексы для производительности
CREATE INDEX IX_Employees_DepartmentId ON Employees(DepartmentId);
CREATE INDEX IX_Cars_ClientId ON Cars(ClientId);
CREATE INDEX IX_Orders_CarId ON Orders(CarId);
CREATE INDEX IX_Orders_Status ON Orders(Status);
CREATE INDEX IX_OrderParts_OrderId ON OrderParts(OrderId);
CREATE INDEX IX_Users_Login ON Users(Login);
CREATE INDEX IX_Users_Role ON Users(Role);
GO

PRINT 'Индексы созданы';
PRINT '';
PRINT '========================================';
PRINT 'ЗАПОЛНЕНИЕ ДАННЫМИ';
PRINT '========================================';
GO

-- ============================================
-- Отделы
-- ============================================
PRINT 'Заполнение Departments...';
INSERT INTO Departments (Name) VALUES
(N'Администрация'),
(N'Сервисный отдел'),
(N'Отдел запчастей'),
(N'Бухгалтерия'),
(N'Склад');
GO

-- ============================================
-- Сотрудники
-- ============================================
PRINT 'Заполнение Employees...';
INSERT INTO Employees (FullName, Position, Phone, Email, DepartmentId) VALUES
(N'Иванов Иван Иванович', N'Директор', N'+7 (999) 001-01-01', N'ivanov@autoworkshop.ru', 1),
(N'Петров Пётр Петрович', N'Мастер-приёмщик', N'+7 (999) 002-02-02', N'petrov@autoworkshop.ru', 2),
(N'Сидоров Сидор Сидорович', N'Автослесарь', N'+7 (999) 003-03-03', N'sidorov@autoworkshop.ru', 2),
(N'Кузнецов Алексей Михайлович', N'Автослесарь', N'+7 (999) 004-04-04', N'kuznetsov@autoworkshop.ru', 2),
(N'Попов Дмитрий Александрович', N'Электрик', N'+7 (999) 005-05-05', N'popov@autoworkshop.ru', 2),
(N'Васильева Анна Сергеевна', N'Менеджер по запчастям', N'+7 (999) 006-06-06', N'vasilyeva@autoworkshop.ru', 3),
(N'Соколов Николай Владимирович', N'Кладовщик', N'+7 (999) 007-07-07', N'sokolov@autoworkshop.ru', 5),
(N'Михайлова Елена Игоревна', N'Бухгалтер', N'+7 (999) 008-08-08', N'mikhailova@autoworkshop.ru', 4),
(N'Козлов Андрей Викторович', N'Автослесарь', N'+7 (999) 009-09-09', N'kozlov@autoworkshop.ru', 2),
(N'Новикова Мария Павловна', N'Администратор', N'+7 (999) 010-10-10', N'novikova@autoworkshop.ru', 1);
GO

-- ============================================
-- Клиенты
-- ============================================
PRINT 'Заполнение Clients...';
INSERT INTO Clients (FullName, Phone, Email, Address) VALUES
(N'ООО "АвтоТранс"', N'+7 (495) 101-01-01', N'info@avtotrans.ru', N'г. Москва, ул. Ленина, д. 1'),
(N'ИП Смирнов А.В.', N'+7 (999) 102-02-02', N'smirnov@mail.ru', N'г. Москва, ул. Мира, д. 10'),
(N'Козлов Виктор Петрович', N'+7 (999) 103-03-03', N'kozlov@yandex.ru', N'г. Химки, ул. Гагарина, д. 5'),
(N'Новикова Ольга Ивановна', N'+7 (999) 104-04-04', N'novikova@gmail.com', N'г. Мытищи, пр. Академика, д. 20'),
(N'АО "ГрузПеревозки"', N'+7 (495) 105-05-05', N'info@gruzper.ru', N'г. Москва, МКАД, д. 50'),
(N'Федоров Сергей Николаевич', N'+7 (999) 106-06-06', N'fedorov@mail.ru', N'г. Королёв, ул. Калинина, д. 15'),
(N'Павлова Татьяна Андреевна', N'+7 (999) 107-07-07', N'pavlova@yandex.ru', N'г. Балашиха, ш. Энтузиастов, д. 30'),
(N'ООО "Такси-Плюс"', N'+7 (495) 108-08-08', N'taxi-plus@yandex.ru', N'г. Москва, ул. Таксистов, д. 7'),
(N'ИП Морозов И.К.', N'+7 (999) 109-09-09', N'morozov@mail.ru', N'г. Подольск, ул. Ленина, д. 45'),
(N'ЗАО "АвтоЛайн"', N'+7 (495) 110-10-11', N'info@autoline.ru', N'г. Москва, Варшавское ш., д. 100');
GO

-- ============================================
-- Автомобили
-- ============================================
PRINT 'Заполнение Cars...';
INSERT INTO Cars (ClientId, Brand, Model, Year, VIN, PlateNumber) VALUES
(1, N'ГАЗ', N'Газель', 2020, N'XTT123456789012345', N'А001АА799'),
(2, N'ВАЗ', N'2114', 2015, N'XTA211400012345678', N'В002ВВ777'),
(3, N'УАЗ', N'Патриот', 2021, N'XTT345678901234567', N'У003УУ199'),
(4, N'КАМАЗ', N'65115', 2019, N'XTM651150K1234567', N'К004КК750'),
(5, N'Ford', N'Transit', 2018, N'WF0LXXTTGL1234567', N'Ф005ФФ777'),
(6, N'ВАЗ', N'Гранта', 2022, N'XTA21900001234567', N'Г006ГГ199'),
(7, N'Hyundai', N'Solaris', 2020, N'XTEFC123456789012', N'Х007ХХ777'),
(8, N'Volkswagen', N'Transporter', 2017, N'WV1ZZZ7HZKE123456', N'Т008ТТ750'),
(9, N'ГАЗ', N'Соболь', 2021, N'XTT987654321098765', N'С009СС799'),
(10, N'ВАЗ', N'Веста', 2023, N'XTA21800009876543', N'В010ВВ777');
GO

-- ============================================
-- Запчасти
-- ============================================
PRINT 'Заполнение Parts...';
INSERT INTO Parts (Name, Article, Price, Quantity, MinQuantity) VALUES
(N'Моторное масло 5W-40', N'OIL-5W40', 5000.00, 50, 10),
(N'Фильтр масляный', N'FLT-OIL-01', 800.00, 100, 20),
(N'Фильтр воздушный', N'FLT-AIR-01', 1200.00, 80, 15),
(N'Фильтр салонный', N'FLT-CAB-01', 1500.00, 60, 10),
(N'Колодки тормозные передние', N'BRK-FRT-01', 3500.00, 40, 8),
(N'Колодки тормозные задние', N'BRK-RR-01', 2500.00, 40, 8),
(N'Свеча зажигания', N'SPK-01', 600.00, 200, 50),
(N'Аккумулятор 60Ач', N'BAT-60', 7000.00, 20, 5),
(N'Лампа H7', N'LMP-H7', 300.00, 150, 30),
(N'Лампа H4', N'LMP-H4', 350.00, 150, 30),
(N'Щетка стеклоочистителя', N'WPR-01', 900.00, 100, 20),
(N'Антифриз G12', N'ANT-G12', 1800.00, 60, 10),
(N'Тормозной диск', N'BRK-FL', 4500.00, 50, 10),
(N'Ремень ГРМ', N'BLT-TMG', 4500.00, 30, 5),
(N'Ролик натяжной', N'RLR-TNS', 2000.00, 30, 5),
(N'Насос водяной', N'PMP-WTR', 5500.00, 20, 5),
(N'Термостат', N'THRM-01', 2200.00, 25, 5),
(N'Датчик кислорода', N'SNS-O2', 4000.00, 15, 3),
(N'Датчик коленвала', N'SNS-CRK', 2500.00, 15, 3),
(N'Форсунка топливная', N'INJ-01', 6000.00, 10, 2);
GO

-- ============================================
-- Заказы
-- ============================================
PRINT 'Заполнение Orders...';
INSERT INTO Orders (CarId, EmployeeId, Description, Status, TotalCost, CreatedDate, CompletedDate) VALUES
(1, 2, N'Замена масла и фильтров', N'Выполнен', 8500.00, '2026-01-10 10:00:00', '2026-01-10 14:00:00'),
(2, 3, N'Диагностика ходовой части', N'В работе', 2000.00, '2026-01-12 09:00:00', NULL),
(3, 3, N'Замена тормозных колодок', N'Выполнен', 12000.00, '2026-01-15 11:00:00', '2026-01-15 16:00:00'),
(4, 4, N'Ремонт двигателя', N'В работе', 45000.00, '2026-01-18 08:00:00', NULL),
(5, 5, N'Замена аккумулятора', N'Выполнен', 9000.00, '2026-01-20 12:00:00', '2026-01-20 13:00:00'),
(6, 2, N'Компьютерная диагностика', N'Ожидает', 3000.00, '2026-01-22 14:00:00', NULL),
(7, 3, N'Замена ремня ГРМ', N'В работе', 18000.00, '2026-01-25 09:00:00', NULL),
(8, 4, N'Регулировка развал-схождения', N'Выполнен', 4500.00, '2026-01-28 10:00:00', '2026-01-28 12:00:00'),
(9, 3, N'Замена тормозных дисков', N'Выполнен', 15000.00, '2026-02-01 11:00:00', '2026-02-01 17:00:00'),
(10, 5, N'Замена свечей зажигания', N'Ожидает', 5000.00, '2026-02-05 13:00:00', NULL);
GO

-- ============================================
-- Пользователи
-- ============================================
PRINT 'Заполнение Users...';
-- Пароли по умолчанию:
-- admin / admin123
-- master / master123
-- mechanic / mechanic123
INSERT INTO Users (Login, Password, Role, EmployeeId, IsActive) VALUES
(N'admin', N'admin123', N'Администратор', 1, 1),
(N'master', N'master123', N'Мастер', 2, 1),
(N'mechanic', N'mechanic123', N'Механик', 3, 1),
(N'accountant', N'accountant123', N'Бухгалтер', 8, 1),
(N'warehouse', N'warehouse123', N'Кладовщик', 7, 1);
GO

-- ============================================
-- Уведомления
-- ============================================
PRINT 'Заполнение Notifications...';
INSERT INTO Notifications (UserId, Message, IsRead, CreatedDate, Type) VALUES
(1, N'Заказ №1 выполнен. Ожидает оплаты.', 0, '2026-01-10 14:00:00', N'Заказ'),
(2, N'Новый заказ принят в работу.', 0, '2026-01-12 09:00:00', N'Заказ'),
(6, N'Требуется пополнение склада запчастей.', 1, '2026-01-15 10:00:00', N'Склад'),
(1, N'Истекает срок ТО для автомобиля клиента.', 0, '2026-01-18 08:00:00', N'Напоминание'),
(5, N'Обновление прайс-листа запчастей.', 1, '2026-01-20 12:00:00', N'Системное'),
(2, N'Клиент ждёт завершения ремонта.', 0, '2026-01-22 15:00:00', N'Заказ'),
(3, N'Необходимо завершить диагностику.', 0, '2026-01-25 11:00:00', N'Задача'),
(1, N'Отчёт за месяц готов к проверке.', 0, '2026-02-01 09:00:00', N'Отчёт');
GO

-- ============================================
-- Настройки
-- ============================================
PRINT 'Заполнение Settings...';
INSERT INTO Settings (KeyName, Value, Description) VALUES
(N'CompanyName', N'АвтоМаст ИС', N'Название компании'),
(N'Address', N'г. Москва, ул. Авторемонтная, д. 1', N'Адрес'),
(N'Phone', N'+7 (495) 123-45-67', N'Телефон'),
(N'Email', N'info@automast.ru', N'Email'),
(N'WorkingHours', N'Пн-Пт: 9:00-18:00, Сб: 10:00-15:00', N'Часы работы'),
(N'TaxRate', N'20', N'Ставка налога (%)');
GO

PRINT '';
PRINT '========================================';
PRINT 'ПРОВЕРКА РЕЗУЛЬТАТОВ';
PRINT '========================================';
GO

PRINT '';
PRINT 'Количество записей в таблицах:';
SELECT 'Departments' AS Таблица, COUNT(*) AS Количество FROM Departments
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
SELECT 'Users', COUNT(*) FROM Users
UNION ALL
SELECT 'Notifications', COUNT(*) FROM Notifications
UNION ALL
SELECT 'Settings', COUNT(*) FROM Settings;
GO

PRINT '';
PRINT '========================================';
PRINT 'УЧЁТНЫЕ ДАННЫЕ ДЛЯ ВХОДА';
PRINT '========================================';
PRINT '';
PRINT '  Логин      | Пароль       | Роль';
PRINT '  -----------|--------------|---------------';
PRINT '  admin      | admin123     | Администратор';
PRINT '  master     | master123    | Мастер';
PRINT '  mechanic   | mechanic123  | Механик';
PRINT '  accountant | accountant123| Бухгалтер';
PRINT '  warehouse  | warehouse123 | Кладовщик';
PRINT '';
PRINT '========================================';
PRINT 'БАЗА ДАННЫХ УСПЕШНО СОЗДАНА!';
PRINT '========================================';
GO
