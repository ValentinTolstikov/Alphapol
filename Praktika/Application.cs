using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Praktika.ViewModels;
using Persistance;
using Domain.Interfaces;
using Domain.Models;
using Application;

namespace Praktika
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            // создаем хост приложения
            var host = Host.CreateDefaultBuilder()
                // внедряем сервисы
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainWindowVM>();
                    services.AddDbContext<CUsersUserSourceReposPraktikaPersistanceDbMdfContext>();
                    services.AddSingleton<IRepository<User>, GenericRepository<User>>();
                    services.AddSingleton<IUserService, UserService>();
                })
                .Build();
            // получаем сервис - объект класса App
            var app = host.Services.GetService<App>();
            // запускаем приложения
            app?.Run();
        }
    }
}
