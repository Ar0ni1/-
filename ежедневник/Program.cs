
using System;
using System.Collections.Generic;

class DailyPlanner
{
    private List<Note> notes = new List<Note>();
    private int currentNoteIndex = 0;
    private bool selectingAction = false;
    private int selectedActionIndex = 0;

    public DailyPlanner()
    {
        notes.Add(new Note
        {
            DueDate = new DateTime(2023, 10, 21),
            Actions = new List<Action>
            {
                new Action { Description = "Исправить 2 по C#", DueDate = new DateTime(2023, 10, 21), NoteDescription = "Переделать практическую работу номер 2" },
                new Action { Description = "Погулять с друзьями", DueDate = new DateTime(2023, 10, 21), NoteDescription = "Встретиться с друзьями и поглуять в парке" }
            }
        });
        notes.Add(new Note
        {
            DueDate = new DateTime(2023, 10, 26),
            Actions = new List<Action>
            {
                new Action { Description = "Съездить в колледж", DueDate = new DateTime(2023, 10, 26), NoteDescription = "Поехать в колледж, чтобы исправить оценку по БД, и показать презентацию по КОМПЬЮТЕРНЫМ СЕТЯМ" },
                new Action { Description = "С Даньком на вписку", DueDate = new DateTime(2023, 10, 26), NoteDescription = "Встетиться с Даньком и поехать на концертYAETA'a" }
            }
        });
        notes.Add(new Note
        {
            DueDate = new DateTime(2023, 10, 30),
            Actions = new List<Action>
            {
                new Action { Description = "Сдача нормтива по физкультуре", DueDate = new DateTime(2023, 10, 30), NoteDescription = "Подготовиться к сдаче норматива по физкультуре" },
                new Action { Description = "Уехать со вписки", DueDate = new DateTime(2023, 10, 30), NoteDescription = "Поехать с Даньком домой" }
            }
        });
    }

    public void DisplayMenu()
    {
        Console.Clear();
        if (selectingAction)
        {
            DisplayActionSelection();
        }
        else
        {
            DisplayDateSelection();
        }
    }

    public void DisplayDateSelection()
    {
        Note note = notes[currentNoteIndex];
        Console.WriteLine($"Выберите дату: {note.DueDate.ToShortDateString()}");
        Console.WriteLine("Используйте стрелки влево и вправо для навигации. Нажмите 'Enter' для перехода к выбору действия. Нажмите 'Q' для выхода.");
    }

    public void DisplayActionSelection()
    {
        Note note = notes[currentNoteIndex];
        Console.WriteLine($"Действия на {note.DueDate.ToShortDateString()}:");
        for (int i = 0; i < note.Actions.Count; i++)
        {
            if (i == selectedActionIndex)
            {
                Console.Write("-> ");
            }
            else
            {
                Console.Write("   ");
            }
            Console.WriteLine(note.Actions[i].Description);
        }
        Console.WriteLine("Используйте стрелки вверх и вниз для выбора действия. Нажмите 'Enter' для отображения подробного описания действия. Нажмите 'Q' для выхода.");
    }

    public void DisplayActionDescription()
    {
        Note currentNote = notes[currentNoteIndex];
        if (selectedActionIndex >= 0 && selectedActionIndex < currentNote.Actions.Count)
        {
            Console.Clear();
            Console.WriteLine($"Действие на {currentNote.DueDate.ToShortDateString()}:");
            Action selectedAction = currentNote.Actions[selectedActionIndex];
            Console.WriteLine(selectedAction.Description);
            Console.WriteLine($"До {selectedAction.DueDate.ToShortDateString()}");
            Console.WriteLine("Описание: " + selectedAction.NoteDescription);
            Console.WriteLine("Введите 'Enter' для возврата к меню выбора действия.");
            Console.ReadKey();
        }
    }

    public void Run()
    {
        while (true)
        {
            DisplayMenu();
            ConsoleKey key = Console.ReadKey().Key;

            if (selectingAction)
            {
                HandleActionSelectionInput(key);
            }
            else
            {
                HandleDateSelectionInput(key);
            }
        }
    }

    public void HandleDateSelectionInput(ConsoleKey key)
    {
        if (key == ConsoleKey.LeftArrow)
        {
            ChangeDate(-1);
        }
        else if (key == ConsoleKey.RightArrow)
        {
            ChangeDate(1);
        }
        else if (key == ConsoleKey.Enter)
        {
            selectingAction = true;
        }
        else if (key == ConsoleKey.Q)
        {
            Environment.Exit(0);
        }
    }

    public void HandleActionSelectionInput(ConsoleKey key)
    {
        if (key == ConsoleKey.UpArrow)
        {
            ChangeAction(-1);
        }
        else if (key == ConsoleKey.DownArrow)
        {
            ChangeAction(1);
        }
        else if (key == ConsoleKey.Enter)
        {
            DisplayActionDescription();
        }
        else if (key == ConsoleKey.Q)
        {
            selectingAction = false;
        }
    }

    public void ChangeDate(int direction)
    {
        int newIndex = currentNoteIndex + direction;
        if (newIndex >= 0 && newIndex < notes.Count)
        {
            currentNoteIndex = newIndex;
        }
    }

    public void ChangeAction(int direction)
    {
        int newActionIndex = selectedActionIndex + direction;
        int actionsCount = notes[currentNoteIndex].Actions.Count;
        if (newActionIndex >= 0 && newActionIndex < actionsCount)
        {
            selectedActionIndex = newActionIndex;
        }
    }
    static void Main(string[] args)
    {
        DailyPlanner planner = new DailyPlanner();
        planner.Run();
    }
}
