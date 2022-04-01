using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportsStore21.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Informe a cidade")]
        public string City { get; set; }

        [Required(ErrorMessage ="Informe o estado")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage ="Informe o país")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }

        [BindNever]
        public bool Shipped { get; set; }

    }
}
