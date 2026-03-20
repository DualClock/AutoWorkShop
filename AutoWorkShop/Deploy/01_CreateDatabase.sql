/*
================================================================================
    AutoWorkshopDB - Скрипт создания базы данных
    Система управления автосервисом "АвтоМаст ИС"
    
    Выполните этот скрипт в SQL Server Management Studio
================================================================================
*/

USE master;
GO

-- Удаляем базу данных если существует
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'AutoWorkshopDB')
BEGIN
    ALTER DATABASE AutoWorkshopDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE AutoWorkshopDB;
    PRINT 'База данных AutoWorkshopDB удалена';
END
GO

-- Создаём базу данных
CREATE DATABASE AutoWorkshopDB;
GO

USE AutoWorkshopDB;
GO

PRINT 'База данных AutoWorkshopDB создана';
GO

-- Таблица: Отделы
CREATE TABLE Departments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);
GO

-- Таблица: Сотрудники
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

-- Таблица: Клиенты
CREATE TABLE Clients (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Email NVARCHAR(50),
    Address NVARCHAR(255),
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- Таблица: Автомобили
CREATE TABLE Cars (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClientId INT NOT NULL,
    Make NVARCHAR(50) NOT NULL,
    Model NVARCHAR(50) NOT NULL,
    Year INT,
    VIN NVARCHAR(17),
    LicensePlate NVARCHAR(20),
    FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE
);
GO

-- Таблица: Запчасти
CREATE TABLE Parts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    PartNumber NVARCHAR(50),
    Price DECIMAL(18,2) NOT NULL DEFAULT 0,
    Quantity INT NOT NULL DEFAULT 0,
    Unit NVARCHAR(20) DEFAULT 'шт'
);
GO

-- Таблица: Заказы
CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CarId INT NOT NULL,
    EmployeeId INT,
    Complaint NVARCHAR(500),
    Status NVARCHAR(20) NOT NULL DEFAULT 'New',
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    CompletedDate DATETIME2,
    TotalAmount DECIMAL(18,2) DEFAULT 0,
    FOREIGN KEY (CarId) REFERENCES Cars(Id) ON DELETE CASCADE,
    FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) ON DELETE SET NULL
);
GO

-- Таблица: Запчасти в заказе
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

-- Таблица: Пользователи
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

-- Таблица: Квитанции
CREATE TABLE Receipts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    PaymentDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    PaymentMethod NVARCHAR(20),
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE
);
GO

-- Таблица: Уведомления
CREATE TABLE Notifications (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    Message NVARCHAR(500) NOT NULL,
    IsRead BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);
GO

-- Таблица: Настройки
CREATE TABLE Settings (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    KeyName NVARCHAR(50) NOT NULL UNIQUE,
    Value NVARCHAR(255),
    Description NVARCHAR(255)
);
GO

PRINT 'Все таблицы созданы';
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
GO

-- Добавляем тестовые данные

-- Отделы
INSERT INTO Departments (Name) VALUES 
('Администрация'),
('Приёмный пункт'),
('Ремонтная зона'),
('Склад');
GO

-- Сотрудники
INSERT INTO Employees (FullName, Position, Phone, Email, DepartmentId) VALUES
('Администратор Системы', 'Администратор', '+7 (999) 000-00-00', 'admin@automast.ru', 1),
('Иванов Иван', 'Менеджер', '+7 (999) 111-11-11', 'ivanov@automast.ru', 2),
('Петров Пётр', 'Механик', '+7 (999) 222-22-22', 'petrov@automast.ru', 3),
('Сидоров Сидор', 'Кладовщик', '+7 (999) 333-33-33', 'sidorov@automast.ru', 4);
GO

-- Пользователи (пароль: admin123 для админа, password123 для остальных)
INSERT INTO Users (Login, Password, Role, EmployeeId, IsActive) VALUES
('admin', 'admin123', 'Администратор', 1, 1),
('ivanov', 'password123', 'Мастер', 2, 1),
('petrov', 'password123', 'Механик', 3, 1);
GO

-- Запчасти
INSERT INTO Parts (Name, PartNumber, Price, Quantity, Unit) VALUES
('Масло моторное 5W-40', 'OIL-5W40-4L', 2500.00, 50, 'шт'),
('Фильтр масляный', 'FLT-OIL-001', 450.00, 100, 'шт'),
('Фильтр воздушный', 'FLT-AIR-001', 800.00, 75, 'шт'),
('Колодки тормозные передние', 'BRK-PAD-FRT', 3200.00, 30, 'компл'),
('Свеча зажигания', 'SPARK-001', 650.00, 200, 'шт');
GO

-- Клиенты
INSERT INTO Clients (FullName, Phone, Email, Address) VALUES
('ООО "ТрансЛогистик"', '+7 (999) 444-44-44', 'info@translog.ru', 'ул. Ленина, 10'),
('ИП Смирнов А.А.', '+7 (999) 555-55-55', 'smirnov@mail.ru', 'пр. Мира, 25-30'),
('Козлов Дмитрий', '+7 (999) 666-66-66', 'kozlov@yandex.ru', 'ул. Гагарина, 5');
GO

-- Автомобили
INSERT INTO Cars (ClientId, Make, Model, Year, VIN, LicensePlate) VALUES
(1, 'Ford', 'Transit', 2020, 'X7F8A9B0C1D2E3F4G', 'А123АА 777'),
(2, 'Toyota', 'Camry', 2019, 'J1K2L3M4N5O6P7Q8R', 'В456ВВ 777'),
(3, 'BMW', 'X5', 2021, 'S9T0U1V2W3X4Y5Z6A', 'С789СС 777');
GO

-- Настройки
INSERT INTO Settings (KeyName, Value, Description) VALUES
('CompanyName', 'АвтоМаст ИС', 'Название компании'),
('TaxRate', '20', 'Ставка налога (%)),
('WorkingHours', '09:00-18:00', 'Часы работы');
GO

PRINT 'Тестовые данные добавлены';
GO

PRINT '';
PRINT '================================================================================';
PRINT '    БАЗА ДАННЫХ УСПЕШНО СОЗДАНА!';
PRINT '================================================================================';
PRINT '    Логин/пароль для входа:';
PRINT '    - admin / admin123 (Администратор)';
PRINT '    - ivanov / password123 (Менеджер)';
PRINT '    - petrov / password123 (Рабочий)';
PRINT '================================================================================';
GO
