
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
