using System;
using System.Collections.Generic;
using Npgsql;


namespace ToDoList
{
    internal class Repository
    {
        private NpgsqlConnection conn;
        //private int lastId;

        public Repository(string connString)
        {
            conn = new NpgsqlConnection(connString);
            conn.Open();            
        }
        public List<MyTask> Read()
        {
            List<MyTask> listOfTasks = new List<MyTask>();
            using (var cmd = new NpgsqlCommand("SELECT id, title, description, do_date, done FROM items", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        MyTask newMyTask = new MyTask()
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            DoDate = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                            Done = reader.GetBoolean(4)
                        };

                        listOfTasks.Add(newMyTask);

                    }

                }
            }
            return listOfTasks;
        }

        

        public MyTask ReadById(int id)
        {
            MyTask newMyTask = new MyTask();

            using (var cmd = new NpgsqlCommand("SELECT id, title, description, do_date, done FROM items where id=@id", conn))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        newMyTask = new MyTask()
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            DoDate = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                            Done = reader.GetBoolean(4)
                        };
                    }

                }
            }
            return newMyTask;
        }
        public void Create(MyTask newTask)
        {


            var insertCmd = new NpgsqlCommand("INSERT INTO items(title, description, do_date, done) VALUES (@title, @description, @do_date, false)", conn);
            insertCmd.Parameters.AddWithValue("title", newTask.Title);
            insertCmd.Parameters.AddWithValue("description", newTask.Description ?? "");
            insertCmd.Parameters.AddWithValue("do_date", NpgsqlTypes.NpgsqlDbType.Date, (object)newTask.DoDate ?? DBNull.Value);
            insertCmd.ExecuteNonQuery();
        }
        public void Update(MyTask newTask)
        {
            MyTask oldTask = ReadById(newTask.Id);

            var insertCmd = new NpgsqlCommand("update items set title=@title, description=@description, do_date=@do_date, done=@done where id=@id", conn);
            insertCmd.Parameters.AddWithValue("title", newTask.Title == null ? oldTask.Title : newTask.Title);
            insertCmd.Parameters.AddWithValue("description", newTask.Description == null ? oldTask.Description ?? "" : newTask.Description);
            insertCmd.Parameters.AddWithValue("do_date", NpgsqlTypes.NpgsqlDbType.Date, newTask.DoDate == null ? (object)oldTask.DoDate ?? DBNull.Value : newTask.DoDate);
            insertCmd.Parameters.AddWithValue("done", newTask.Done == false ? oldTask.Done : newTask.Done);
            insertCmd.Parameters.AddWithValue("id", newTask.Id);
            insertCmd.ExecuteNonQuery();


        }
        public void Delete(int id)
        {
            var insertCmd = new NpgsqlCommand("delete from items where id=@id", conn);
            insertCmd.Parameters.AddWithValue("id", id);
            insertCmd.ExecuteNonQuery();
        }
    }
}