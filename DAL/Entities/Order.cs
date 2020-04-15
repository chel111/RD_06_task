using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using DAL.Interfaces;

namespace DAL.Entities
{
    [Table("orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        
    }
}
