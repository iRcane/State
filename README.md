# State

Как это работает.
Есть два текстбокса, верхний отвечает за относительный путь файла, нижний - за текст внутри этого файла. 
Кнопка Save позволяет сохранить в указанный файл содержимое нижнего текстбокса.
В зависимости от того, введены ли данные в текстбоксы, существуют следующие состояния:
* InitialState
  - CloseHandler закрывает форму.
  - Save выводит: "Can not save changes as no file name entered".
* EmptyState
  - CloseHandler выводит: "Changes might not been saved. Save changes?". По выбору пользователя вызывает Save и закрывает форму.
  - Save выводит: "Are you sure you want to save an empty file?". По выбору пользователя сохраняет пустой файл.
* NoNameState
  - CloseHandler выводит: "Closing will not save changes as no file name entered. Continue closing?". По выбору пользователя закрывает форму.
  - Save выводит: "Can not save changes as no file name entered".
* ChangedState
  - CloseHandler выводит: "Changes might not been saved. Save changes?". По выбору пользователя вызывает Save и закрывает форму.
  - Save сохраняет измененный файл.
