using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikeStoreDb.Model
{
    [Table("sales.stores")]
    public class store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int store_id { get; set; }


        [Required]
        [StringLength(255)]
        public string store_name { get; set; }


        [StringLength(25)]
        public string phone { get; set;}


        [StringLength(255)]
        public string email { get; set;}


        [StringLength(255)]
        public string street { get; set;}


        [StringLength(255)]
        public string city { get; set;}


        [StringLength(10)]
        public string state { get; set;}


        [StringLength(5)]
        public string zip_code { get; set;}


        public virtual ICollection<staff> Staffs { get; set;}
        public virtual ICollection<order> Orders { get; set;}

        public virtual ICollection<stock> Stocks { get; set;}
    }
}
