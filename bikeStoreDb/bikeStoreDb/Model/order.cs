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
    [Table("sales.orders")]
    public class order
    {
        [Key/*, ForeignKey(nameof(Order_item))*/]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public int order_id { get; set; }
        public virtual ICollection<order_item> Order_item { get; set; }


        [ForeignKey(nameof(Customer))]
        public int customer_id { get; set; }
        public virtual customer Customer { get; set; }

        //add order_status
        [Required]
        public byte order_status { get; set; }

        
        [Required]
        [Column(TypeName = "Date")]
        public DateTime order_date { get; set; }

        
        [Required]
        [Column(TypeName = "Date")]
        public DateTime required_date { get; set; }

        //drop required
        
        [Column(TypeName = "Date")]
        public DateTime? shipped_date { get; set; }


        [Required]
        [ForeignKey(nameof(Store))]
        public int store_id { get; set;}
        public virtual store Store { get; set; }


        [Required]
        [ForeignKey(nameof(Staff))]
        public int staff_id { get; set; }
        public virtual staff Staff { get; set; }

        

    }

    internal class orderConfiguration : EntityTypeConfiguration<order>
    {
        public orderConfiguration()
        {
            HasRequired<customer>(o => o.Customer)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.customer_id)
                .WillCascadeOnDelete(true);

            HasRequired<store>(o => o.Store)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.store_id)
                .WillCascadeOnDelete(true);

            HasRequired<staff>(o => o.Staff)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.staff_id)
                .WillCascadeOnDelete(false);


            //HasRequired<order_item>(oi => oi.Order_item)
            //    .WithRequiredPrincipal(o => o.Order)
            //    .WillCascadeOnDelete(true);

        }
    }
}
