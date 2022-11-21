using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Task_Master.Models;
using Xamarin.Essentials;

namespace Task_Master.Services.TaskService
{
    public static class TaskService
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<UserTask>();
        }

        public static async Task AddTask(string name, string description)
        {
            await Init();
            var task = new UserTask()
            {
                Name = name,
                Description = description,
                CreateDate = DateTime.Now,
                Status = StatusService.GetStatus(EnumTaskStatuses.opened)
            };

            var id = await db.InsertAsync(task);
        }

        public static async Task<UserTask> ChangeStatus(int id, EnumTaskStatuses statusId)
        {
            await Init();
            var task = await db.Table<UserTask>()
                .FirstOrDefaultAsync(c => c.Id == id);
            task.Status = StatusService.GetStatus(statusId);
            return task;
        }

        public static async Task<UserTask> GetTask(int id)
        {
            await Init();

            var task = await db.Table<UserTask>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return task;
        }

        public static async Task<IEnumerable<UserTask>> GetTasks()
        {
            await Init();

            var tasks = await db.Table<UserTask>().ToListAsync();
            return tasks;
        }

        public static async Task RemoveTask(int id)
        {
            await Init();

            await db.DeleteAsync<UserTask>(id);
        }

        public static async Task<IEnumerable<UserTask>> UpdateTask(int id, string name, string description)
        {
            await Init();
            var task = await db.Table<UserTask>()
                .FirstOrDefaultAsync(c => c.Id == id);
            task.Name = name;
            task.Description = description;
            var tasks = await db.Table<UserTask>().ToListAsync();
            return tasks;
        }
    }
}
