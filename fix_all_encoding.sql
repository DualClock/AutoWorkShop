-- ============================================
-- ПОЛНЫЙ СКРИПТ ИСПРАВЛЕНИЯ КОДИРОВКИ В БД
-- AutoWorkshopDB - все таблицы с русскими данными
-- ============================================
-- ВЫПОЛНИТЬ В SQL SERVER MANAGEMENT STUDIO
-- ============================================

USE AutoWorkshopDB;
GO

-- ============================================
-- 1. ТАБЛИЦА Departments (Отделы)
-- ============================================
PRINT 'Обновление таблицы Departments...';

UPDATE Departments SET Name = N'Администрация' WHERE Id = 1;
UPDATE Departments SET Name = N'Сервисный отдел' WHERE Id = 2;
UPDATE Departments SET Name = N'Отдел запчастей' WHERE Id = 3;
UPDATE Departments SET Name = N'Бухгалтерия' WHERE Id = 4;
UPDATE Departments SET Name = N'Склад' WHERE Id = 5;
GO

-- ============================================
-- 2. ТАБЛИЦА Employees (Сотрудники)
-- ============================================
PRINT 'Обновление таблицы Employees...';

UPDATE Employees SET FullName = N'Иванов Иван Иванович', Position = N'Директор', Phone = N'+7 (999) 001-01-01', Email = N'ivanov@autoworkshop.ru' WHERE Id = 1;
UPDATE Employees SET FullName = N'Петров Пётр Петрович', Position = N'Мастер-приёмщик', Phone = N'+7 (999) 002-02-02', Email = N'petrov@autoworkshop.ru' WHERE Id = 2;
UPDATE Employees SET FullName = N'Сидоров Сидор Сидорович', Position = N'Автослесарь', Phone = N'+7 (999) 003-03-03', Email = N'sidorov@autoworkshop.ru' WHERE Id = 3;
UPDATE Employees SET FullName = N'Кузнецов Алексей Михайлович', Position = N'Автослесарь', Phone = N'+7 (999) 004-04-04', Email = N'kuznetsov@autoworkshop.ru' WHERE Id = 4;
UPDATE Employees SET FullName = N'Попов Дмитрий Александрович', Position = N'Электрик', Phone = N'+7 (999) 005-05-05', Email = N'popov@autoworkshop.ru' WHERE Id = 5;
UPDATE Employees SET FullName = N'Васильева Анна Сергеевна', Position = N'Менеджер по запчастям', Phone = N'+7 (999) 006-06-06', Email = N'vasilyeva@autoworkshop.ru' WHERE Id = 6;
UPDATE Employees SET FullName = N'Соколов Николай Владимирович', Position = N'Кладовщик', Phone = N'+7 (999) 007-07-07', Email = N'sokolov@autoworkshop.ru' WHERE Id = 7;
UPDATE Employees SET FullName = N'Михайлова Елена Игоревна', Position = N'Бухгалтер', Phone = N'+7 (999) 008-08-08', Email = N'mikhailova@autoworkshop.ru' WHERE Id = 8;
GO

-- ============================================
-- 3. ТАБЛИЦА Clients (Клиенты)
-- ============================================
PRINT 'Обновление таблицы Clients...';

UPDATE Clients SET FullName = N'ООО "АвтоТранс"', Phone = N'+7 (495) 101-01-01', Email = N'info@avtotrans.ru', Address = N'г. Москва, ул. Ленина, д. 1' WHERE Id = 1;
UPDATE Clients SET FullName = N'ИП Смирнов А.В.', Phone = N'+7 (999) 102-02-02', Email = N'smirnov@mail.ru', Address = N'г. Москва, ул. Мира, д. 10' WHERE Id = 2;
UPDATE Clients SET FullName = N'Козлов Виктор Петрович', Phone = N'+7 (999) 103-03-03', Email = N'kozlov@yandex.ru', Address = N'г. Химки, ул. Гагарина, д. 5' WHERE Id = 3;
UPDATE Clients SET FullName = N'Новикова Ольга Ивановна', Phone = N'+7 (999) 104-04-04', Email = N'novikova@gmail.com', Address = N'г. Мытищи, пр. Академика, д. 20' WHERE Id = 4;
UPDATE Clients SET FullName = N'АО "ГрузПеревозки"', Phone = N'+7 (495) 105-05-05', Email = N'info@gruzper.ru', Address = N'г. Москва, МКАД, д. 50' WHERE Id = 5;
UPDATE Clients SET FullName = N'Федоров Сергей Николаевич', Phone = N'+7 (999) 106-06-06', Email = N'fedorov@mail.ru', Address = N'г. Королёв, ул. Калинина, д. 15' WHERE Id = 6;
UPDATE Clients SET FullName = N'Павлова Татьяна Андреевна', Phone = N'+7 (999) 107-07-07', Email = N'pavlova@yandex.ru', Address = N'г. Балашиха, ш. Энтузиастов, д. 30' WHERE Id = 7;
UPDATE Clients SET FullName = N'ООО "Такси-Плюс"', Phone = N'+7 (495) 108-08-08', Email = N'taxi-plus@yandex.ru', Address = N'г. Москва, ул. Таксистов, д. 7' WHERE Id = 8;
GO

-- ============================================
-- 4. ТАБЛИЦА Cars (Автомобили)
-- ============================================
PRINT 'Обновление таблицы Cars...';

UPDATE Cars SET Brand = N'ГАЗ', Model = N'Газель', VIN = N'XTT123456789012345', PlateNumber = N'А001АА799', Year = 2020 WHERE Id = 1;
UPDATE Cars SET Brand = N'ВАЗ', Model = N'2114', VIN = N'XTA211400012345678', PlateNumber = N'В002ВВ777', Year = 2015 WHERE Id = 2;
UPDATE Cars SET Brand = N'УАЗ', Model = N'Патриот', VIN = N'XTT345678901234567', PlateNumber = N'У003УУ199', Year = 2021 WHERE Id = 3;
UPDATE Cars SET Brand = N'КАМАЗ', Model = N'65115', VIN = N'XTM651150K1234567', PlateNumber = N'К004КК750', Year = 2019 WHERE Id = 4;
UPDATE Cars SET Brand = N'Ford', Model = N'Transit', VIN = N'WF0LXXTTGL1234567', PlateNumber = N'Ф005ФФ777', Year = 2018 WHERE Id = 5;
UPDATE Cars SET Brand = N'ВАЗ', Model = N'Гранта', VIN = N'XTA21900001234567', PlateNumber = N'Г006ГГ199', Year = 2022 WHERE Id = 6;
UPDATE Cars SET Brand = N'Hyundai', Model = N'Solaris', VIN = N'XTEFC123456789012', PlateNumber = N'Х007ХХ777', Year = 2020 WHERE Id = 7;
UPDATE Cars SET Brand = N'Volkswagen', Model = N'Transporter', VIN = N'WV1ZZZ7HZKE123456', PlateNumber = N'Т008ТТ750', Year = 2017 WHERE Id = 8;
GO

-- ============================================
-- 5. ТАБЛИЦА Parts (Запчасти)
-- ============================================
PRINT 'Обновление таблицы Parts...';

UPDATE Parts SET Name = N'Моторное масло 5W-40', Article = N'OIL-5W40' WHERE Id = 1;
UPDATE Parts SET Name = N'Фильтр масляный', Article = N'FLT-OIL-01' WHERE Id = 2;
UPDATE Parts SET Name = N'Фильтр воздушный', Article = N'FLT-AIR-01' WHERE Id = 3;
UPDATE Parts SET Name = N'Фильтр салонный', Article = N'FLT-CAB-01' WHERE Id = 4;
UPDATE Parts SET Name = N'Колодки тормозные передние', Article = N'BRK-FRT-01' WHERE Id = 5;
UPDATE Parts SET Name = N'Колодки тормозные задние', Article = N'BRK-RR-01' WHERE Id = 6;
UPDATE Parts SET Name = N'Свеча зажигания', Article = N'SPK-01' WHERE Id = 7;
UPDATE Parts SET Name = N'Аккумулятор 60Ач', Article = N'BAT-60' WHERE Id = 8;
UPDATE Parts SET Name = N'Лампа H7', Article = N'LMP-H7' WHERE Id = 9;
UPDATE Parts SET Name = N'Лампа H4', Article = N'LMP-H4' WHERE Id = 10;
UPDATE Parts SET Name = N'Щетка стеклоочистителя', Article = N'WPR-01' WHERE Id = 11;
UPDATE Parts SET Name = N'Антифриз G12', Article = N'ANT-G12' WHERE Id = 12;
UPDATE Parts SET Name = N'Тормозной диск', Article = N'BRK-FL' WHERE Id = 13;
UPDATE Parts SET Name = N'Ремень ГРМ', Article = N'BLT-TMG' WHERE Id = 14;
UPDATE Parts SET Name = N'Ролик натяжной', Article = N'RLR-TNS' WHERE Id = 15;
UPDATE Parts SET Name = N'Насос водяной', Article = N'PMP-WTR' WHERE Id = 16;
UPDATE Parts SET Name = N'Термостат', Article = N'THRM-01' WHERE Id = 17;
UPDATE Parts SET Name = N'Датчик кислорода', Article = N'SNS-O2' WHERE Id = 18;
UPDATE Parts SET Name = N'Датчик коленвала', Article = N'SNS-CRK' WHERE Id = 19;
UPDATE Parts SET Name = N'Форсунка топливная', Article = N'INJ-01' WHERE Id = 20;
UPDATE Parts SET Name = N'Катушка зажигания', Article = N'COIL-01' WHERE Id = 21;
UPDATE Parts SET Name = N'Прокладка ГБЦ', Article = N'GKT-HEAD' WHERE Id = 22;
UPDATE Parts SET Name = N'Сальник коленвала', Article = N'SEL-CRK' WHERE Id = 23;
UPDATE Parts SET Name = N'Подшипник ступичный', Article = N'BRG-HUB' WHERE Id = 24;
UPDATE Parts SET Name = N'Амортизатор передний', Article = N'SHK-FRT' WHERE Id = 25;
UPDATE Parts SET Name = N'Амортизатор задний', Article = N'SHK-RR' WHERE Id = 26;
UPDATE Parts SET Name = N'Рычаг подвески', Article = N'ARM-01' WHERE Id = 27;
UPDATE Parts SET Name = N'Стабилизатор', Article = N'SBL-01' WHERE Id = 28;
UPDATE Parts SET Name = N'Шаровая опора', Article = N'BALL-01' WHERE Id = 29;
UPDATE Parts SET Name = N'Рулевая тяга', Article = N'TIE-ROD' WHERE Id = 30;
GO

-- ============================================
-- 6. ТАБЛИЦА Orders (Заказы)
-- ============================================
PRINT 'Обновление таблицы Orders...';

UPDATE Orders SET Description = N'Замена масла и фильтров', Status = N'Выполнен' WHERE Id = 1;
UPDATE Orders SET Description = N'Диагностика ходовой части', Status = N'В работе' WHERE Id = 2;
UPDATE Orders SET Description = N'Замена тормозных колодок', Status = N'Выполнен' WHERE Id = 3;
UPDATE Orders SET Description = N'Ремонт двигателя', Status = N'В работе' WHERE Id = 4;
UPDATE Orders SET Description = N'Замена аккумулятора', Status = N'Выполнен' WHERE Id = 5;
UPDATE Orders SET Description = N'Компьютерная диагностика', Status = N'Ожидает' WHERE Id = 6;
UPDATE Orders SET Description = N'Замена ремня ГРМ', Status = N'В работе' WHERE Id = 7;
UPDATE Orders SET Description = N'Регулировка развал-схождения', Status = N'Выполнен' WHERE Id = 8;
GO

-- ============================================
-- 7. ТАБЛИЦА Users (Пользователи)
-- ============================================
PRINT 'Обновление таблицы Users...';

UPDATE Users SET Login = N'admin', Role = N'Администратор' WHERE Id = 1;
UPDATE Users SET Login = N'master', Role = N'Мастер' WHERE Id = 2;
UPDATE Users SET Login = N'mechanic', Role = N'Механик' WHERE Id = 3;
UPDATE Users SET Login = N'accountant', Role = N'Бухгалтер' WHERE Id = 4;
UPDATE Users SET Login = N'warehouse', Role = N'Кладовщик' WHERE Id = 5;
GO

-- ============================================
-- 8. ТАБЛИЦА Notifications (Уведомления)
-- ============================================
PRINT 'Обновление таблицы Notifications...';

UPDATE Notifications SET Message = N'Заказ №1 выполнен. Ожидает оплаты.', Type = N'Заказ' WHERE Id = 1;
UPDATE Notifications SET Message = N'Новый заказ принят в работу.', Type = N'Заказ' WHERE Id = 2;
UPDATE Notifications SET Message = N'Требуется пополнение склада запчастей.', Type = N'Склад' WHERE Id = 3;
UPDATE Notifications SET Message = N'Истекает срок ТО для автомобиля клиента.', Type = N'Напоминание' WHERE Id = 4;
UPDATE Notifications SET Message = N'Обновление прайс-листа запчастей.', Type = N'Системное' WHERE Id = 5;
GO

-- ============================================
-- 9. ТАБЛИЦА Settings (Настройки)
-- ============================================
PRINT 'Обновление таблицы Settings...';

UPDATE Settings SET KeyName = N'CompanyName', Value = N'АвтоМастер' WHERE Id = 1;
UPDATE Settings SET KeyName = N'Address', Value = N'г. Москва, ул. Авторемонтная, д. 1' WHERE Id = 2;
UPDATE Settings SET KeyName = N'Phone', Value = N'+7 (495) 123-45-67' WHERE Id = 3;
UPDATE Settings SET KeyName = N'Email', Value = N'info@avtomaster.ru' WHERE Id = 4;
UPDATE Settings SET KeyName = N'WorkingHours', Value = N'Пн-Пт: 9:00-18:00, Сб: 10:00-15:00' WHERE Id = 5;
GO

-- ============================================
-- ПРОВЕРКА РЕЗУЛЬТАТОВ
-- ============================================
PRINT 'Проверка результатов...';

PRINT '';
PRINT '=== Departments ===';
SELECT Id, Name FROM Departments;

PRINT '';
PRINT '=== Employees ===';
SELECT Id, FullName, Position FROM Employees;

PRINT '';
PRINT '=== Clients ===';
SELECT Id, FullName, Phone FROM Clients;

PRINT '';
PRINT '=== Cars ===';
SELECT Id, Brand, Model, PlateNumber FROM Cars;

PRINT '';
PRINT '=== Parts (первые 10) ===';
SELECT TOP 10 Id, Name, Article FROM Parts;

PRINT '';
PRINT '=== Orders ===';
SELECT Id, Description, Status FROM Orders;

PRINT '';
PRINT '=== Users ===';
SELECT Id, Login, Role FROM Users;

PRINT '';
PRINT '=== Notifications ===';
SELECT Id, Message, Type FROM Notifications;

PRINT '';
PRINT '=== Settings ===';
SELECT Id, KeyName, Value FROM Settings;

PRINT '';
PRINT '========================================';
PRINT 'ОБНОВЛЕНИЕ ЗАВЕРШЕНО УСПЕШНО!';
PRINT '========================================';
GO
