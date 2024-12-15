using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Praktika.ViewModels;
using Persistance;
using Domain.Interfaces;
using Domain.Models;
using Application;
using Praktika.Views;

namespace Praktika
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainWindowVM>();
                    services.AddTransient<UserMainView>();
                    services.AddSingleton<UserMainVM>();
                    services.AddDbContext<CUsersUserSourceReposPraktikaPersistanceDbMdfContext>();
                    services.AddSingleton<IRepository<User>, GenericRepository<User>>();
                    services.AddSingleton<IUserService, UserService>();
                })
                .Build();
            
            var app = host.Services.GetService<App>();
            
            app?.Run();
        }
    }
}
