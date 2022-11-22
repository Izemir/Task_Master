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

    /// <summary>
    /// сервис взаимодействия с БД, где сохраняются данные
    /// CRUD операции реализованы
    /// </summary>
    public static class TaskService
    {
        static SQLiteAsyncConnection db;

        public static async Task Init()
        {
            if (db != null)
                return;

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
                StatusId = (int)EnumTaskStatuses.opened
            };

            var id = await db.InsertAsync(task);
        }

        public static async Task<UserTask> ChangeStatus(int id, EnumTaskStatuses statusId)
        {
            await Init();
            var task = await db.Table<UserTask>()
                .FirstOrDefaultAsync(c => c.Id == id);
            task.StatusId = (int)statusId;
            await db.UpdateAsync(task);
            return task;
        }

        public static async Task<UserTask> GetTask(int id)
        {
            await Init();

            var task = await db.Table<UserTask>()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (task != null) task.Status = StatusService.GetStatus((EnumTaskStatuses)task.StatusId);

            return task;
        }

        public static async Task<IEnumerable<UserTask>> GetTasks()
        {
            await Init();

            var tasks = await db.Table<UserTask>().ToListAsync();
            foreach(var task in tasks)
            {
                task.Status = StatusService.GetStatus((EnumTaskStatuses)task.StatusId);
            }
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
            await db.UpdateAsync(task);
            var tasks = await db.Table<UserTask>().ToListAsync();
            return tasks;
        }
    }
}
