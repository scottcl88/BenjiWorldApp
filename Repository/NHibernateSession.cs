using NHibernate;
using NHibernate.Cfg;
using System;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Models;
using Repository.Mappings;
using NHibernate.Driver;
using NHibernate.Dialect;
using System.Data;
using System.Reflection;

namespace Repository
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            //string configurationPath = Path.GetFullPath(currentDir + @"\Bin\Models\hibernate.cfg.xml");
            //configuration.Configure(configurationPath);
            //configurationPath
            //string carConfigurationFile = Path.GetFullPath(currentDir + @"\Bin\Mappings\Car.hbm.xml");
            //configuration.AddFile(carConfigurationFile);

            //string repairServicesConfigurationFile = Path.GetFullPath(currentDir + @"\Bin\Mappings\RepairServices.hbm.xml");
            //configuration.AddFile(repairServicesConfigurationFile);
            //string repairServicesTypesConfigurationFile = Path.GetFullPath(currentDir + @"\Bin\Mappings\RepairServiceTypes.hbm.xml");
            //configuration.AddFile(repairServicesTypesConfigurationFile);
            //string repairConfigurationFile = Path.GetFullPath(currentDir + @"\Bin\Mappings\Repair.hbm.xml");
            //configuration.AddFile(repairConfigurationFile);
            //string gasConfigurationFile = Path.GetFullPath(currentDir + @"\Bin\Mappings\Gas.hbm.xml");
            //configuration.AddFile(gasConfigurationFile);
            //string userConfigurationFile = Path.GetFullPath(currentDir + @"Bin\Mappings\Users.hbm.xml");//@"..\Repository\Mappings\Users.hbm.xml"
            //configuration.AddFile(userConfigurationFile);
            //garage.c2zetmoolnra.us-west-2.rds.amazonaws.com,1433;Initial Catalog=elements;User ID=admin;Password=xb!0YLor9gC0nRK


            configuration.DataBaseIntegration(x => {
                x.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2008Dialect>();
                x.IsolationLevel = IsolationLevel.RepeatableRead;
                x.Timeout = 10;
                x.BatchSize = 10;
            });
            //configuration.SessionFactory().GenerateStatistics();
            //configuration.AddAssembly(Assembly.GetExecutingAssembly());

            var dbConn = MsSqlConfiguration.MsSql2008
              .ConnectionString("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;").ShowSql();
            //.Username("admin")
            //.Password("xb!0YLor9gC0nRK"));
            var sessionFactory = Fluently.Configure()
                .Database(dbConn)
                .Mappings(m =>
                {
                    m.FluentMappings
                        .AddFromAssemblyOf<DogMap>();
                    //m.FluentMappings
                    //    .AddFromAssemblyOf<GasMap>();
                })
                    .BuildSessionFactory();
            //ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}