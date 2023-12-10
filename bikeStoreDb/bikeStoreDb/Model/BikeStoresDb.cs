using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikeStoreDb.Model
{
    public class BikeStoresDb:DbContext
    {
        public BikeStoresDb() :base("BikeStoresConnectString") { }

        public DbSet<store> stores { get; set; }

        public DbSet<staff> staffs { get; set; }

        public DbSet<brand> brands { get; set; }

        public DbSet<category> categories { get; set; }

        public DbSet<product> products { get; set; }

        public DbSet<customer> customers { get; set; }

        public DbSet<order> orders { get; set; }

        public DbSet<stock> stocks {  get; set; }

        public DbSet<order_item> order_items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new staffConfiguration());
            modelBuilder.Configurations.Add(new productConfiguration());
            modelBuilder.Configurations.Add(new orderConfiguration());
            modelBuilder.Configurations.Add(new order_itemConfiguration());
            modelBuilder.Configurations.Add(new stockConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
