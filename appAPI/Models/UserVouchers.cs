﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace appAPI.Models
{
    public class UserVouchers
    {
        [Key]
        public long Id { get; set; }

        public long UserId { get; set; }

        public long VoucherId { get; set; }

        public bool IsApplied { get; set; } = false;

        public DateTime? AppliedAt { get; set; }
        public DateTime Create_at { get; set; }

        public DateTime Update_at { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public virtual Users? Users { get; set; }
        [ForeignKey("VoucherId")]
        public virtual Vouchers? Vouchers { get; set; }
    }
}
