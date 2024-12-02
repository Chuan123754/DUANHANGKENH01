using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace appAPI.Models
{
    
    public class Payment
    {
        [Key]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<Orders>? Orders { get; set; } = new List<Orders>();

    }

    //public class PaymentInformationModel
    //{
    //    public string OrderType { get; set; }
    //    public double Amount { get; set; }
    //    public string OrderDescription { get; set; }
    //    public string Name { get; set; }
    //}

    //public class PaymentResponseModel
    //{
    //    public string OrderDescription { get; set; }
    //    public string TransactionId { get; set; }
    //    public string OrderId { get; set; }
    //    public string PaymentMethod { get; set; }
    //    public string PaymentId { get; set; }
    //    public bool Success { get; set; }
    //    public string Token { get; set; }
    //    public string VnPayResponseCode { get; set; }
    //}

}
