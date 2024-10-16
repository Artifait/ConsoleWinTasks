# FrameWork: ConsoleWin
## Documentation of FrameWork

### Класс - зачем он:
* Application - НАДО
* WindowsHandler - Для менеджмента/хранения используемых окон + добавления, оповещяющих пользователя окон - AddErroreWindow(string[] msgs, bool isFatal = false), AddInfoWindow(string[] msgs)
* WindowTools - Туда вынесена вся логика, которая используется всеми окнами 
* WindowDisplay - Там все Данные + Логика, для правльной отрисовки в консоли
* MatrixFormater - там проясходят проектирование матрицы окна, для отображения
* TV - Для отрисовки виджетов, на данный момент только таблиц 
* WinInfo, WinErrore - окна оповещений пользователя
* WinStart - в это окно пользователь попадает при запуске
