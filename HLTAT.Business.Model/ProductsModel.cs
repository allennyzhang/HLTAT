using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HLTAT.Business.Model
{
    [Table(name: "Products", Schema = "Main")]
    public class ProductsModel
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime Timestamp { get; set; }
        [Required(ErrorMessage = "Product is required.")]
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        public int? Quantity { get; set; }
        [Range(0, 99999999999999.9999)]
        [Column(TypeName = "decimal(18, 4)")]
        public double? Sale_Amount { get; set; }
    }
}
