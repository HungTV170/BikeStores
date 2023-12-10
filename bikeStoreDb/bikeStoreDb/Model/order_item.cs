using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikeStoreDb.Model
{
    [Table("sales.order_items")]
    public class order_item
    {

        [Key, ForeignKey(nameof(Order))]
        public int order_id { get; set; }
        public virtual order Order { get; set; }


        public int item_id { get; set; }


        [Required]
        [ForeignKey(nameof(Product))]
        public int product_id { get; set; }
        public virtual product Product { get; set; }


        [Required]
        public int quantity { get; set; }


        [Required]
        public decimal list_price { get; set; }


        [Required]
        [DefaultValue(0)]
        public decimal discount { get; set; }


    }

    internal class order_itemConfiguration : EntityTypeConfiguration<order_item>
    {
        public order_itemConfiguration()
        {
            Property(oi => oi.list_price)
                .HasPrecision(10, 2);

            Property(oi => oi.discount)
                .HasPrecision(4, 2);

            HasKey(oi => new {oi.order_id,oi.item_id});

            HasRequired<product>(oi => oi.Product)
                .WithMany(p => p.Order_Items)
                .HasForeignKey(oi => oi.product_id)
                .WillCascadeOnDelete(true);

            HasRequired<order>(oi => oi.Order)
                .WithMany(o => o.Order_item)
                .WillCascadeOnDelete(true);


        }
    }
}

