using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikeStoreDb.Model
{
    [Table("production.products")]
    public class product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int product_id { get; set; }


        [Required]
        [StringLength(255)]
        public string product_name { get; set;}


        [Required]
        [ForeignKey(nameof(Brand))]
        public int brand_id { get; set; }
        public virtual brand Brand { get; set; }


        [Required]
        [ForeignKey(nameof(Category))]
        public int category_id {  get; set; }   
        public virtual category Category { get; set; }


        [Required]
        public Int16 model_year { get; set; }


        [Required]
        public decimal list_price { get; set; }

        public virtual ICollection<order_item> Order_Items { get; set; }

        public virtual ICollection<stock> Stocks { get; set; }
    }

    internal class productConfiguration : EntityTypeConfiguration<product>
    {
        public productConfiguration()
        {
            Property(p => p.model_year)
                .HasColumnType("SMALLINT");

            Property(p => p.list_price)
                .HasPrecision(10, 2);

            HasRequired<brand>(p => p.Brand)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.brand_id)
                .WillCascadeOnDelete(true);

            HasRequired<category>(p=>p.Category)
                .WithMany(p=>p.Products)
                .HasForeignKey(p=>p.category_id)
                .WillCascadeOnDelete(true);
        }
    }
}
