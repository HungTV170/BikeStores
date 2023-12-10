using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikeStoreDb.Model
{
    [Table("production.stocks")]
    public class stock
    {
        [ForeignKey(nameof(Store))]
        public int store_id {  get; set; }

        public store Store { get; set; }

        [ForeignKey(nameof(Product))]
        public int product_id { get; set; }

        public product Product { get; set; }

        public int quantity { get; set; }
    }

    internal class stockConfiguration : EntityTypeConfiguration<stock>
    {
        public stockConfiguration()
        {
            HasKey(s => new { s.product_id, s.store_id });

            HasRequired<store>(s => s.Store)
                .WithMany(s => s.Stocks)
                .WillCascadeOnDelete(true);

            HasRequired<product>(s => s.Product)
                .WithMany(s=>s.Stocks)
                .WillCascadeOnDelete(true);
        }
    }
}
