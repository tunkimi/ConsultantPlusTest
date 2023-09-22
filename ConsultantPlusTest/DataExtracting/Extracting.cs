

using dataExtracting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IronXL;
using Microsoft.EntityFrameworkCore.Storage;

namespace dataExtracting
{
    class Extracting
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            var a = Directory.GetCurrentDirectory();
            builder.AddJsonFile("D:\\Projects\\ConsultantPlusTest\\DataExtracting\\appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DatabaseConnection");
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var dbContextOptions = optionsBuilder.UseSqlServer(connectionString).Options;

            var file = @"D:\Projects\github\ConsultantPlusTest\ConsultantPlusTest\DataExtracting\Тест.xlsx";

            WorkBook wb = WorkBook.Load(file);
            WorkSheet ws = wb.WorkSheets[0];

            List<Company> companyList = new List<Company>();

            for (int i = 1; i < 108; i++)
            {
                var row = ws.Rows[i];
                var inn = (string)row.Columns[0].Value;
                var name = (string)row.Columns[1].Value;
                var city = (string)row.Columns[2].Value;
                var region = (string)row.Columns[3].Value;
                var status = (bool)row.Columns[4].Value;
                var comp = new Company { INN = inn, Name = name, City = city, Region = region, Status = status };
                companyList.Add(comp);
            }

            using (var db = new AppDbContext(dbContextOptions))
            {
                db.Companies.AddRange(companyList);
                db.SaveChanges();
            }

        }
    }
}