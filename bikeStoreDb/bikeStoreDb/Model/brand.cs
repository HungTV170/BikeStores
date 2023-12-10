using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikeStoreDb.Model
{
    [Table("production.brands")]
    public class brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int brand_id { get; set; }


        [Required]
        [StringLength(255)]
        public string brand_name { get; set; }

        public virtual ICollection<product> Products { get; set; }
    }
}
