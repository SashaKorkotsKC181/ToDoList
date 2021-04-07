using System;
using System.Collections.Generic;
using Npgsql;

namespace ToDoList
{
    class Program
    {
        static void OutPut(List<MyTask> tasks)
        {
            foreach (MyTask task in tasks)
            {
                Console.WriteLine(task);
            }
        }
        static void Main(string[] args)
        {
            Repository repository = new Repository("Host=127.0.0.1;Username=todolist_api;Password=secret;Database=todolist");

            MyTask newMyTask = new MyTask()
            {
                Id = 4,
                Title = "make todolist",
                Description = null,
                DoDate = new DateTime(2021,4,10),
                Done = false
            };

            string str = Console.ReadLine();
            switch (str)
            {
                case "1":
                    OutPut(repository.Read());
                    break;
                case "2":
                    repository.Create(newMyTask);
                    break;
                case "3":
                    repository.Update(newMyTask);
                    break;
                case "4":
                    repository.Delete(6);
                    break;
                default:
                    break;
            }            
        }
    }
}
