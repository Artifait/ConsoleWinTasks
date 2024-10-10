```
-- Создание таблицы для континентов (часть света)
CREATE TABLE Continents (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Идентификатор континента
    ContinentName NVARCHAR(100) NOT NULL -- Название континента
);
GO

-- Создание таблицы для хранения данных о странах (без внешнего ключа на IdCapitalCity)
CREATE TABLE Countries (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Идентификатор страны
    CountryName NVARCHAR(100) NOT NULL, -- Название страны
    Population INT NOT NULL, -- Количество жителей страны
    Area DECIMAL(18,2) NOT NULL, -- Площадь страны в км²
    IdContinent INT NOT NULL, -- Внешний ключ на таблицу Continents
    CapitalName NVARCHAR(100) NOT NULL, -- Название столицы
    FOREIGN KEY (IdContinent) REFERENCES Continents(Id) -- Связь с таблицей континентов
);
GO

-- Создание таблицы для крупных городов
CREATE TABLE Cities (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Идентификатор города
    CityName NVARCHAR(100) NOT NULL, -- Название крупного города
    CityPopulation INT NOT NULL, -- Количество жителей крупного города
    IdCountry INT NOT NULL, -- Внешний ключ на таблицу стран
    FOREIGN KEY (IdCountry) REFERENCES Countries(Id) -- Связь с таблицей стран
);
GO

-- Обновление таблицы стран для добавления внешнего ключа на столицу (ссылку на Cities)
ALTER TABLE Countries
ADD IdCapitalCity INT,
    FOREIGN KEY (IdCapitalCity) REFERENCES Cities(Id);
GO

-- Вставка данных в таблицу Continents
INSERT INTO Continents (ContinentName) VALUES (N'Европа'), (N'Азия'), (N'Африка'), (N'Северная Америка'), (N'Южная Америка'), (N'Австралия');

-- Вставка данных в таблицу Countries
INSERT INTO Countries (CountryName, Population, Area, IdContinent, CapitalName) VALUES
(N'Россия', 146000000, 17098242, 2, N'Москва'),
(N'Китай', 1400000000, 9596960, 2, N'Пекин'),
(N'Германия', 83000000, 357022, 1, N'Берлин'),
(N'Франция', 67000000, 643801, 1, N'Париж'),
(N'Египет', 102000000, 1002450, 3, N'Каир'),
(N'Канада', 38000000, 9984670, 4, N'Оттава'),
(N'Бразилия', 213000000, 8515767, 5, N'Бразилиа'),
(N'Австралия', 25600000, 7692024, 6, N'Канберра'),
(N'Япония', 126000000, 377975, 2, N'Токио'),
(N'Южная Африка', 60000000, 1221037, 3, N'Претория');

-- Вставка данных в таблицу Cities
INSERT INTO Cities (CityName, CityPopulation, IdCountry) VALUES
(N'Санкт-Петербург', 5500000, 1),
(N'Шанхай', 24200000, 2),
(N'Мюнхен', 1450000, 3),
(N'Лион', 515695, 4),
(N'Александрия', 5200000, 5),
(N'Торонто', 2800000, 6),
(N'Сан-Паулу', 12200000, 7),
(N'Сидней', 5000000, 8),
(N'Осака', 2700000, 9),
(N'Йоханнесбург', 5600000, 10);

-- Обновление таблицы Countries для добавления IdCapitalCity
UPDATE Countries SET IdCapitalCity = 1 WHERE CountryName = N'Россия';
UPDATE Countries SET IdCapitalCity = 2 WHERE CountryName = N'Китай';
UPDATE Countries SET IdCapitalCity = 3 WHERE CountryName = N'Германия';
UPDATE Countries SET IdCapitalCity = 4 WHERE CountryName = N'Франция';
UPDATE Countries SET IdCapitalCity = 5 WHERE CountryName = N'Египет';
UPDATE Countries SET IdCapitalCity = 6 WHERE CountryName = N'Канада';
UPDATE Countries SET IdCapitalCity = 7 WHERE CountryName = N'Бразилия';
UPDATE Countries SET IdCapitalCity = 8 WHERE CountryName = N'Австралия';
UPDATE Countries SET IdCapitalCity = 9 WHERE CountryName = N'Япония';
UPDATE Countries SET IdCapitalCity = 10 WHERE CountryName = N'Южная Африка';
```