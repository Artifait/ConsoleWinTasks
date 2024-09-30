# Name DB: Stationery
```
CREATE TABLE ProductType (
    Id INT PRIMARY KEY IDENTITY(1, 1),  
    TypeName NVARCHAR(100) NOT NULL    
);

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1, 1), 
    ProductName NVARCHAR(100) NOT NULL,
    ProductTypeId INT NOT NULL,        
    Quantity INT NOT NULL,             
    CostPrice DECIMAL(10, 2) NOT NULL, 
    UnitPrice DECIMAL(10, 2) NOT NULL, 
    CONSTRAINT FK_Products_ProductType FOREIGN KEY (ProductTypeId) REFERENCES ProductType(Id)
);

CREATE TABLE Managers (
    Id INT PRIMARY KEY IDENTITY(1, 1), 
    ManagerName NVARCHAR(100) NOT NULL  
);

CREATE TABLE Firms (
    Id INT PRIMARY KEY IDENTITY(1, 1),
    FirmName NVARCHAR(100) NOT NULL  
);

CREATE TABLE Sales (
    Id INT PRIMARY KEY IDENTITY(1, 1),  
    ProductId INT NOT NULL,        
    ManagerId INT NOT NULL,           
    FirmId INT NOT NULL,             
    QuantitySold INT NOT NULL,         
    SaleDate DATE NOT NULL,          
    CONSTRAINT FK_Sales_Products FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT FK_Sales_Managers FOREIGN KEY (ManagerId) REFERENCES Managers(Id),
    CONSTRAINT FK_Sales_Firms FOREIGN KEY (FirmId) REFERENCES Firms(Id)
);


INSERT INTO ProductType (TypeName) 
VALUES 
(N'Ручки'),
(N'Бумага'),
(N'Карандаши'),
(N'Тетради'),
(N'Маркер'),
(N'Скрепки');

INSERT INTO Products (ProductName, ProductTypeId, Quantity, CostPrice, UnitPrice) 
VALUES 
(N'Ручка шариковая', 1, 100, 5.50, 7.00),
(N'Бумага для принтера', 2, 200, 300.00, 350.00),
(N'Карандаш простой', 3, 150, 10.00, 12.00),
(N'Тетрадь в клетку', 4, 250, 15.00, 20.00),
(N'Маркер черный', 5, 80, 25.00, 30.00),
(N'Скрепки', 6, 500, 1.50, 2.00);

INSERT INTO Managers (ManagerName) 
VALUES 
(N'Иван Иванов'),
(N'Мария Петрова'),
(N'Сергей Смирнов'),
(N'Елена Козлова'),
(N'Алексей Сидоров'),
(N'Анна Васильева');

INSERT INTO Firms (FirmName) 
VALUES 
(N'ООО "КанцСнаб"'),
(N'ЗАО "БумагаПлюс"'),
(N'ИП "МаркерТорг"'),
(N'ООО "ТетрадьОпт"'),
(N'ОАО "ОфисМир"'),
(N'ЗАО "СкрепСнаб"');

INSERT INTO Sales (ProductId, ManagerId, FirmId, QuantitySold, SaleDate) 
VALUES 
(1, 1, 1, 50, '2024-09-30'),
(2, 2, 2, 100, '2024-09-29'),
(3, 3, 3, 75, '2024-09-28'),
(4, 4, 4, 120, '2024-09-27'),
(5, 5, 5, 60, '2024-09-26'),
(6, 6, 6, 400, '2024-09-25');
```
