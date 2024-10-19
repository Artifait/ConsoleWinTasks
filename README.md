
# Task Info

## For DB
```
INSERT INTO Game (Name, StuleGame, ReleaseDate, GameMode, CopiesSold, Studio)
VALUES 
('The Last of Us', 'Action-adventure', '2013-06-14', 'Single-player', 17000000, 'Naughty Dog'),
('Red Dead Redemption 2', 'Action-adventure', '2018-10-26', 'Single-player/Multiplayer', 50000000, 'Rockstar Games'),
('Cyberpunk 2077', 'RPG', '2020-12-10', 'Single-player', 20000000, 'CD Projekt'),
('Halo Infinite', 'First-person shooter', '2021-12-08', 'Single-player/Multiplayer', 10000000, '343 Industries'),
('Horizon Zero Dawn', 'Action RPG', '2017-02-28', 'Single-player', 10000000, 'Guerrilla Games'),
('The Witcher 3: Wild Hunt', 'RPG', '2015-05-19', 'Single-player', 40000000, 'CD Projekt'),
('GTA V', 'Action-adventure', '2013-09-17', 'Single-player/Multiplayer', 180000000, 'Rockstar Games'),
('Minecraft', 'Sandbox', '2011-11-18', 'Single-player/Multiplayer', 238000000, 'Mojang Studios'),
('FIFA 21', 'Sports', '2020-10-09', 'Multiplayer', 35000000, 'EA Sports'),
('Call of Duty: Modern Warfare', 'First-person shooter', '2019-10-25', 'Single-player/Multiplayer', 30000000, 'Infinity Ward');
```


# FrameWork: ConsoleWin
## Documentation of FrameWork

### Класс - зачем он:
* WindowsHandler - Для менеджмента/хранения используемых окон + добавления, оповещяющих пользователя окон - AddErroreWindow(string[] msgs, bool isFatal = false), AddInfoWindow(string[] msgs)
* WindowTools - Туда вынесена вся логика, которая используется всеми окнами 
* WindowDisplay - Там все Данные + Логика, для правльной отрисовки в консоли
* MatrixFormater - там проясходят проектирование матрицы окна, для отображения
* TV - Для отрисовки виджетов, на данный момент только таблиц 
* WinInfo, WinErrore - окна оповещений пользователя
* WinStart - в это окно пользователь попадает при запуске

## Обнова
### WinTemplate 
* CwTask - шаблон окна для выполнения заданий 

### T4 Templates and MsBuild
* CwTaskTemp.tt - используется для генерации окна с заданием номера выполняемого задания и перечисления методов:
```
<#
    // <Конфиг>
    int TaskNumber = 3;
    var Methods = new string[] { "CreateGame", "UpdateGame", "DeleteGame"};
    //</Конфиг>

    string fileName = $"CwTask{TaskNumber}.cs";

    string outputFilePath = Path.Combine(".", "GeneratedFileName.txt");
    File.WriteAllText(outputFilePath, fileName);
#>
```
После того как был сгенерирован класс(+ GeneratedFileName.txt), его перехватывает MsBuild(.csproj) и начинает прогонять по инструкциям:
1) Чтение имени из GeneratedFileName.txt, которое должен иметь сгенереный класс:
```
<ReadLinesFromFile File="GeneratedFileName.txt">
    <Output TaskParameter="Lines" ItemName="GeneratedFileName" />
</ReadLinesFromFile>
```
2) Построение пути(Новое распололжение + имя)
```
<PropertyGroup>
	<NewFilePath>$(FilesPath)\@(GeneratedFileName)</NewFilePath>
</PropertyGroup> 		
```
3) Проверка что по этому пути нету такого же сгенерироного файла
```
<ItemGroup>
	<ExistingFileInPath Include="$(NewFilePath)" Condition="Exists('$(NewFilePath)')" />
</ItemGroup>
```
4.1) Если файл с новым именем уже существует в папке, удаляем тк можешь затереть уже созданые изменения
4.2) Если файл с новым именем не существует, переименовываем и перемещаем файл
5) Удаляем более не нужный файл: GeneratedFileName.txt
