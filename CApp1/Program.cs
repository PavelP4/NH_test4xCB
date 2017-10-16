using Eg.Core;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Reflection;

//[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace CApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            var nhConfig = new Configuration().Configure();

            nhConfig.AddAssembly(typeof(Product).Assembly);

            var sessionFactory = nhConfig.BuildSessionFactory();

            var update = new SchemaUpdate(nhConfig);
            update.Execute(false, true);

            using (var session = sessionFactory.OpenSession())
            {
                using (var tr = session.BeginTransaction())
                {
                    var prod = new Product() {                        
                        Name = "Pepsi4",
                        Description = "Beverage",
                        UnitPrice = 1.15M
                    };

                    session.Save(prod);

                    tr.Commit();
                }
            }
            

            Console.WriteLine("Finished!");
            Console.ReadLine();
        }
    }
}
