# Домашнее задание: Работа с Git

## Подготовка

### 1. Форк репозитория

1. Откройте [репозиторий проекта на GitHub](https://github.com/sheeesh3rrr/Homework_6_Git_Practice_TaskManager)
2. Нажмите кнопку **Fork** в правом верхнем углу страницы.
3. В появившемся окне убедитесь, что выбран ваш аккаунт, и нажмите **Create fork**.
4. **Важно:** снимите галочку "Copy the `master` branch only".

### 2. Клонирование репозитория

После создания форка склонируйте его на свой компьютер.

Создайте в файловой системе папку, где вы хотите разместить локальный репозиторий, после чего используйте команды терминала:

```bash
cd путь/к/созданной/папке
git clone <ссылка на ваш форк>
cd TaskTracker
```

Ссылку на форк можно скопировать, нажав зелёную кнопку **Code** на странице вашего форка.

### 3. Добавление ассистента в коллабораторы

1. На странице вашего форка на GitHub перейдите в **Settings** → **Collaborators**.
2. Нажмите **Add people**.
3. Введите никнейм вашего ассистента:
   - БРИЦП 261: `mporo-prog`
   - БРИЦП 262: `sheeesh3rrr`
   - БРИЦП 263: `FillRomanko`
4. Нажмите **Add**.

### 4. Проверьте ветки

После клонирования выполните команду, чтобы увидеть все ветки:

```bash
git branch -a
```

Вы должны увидеть ветку `master` и три удалённые ветки:
- `origin/feature/priority-filter`
- `origin/feature/task-edit`
- `origin/feature/statistics`

Переключитесь на каждую ветку, чтобы они появились локально:

```bash
git checkout feature/priority-filter
git checkout feature/task-edit
git checkout feature/statistics
git checkout master
```

---

## Задание 1: Добавление нового статуса задачи (работа в ветке `feature/task-edit`)

В ветке `feature/task-edit` ведётся разработка функции редактирования задач. В этой ветке уже добавлена возможность менять приоритет задачи. Вам нужно добавить новый статус задачи.

### 1.1. Переключитесь на ветку `feature/task-edit`

Переключитесь на ветку, в которой ведётся разработка функции редактирования.

### 1.2. Добавьте новый статус `Cancelled`

Откройте файл `TaskTracker/Models/TaskStatus.cs` и добавьте новое значение в enum:

```csharp
namespace TaskTracker.Models
{
    public enum TaskStatus
    {
        New,
        InProgress,
        Done,
        Cancelled    // <-- добавьте эту строку
    }
}
```

Это нужно, чтобы пользователь мог отменять задачи, а не только завершать их.

**После изменения сделайте коммит с осознанным сообщением**

### 1.3. Обновите меню смены статуса

Теперь нужно обновить интерфейс, чтобы новый статус можно было выбрать. Откройте файл `TaskTracker/UI/ConsoleUI.cs`, найдите метод `ChangeTaskStatus()` и обновите вывод вариантов статуса:

Замените:
```csharp
Console.WriteLine("1. New");
Console.WriteLine("2. InProgress");
Console.WriteLine("3. Done");
Console.Write("Выберите: ");
```

На:
```csharp
Console.WriteLine("1. New");
Console.WriteLine("2. InProgress");
Console.WriteLine("3. Done");
Console.WriteLine("4. Cancelled");
Console.Write("Выберите: ");
```

И в `switch` ниже добавьте обработку нового варианта:

```csharp
case 4: taskService.ChangeStatus(id, TaskStatus.Cancelled); break;
```

**После изменения сделайте коммит с осознанным сообщением**

---

## Задание 2: Влитие ветки `feature/task-edit` в `master`

Ветка `feature/task-edit` готова. Влейте ее в `master`, используя команды терминала или gui расширения.

Если Git попросит написать сообщение для merge-коммита, оставьте предложенное по умолчанию.

---

## Задание 3: Работа со статистикой (ребейз ветки `feature/statistics`)

В ветке `feature/statistics` ведётся разработка функции показа статистики по задачам. Эта ветка была создана от старой версии `master`, поэтому перед продолжением работы нужно обновить её.

### 3.1. Работа с веткой

Переключитесь на ветку `feature/statistics` и выполните ребейз ветки на текущее состояние `master`. Это перенесёт коммиты ветки `feature/statistics` поверх последних изменений из `master`.

Если при ребейзе возникнут конфликты, разрешите их — оставьте оба изменения (и из `master`, и из `feature/statistics`), затем продолжите ребейз.

### 3.3. Добавьте подсчёт отменённых задач

После ребейза ветка `feature/statistics` содержит весь актуальный код. Теперь добавьте подсчёт задач с новым статусом `Cancelled`.

Откройте файл `TaskTracker/Services/TaskService.cs` и добавьте новый метод после `GetTaskCount()`:

```csharp
public string GetStatistics()
{
    int total = tasks.Count;
    int newCount = GetTasksByStatus(TaskStatus.New).Count;
    int inProgressCount = GetTasksByStatus(TaskStatus.InProgress).Count;
    int doneCount = GetTasksByStatus(TaskStatus.Done).Count;
    int cancelledCount = GetTasksByStatus(TaskStatus.Cancelled).Count;

    return $"Всего: {total} | Новых: {newCount} | В работе: {inProgressCount} | Завершено: {doneCount} | Отменено: {cancelledCount}";
}
```

**После изменения сделайте коммит с осознанным сообщением**

### 3.4. Добавьте пункт «Статистика» в меню

Откройте файл `TaskTracker/UI/ConsoleUI.cs` и добавьте новый пункт в меню.

В методе `Run()`, в блоке с `useArrowMenu`, обновите вызов `Menu.ProcessArrowMenu`:

```csharp
choice = Menu.ProcessArrowMenu(
    "=== TaskTracker ===",
    "Используйте стрелки для навигации, Enter для выбора",
    "Показать все задачи",
    "Добавить задачу",
    "Изменить статус задачи",
    "Удалить задачу",
    "Статистика",           // <-- новый пункт
    "Сохранить и выйти"
);
```

В простом меню (метод `ShowSimpleMenu`) добавьте строку перед «Сохранить и выйти»:

```csharp
Console.WriteLine("5. Статистика");
Console.WriteLine("6. Сохранить и выйти");  // было 5, стало 6
```

Добавьте обработку нового пункта в `switch` в методе `Run()`:

```csharp
case 5:
    ShowStatistics();
    break;
case 6:                    // было 5, стало 6
    storage.SaveTasks(taskService.GetAllTasks());
    running = false;
    Console.WriteLine("Задачи сохранены. До свидания!");
    break;
```

И добавьте новый метод `ShowStatistics()` в класс `ConsoleUI`:

```csharp
private void ShowStatistics()
{
    Console.WriteLine();
    Console.WriteLine("--- Статистика ---");
    Console.WriteLine(taskService.GetStatistics());

    if (useArrowMenu)
    {
        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}
```

**После изменения сделайте коммит с осознанным сообщением**

---

## Задание 4: Влитие ветки `feature/statistics` в `master`

Влейте ветку `feature/statistics` в `master`.

**При слиянии возникнет конфликт** в файле `ConsoleUI.cs`, потому что и ветка `feature/task-edit`, и ветка `feature/statistics` изменяли меню.

Ваша задача — оставить **оба** изменения. Итоговый код должен содержать и статус `Cancelled` в меню смены статуса, и пункт «Статистика» в главном меню. Удалите маркеры конфликта, убедитесь, что код выглядит правильно.

---

## Задание 5: Фильтрация по приоритету (работа в ветке `feature/priority-filter`)

### 5.1. Переключитесь на ветку `feature/priority-filter`

Перейдите на ветку `feature/priority-filter`. В этой ветке уже начата разработка фильтрации задач по приоритету.

### 5.2. Добавьте метод фильтрации

Откройте файл `TaskTracker/Services/TaskService.cs` и добавьте метод:

```csharp
public List<TaskItem> GetTasksByPriority(TaskPriority priority)
{
    List<TaskItem> result = new List<TaskItem>();
    for (int i = 0; i < tasks.Count; i++)
    {
        if (tasks[i].Priority == priority)
        {
            result.Add(tasks[i]);
        }
    }
    return result;
}
```

**После изменения сделайте коммит с осознанным сообщением**

### 5.3. Влейте `feature/priority-filter` в `master`

Выполните влитие ветки в `master`.

Если возникнут конфликты — разрешите их аналогично предыдущему заданию: оставьте все изменения из обеих веток.

---

## Задание 6: Переключение на Production-режим

Сейчас приложение работает в режиме `Development` (простое текстовое меню).

**Требование:** переключите приложение на режим `Production`, чтобы активировалось стрелочное меню.

**Ограничение:** коммитить напрямую в ветку `master` запрещено.

**После выполнения сделайте коммит.**

---

## Финальные шаги

### Протестируйте приложение

Убедитесь, что вы на ветке `master` и запустите проект:

```bash
dotnet run --project TaskTracker
```

Поиграйтесь с приложением! Попробуйте:
- Добавить несколько задач
- Изменить статусы
- Посмотреть статистику
- Сохранить и перезапустить — задачи должны сохраниться

### Отправьте на GitHub

Отправьте все ваши изменения на удалённый сервер (ваш форк):

```bash
git push origin --all
```

Убедитесь, что ваш ассистент добавлен в коллабораторы (см. шаг «Подготовка», пункт 3).
