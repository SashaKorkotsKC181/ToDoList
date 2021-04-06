using System;

namespace ToDoList
{
    public class MyTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DoDate { get; set; }
        public bool Done { get; set; }

        public override string ToString()
        {
            return $"{Id} {Title} {Description} {DoDate} {Done}";
        }
    }
}