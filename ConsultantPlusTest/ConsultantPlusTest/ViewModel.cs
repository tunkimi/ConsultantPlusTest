using ConsultantPlusTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ConsultantPlusTest
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        DbContextOptions<AppDbContext> dbContextOptions = null;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public ViewModel()
        {
            var builder = new ConfigurationBuilder();
            var a = Directory.GetCurrentDirectory();
            builder.AddJsonFile("D:\\Projects\\ConsultantPlusTest\\DataExtracting\\appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DatabaseConnection");
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptions = optionsBuilder.UseSqlServer(connectionString).Options;
        }

        public ICommand ExitCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Application.Current.Shutdown();
                }, (obj) => true);
            }
        }
        public ICommand OpenedCompaniesCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    _navigation.Content = new OpenedCompaniesPage();
                }, (obj) => true);
            }
        }
        public ICommand ClosedCompaniesCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    _navigation.Content = new ClosedCompaniesPage();
                }, (obj) => true);
            }
        }
        public ICommand MainPageCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    _navigation.Content = new MainPage();
                }, (obj) => true);
            }
        }

        public static Frame _navigation = null;


        List<Company> _companies = null;
        List<Company> Companies
        {
            get
            {
                if (_companies == null)
                {
                    using (var db = new AppDbContext(dbContextOptions))
                    {
                        _companies = db.Companies.ToList<Company>();
                    }
                }
                return _companies;
            }
        }


        public List<Company> OpenedCompanies
        {
            get
            {
                return Companies.Where(c => c.Status == true).ToList();
            }
        }
        public List<Company> ClosedCompanies
        {
            get
            {
                return Companies.Where(c => c.Status == false).ToList();
            }
        }

    }
}
