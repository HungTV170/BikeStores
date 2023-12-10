using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace bikeStoreDb.Model
{
    [Table("sales.staffs")]
    public class staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int staff_id {  get; set; }


        [MaxLength(50)]
        [Required]
        public string first_name {  get; set; }


        [MaxLength(50)]
        [Required]
        public string last_name { get; set;}


        [MaxLength(255)]
        [Required]
        [Index(IsUnique =true)]
        public string email { get; set; }


        [MaxLength(25)]
        public string phone { get; set; }


        [Required]
        public byte active { get; set; }


        [Required]
        [ForeignKey(nameof(Store))]
        public int store_id { get; set;}
        public virtual store Store { get; set; }
        

        public int? manager_id { get; set; }
        public virtual staff Manager { get; set; }
        //public virtual ICollection<staff> StaffMangagers { get; set; }

        public virtual ICollection<order> Orders { get; set; }
    }

    internal class staffConfiguration : EntityTypeConfiguration<staff>
    {
        public staffConfiguration()
        {
            HasRequired<store>(s => s.Store)
                .WithMany(s => s.Staffs)
                .HasForeignKey(s=>s.store_id)
                .WillCascadeOnDelete(true);


            HasOptional(c => c.Manager)
                .WithMany()
                .HasForeignKey(s => s.manager_id)
                .WillCascadeOnDelete(false);
                

            
        }
    }
}
