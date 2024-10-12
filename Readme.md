## DB Name: CoffeeShop

```
-- Таблица для стран происхождения кофе
CREATE TABLE Countries (
    Id INT PRIMARY KEY IDENTITY(1, 1),    
    CountryName NVARCHAR(100) NOT NULL           
);

-- Таблица для видов кофе
CREATE TABLE CoffeeTypes (
    Id INT PRIMARY KEY IDENTITY(1, 1), 
    CoffeeTypeName NVARCHAR(50) NOT NULL         
);

-- Основная таблица для хранения информации о сортах кофе
CREATE TABLE Coffee (
    CoffeeID INT PRIMARY KEY IDENTITY(1, 1),    
    CoffeeName NVARCHAR(255) NOT NULL,         
    CountryID INT NOT NULL,                      
    CoffeeTypeID INT NOT NULL,                 
    Description NVARCHAR(MAX),                  
    QuantityInGrams INT NOT NULL,              
    CostPrice DECIMAL(10, 2) NOT NULL,         
    FOREIGN KEY (CountryID) REFERENCES Countries(Id),
    FOREIGN KEY (CoffeeTypeID) REFERENCES CoffeeTypes(Id)
);




INSERT INTO Countries (CountryName) 
VALUES 
('Ethiopia'), 
('Colombia'), 
('Vietnam'), 
('Brazil'), 
('Indonesia'), 
('Kenya'), 
('Guatemala'),
('Honduras'),
('Peru'),
('Mexico');

INSERT INTO CoffeeTypes (CoffeeTypeName) 
VALUES 
('Arabica'), 
('Robusta'), 
('Blend');

INSERT INTO Coffee (CoffeeName, CountryID, CoffeeTypeID, Description, QuantityInGrams, CostPrice) 
VALUES 
-- Арабика из разных стран
('Ethiopian Yirgacheffe', 1, 1, 'Floral and citrus notes with a smooth finish.', 500, 15.00),
('Colombian Supremo', 2, 1, 'Balanced flavor with notes of chocolate and nuts.', 1000, 12.50),
('Guatemalan Antigua', 7, 1, 'Bright acidity with flavors of chocolate and citrus.', 750, 13.50),
('Peruvian Organic', 9, 1, 'Mild and nutty, with smooth finish.', 1000, 14.00),
('Mexican Altura', 10, 1, 'Delicate body with fruity and sweet notes.', 500, 12.00),

-- Робуста из разных стран
('Vietnamese Robusta', 3, 2, 'Strong, bold flavor with hints of dark chocolate.', 750, 8.00),
('Indian Robusta', 3, 2, 'Smoky and earthy with a bitter aftertaste.', 1000, 7.50),
('Ugandan Robusta', 6, 2, 'Heavy body with strong bitterness.', 750, 7.80),
('Indonesian Robusta', 5, 2, 'Full-bodied with earthy and woody notes.', 500, 9.00),

-- Купаж/Бленд (смешанные сорта)
('Brazilian Santos Blend', 4, 3, 'Smooth body with nutty and caramel flavors.', 1000, 10.00),
('Honduran Medium Roast Blend', 8, 3, 'Balanced flavor with hints of cocoa and fruit.', 750, 11.50),
('Sumatran Mandheling Blend', 5, 3, 'Full-bodied with spicy and earthy notes.', 1000, 14.50),
('Kenya AA Blend', 6, 3, 'Bright acidity with flavors of berries and wine.', 750, 16.50),
('Colombian Espresso Blend', 2, 3, 'Strong and intense with chocolatey undertones.', 1000, 15.00),

-- Дополнительные арабики
('Honduran Reserve', 8, 1, 'Full-bodied with bright acidity and a floral aroma.', 500, 13.00),
('Kenyan Peaberry', 6, 1, 'Bright and fruity with a wine-like acidity.', 500, 16.00);
```