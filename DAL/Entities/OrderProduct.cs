using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using DAL.Interfaces;

namespace DAL.Entities
{
    [Table("orderproduct")]

    public class OrderProduct
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
    
        public int ProductId { get; set; }
        public Product Product { get; set; }

      
    }
}
