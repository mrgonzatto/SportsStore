using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore21.Models
{
    public class Product
    {

        public long ProductID { get; set; }
        

        [Required(ErrorMessage = "Informe o nome.")]
        public string Name { get; set; }
        

        [Required(ErrorMessage = "Informe a descrição.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Informe o preço")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        

        [Required(ErrorMessage = "Informe a categoria")]
        public string Category { get; set; }
    }
}
