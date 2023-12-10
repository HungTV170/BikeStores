using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikeStoreDb.Model
{
    [Table("sales.customers")]
    public class customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int customer_id { get; set; }


        [Required]
        [StringLength(255)]
        public string first_name { get; set; }


        [Required]
        [StringLength(255)]
        public string last_name { get; set; }


        [StringLength(25)]
        public string phone { get; set; }


        [Required]
        [StringLength(255)]
        public string email { get; set; }


        [StringLength(255)]
        public string street { get; set; }


        [StringLength(50)]
        public string city { get; set; }


        [StringLength(25)]
        public string state { get; set; }


        [StringLength(5)]
        public string zip_code { get; set; }

        public virtual ICollection<order> Orders { get; set; }
    }
}
