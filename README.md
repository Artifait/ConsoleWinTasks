# Модуль 2
## Создание и заполнение начальными данными(База называеться: Storage):

```

-- Таблица типов товаров
CREATE TABLE ProductTypes (
    Id INT identity(1, 1) PRIMARY KEY,
    TypeName NVARCHAR(100) NOT NULL UNIQUE
);

-- Таблица поставщиков
CREATE TABLE Suppliers (
    Id INT identity(1, 1) PRIMARY KEY,
    SupplierName NVARCHAR(100) NOT NULL UNIQUE,
    ContactInfo NVARCHAR(255)
);

-- Таблица товаров
CREATE TABLE Products (
    Id INT identity(1, 1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
	DeliveryDate DATE NOT NULL,
    TypeID INT NOT NULL,
    SupplierID INT NOT NULL,
    FOREIGN KEY (TypeID) REFERENCES ProductTypes(Id),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(Id)
);


--Вставка нового типа товаров
INSERT INTO ProductTypes (TypeName)
VALUES (N'Электроника'), (N'Мебель'), (N'Одежда'), (N'Еда'), (N'Посуда'), (N'Канцелярия'), (N'Лекарства');

-- Вставка данных в таблицу поставщиков
INSERT INTO Suppliers (SupplierName, ContactInfo)
VALUES 
(N'ООО "ТехноПарк"', N'г. Москва, ул. Ленина, д. 10, тел: +7 495 123-45-67'),
(N'ИП "Мебельный Мир"', N'г. Санкт-Петербург, ул. Советская, д. 15, тел: +7 812 234-56-78'),
(N'ООО "Мода и Стиль"', N'г. Казань, ул. Центральная, д. 25, тел: +7 843 345-67-89'),
(N'ЗАО "Продукты 24"', N'г. Новосибирск, ул. Комсомольская, д. 12, тел: +7 383 456-78-90'),
(N'ООО "Керамика и Посуда"', N'г. Екатеринбург, ул. Мира, д. 8, тел: +7 343 567-89-01'),
(N'ИП "КанцСнаб"', N'г. Самара, ул. Пушкина, д. 6, тел: +7 846 678-90-12'),
(N'АО "ФармаЛайн"', N'г. Нижний Новгород, ул. Горького, д. 5, тел: +7 831 789-01-23');


-- Вставка данных в таблицу товаров
INSERT INTO Products (ProductName, Quantity, Price, DeliveryDate, TypeID, SupplierID)
VALUES 
(N'Смартфон', 50, 25000.00, '2024-09-01', 1, 1), -- Электроника
(N'Ноутбук', 30, 70000.00, '2024-09-05', 1, 1), -- Электроника
(N'Диван', 15, 45000.00, '2024-09-10', 2, 2),   -- Мебель
(N'Куртка зимняя', 100, 8000.00, '2024-09-12', 3, 3), -- Одежда
(N'Хлеб', 200, 50.00, '2024-09-07', 4, 4),     -- Еда
(N'Чайник', 80, 1200.00, '2024-09-08', 5, 5),  -- Посуда
(N'Бумага А4', 500, 300.00, '2024-09-09', 6, 6), -- Канцелярия
(N'Антибиотики', 60, 1500.00, '2024-09-11', 7, 7); -- Лекарства

```