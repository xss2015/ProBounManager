using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProBounManager.Models
{
    class DBEmp : DbContext
    {
       
        public DBEmp()
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Emp> Emps { get; set; }
        public DbSet<PromoHistory> PromoHistories { get; set; }
        public DbSet<BouncHistory> BouncHistories { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<PrivilageMonth> PrivilageMonths { get; set; }
        public DbSet<PrivilageYear> PrivilageYears { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<PWD> PWDs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder ConnectionString = new SqlConnectionStringBuilder()
            {
                //DataSource = @"HASSAN\SQLEXPRESS",
                //InitialCatalog = "DbEmp",
                //IntegratedSecurity = true,

                //////DataSource = @"(localdb)\MSSQLLocalDB",
                //InitialCatalog = "EmpDBTest",
                //////IntegratedSecurity = true,
                ////AttachDBFilename = Path.GetDirectoryName(Application.) +@"EmpDb.mdf",
                //////AttachDBFilename = @"F:\EmpDB.mdf",
                ///
                DataSource = @"(localdb)\mssqllocaldb",
                //InitialCatalog = "MaterialDB",
                IntegratedSecurity = true,
                AttachDBFilename = Path.GetDirectoryName(Application.ExecutablePath) + @"\DbEmp.mdf"


            };
            optionsBuilder.UseSqlServer(ConnectionString.ToString());
        }




    }
}
