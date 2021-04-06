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
                DoDate = null,
                Done = false
            };

            string str = Console.ReadLine();
            switch (str)
            {
                case "Read":
                    OutPut(repository.Read());
                    break;
                case "Create":
                    repository.Create(newMyTask);
                    break;
                case "Update":
                    repository.Update(newMyTask);
                    break;
                case "Delete":
                    repository.Delete(7);
                    break;
                default:
                    break;
            }            
        }
    }
}
