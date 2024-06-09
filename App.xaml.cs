using bazy1.ViewModels;
using bazy1.ViewModels.Admin;
using bazy1.ViewModels.Doctor;
using bazy1.Views;
using bazy1.Jobs;
using bazy1.Views.Admin;
using bazy1.Views.Doctor;
using bazy1.Views.Receptionist;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MvvmDialogs;
using System.Configuration;
using System.Data;
using System.Windows;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Threading.Tasks;

namespace bazy1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {
        }

        protected void ApplicationStartup(object sender, StartupEventArgs e)
        {
            var vm = new LoginViewModel();
            var loginView = new LoginView
            {
                DataContext = vm
            };
            vm.LoginCompleted += (s, e) =>
            {
                if (((UserEventArgs)e).UserType.Equals("lekarz"))
                {
                    var mainView = new DoctorView();
                    loginView.Close();
                    mainView.Show();
                }
                else if (((UserEventArgs)e).UserType.Equals("admin"))
                {
                    var mainView = new AdminView();
                    loginView.Close();
                    mainView.Show();
                }
                else if (((UserEventArgs)e).UserType.Equals("recepcjonista"))
                {
                    var mainView = new ReceptionistView();
                    loginView.Close(); mainView.Show();
                }
            };
            loginView.Show();

            StartReminderScheduler();
        }

        private async void StartReminderScheduler()
        {
            // Create a new scheduler factory
            StdSchedulerFactory factory = new StdSchedulerFactory();

            // Get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            // Define the job and tie it to our ReminderJob class
            IJobDetail job = JobBuilder.Create<ReminderJob>()
                .WithIdentity("ReminderJob", "group1")
                .Build();

            // Trigger the job to run now, and then every hour
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("ReminderTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(60)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}

