using bikeStoreDb.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace bikeStoreDb
{
    public class bikeStoreDbService : BikeStoresDb, IDatabase
    {
        public DbSet<T> GetEntities<T>() where T : class
        {
            Type BikeStoreType = typeof(BikeStoresDb);
            PropertyInfo[] properties = BikeStoreType.GetProperties();
            foreach (var item in properties)
            {
                if (item.PropertyType == typeof(DbSet<T>))
                {
                    return (DbSet<T>)item.GetValue(this);
                }
            }
            throw new ArgumentException("khong co kieu phu hop.");


        }
    }
}
