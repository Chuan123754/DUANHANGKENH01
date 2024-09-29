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
    [Index("Attribute_Id", Name = "IX_product_attributes_attribute_id")]
    [Index("Product_Id", Name = "IX_product_attributes_product_id")]
    public class Product_attributes
    {
        [Key]
        public long Id { get; set; }
        public long Product_Id { get; set; }
        public long Attribute_Id { get; set; }
        [ForeignKey("Product_Id")]
        [JsonIgnore]
        public virtual Attributes? Attribute { get; set; }
        [ForeignKey("Attribute_Id")]
        [JsonIgnore]
        public virtual Products? Product { get; set; }
    }
}
