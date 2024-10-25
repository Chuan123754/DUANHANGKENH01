using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ClientViews.Models
{
    public class Product_variants_wishlist
    {
        [Key]
        public long Id { get; set; }
        public long Product_variants_id { get; set; }
        public long Wishlist_id { get; set; }
        [ForeignKey("Product_variants_id")]
        [JsonIgnore]
        public virtual Product_variants? Product_Variants { get; set; }
        [ForeignKey("Wishlist_id")]
        [JsonIgnore]
        public virtual Wishlist? Wishlist {  get; set; }

    }
}
