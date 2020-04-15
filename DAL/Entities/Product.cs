using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Interfaces;

namespace DAL.Entities
{
    [Table("products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
