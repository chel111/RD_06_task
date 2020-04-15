using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTO
{
    public class ProductInOrderDTO
    {
        public ProductDTO ProductDTO { get; set; }
        public uint Count { get; set; }
    }
}
