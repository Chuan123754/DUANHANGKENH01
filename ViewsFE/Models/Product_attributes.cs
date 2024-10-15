using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ViewsFE.Models
{
    [Table("product_attributes")]
    public class Product_attributes
    {
        [Key]
        public long Id { get; set; }
        public long Product_Variants_Id { get; set; }
        public long Attribute_Id { get; set; }
        [ForeignKey("Product_Variants_Id")]
        [JsonIgnore]
        public virtual Product_variants? Product_Variants { get; set; }
        [ForeignKey("Attribute_Id")]
        [JsonIgnore]
        public virtual Attributes? Attributes { get; set; }
    }
}
