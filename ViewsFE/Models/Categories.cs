
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ViewsFE.Models
{
    [Table("categories")]
    public partial class Categories
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        public string? Short_title { get; set; }
        [StringLength(255)]
        public string? Title { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Slug { get; set; }
        [StringLength(255)]
        public string? Type { get; set; }
        public long Parent_id { get; set; }
        public bool? Deleted { get; set; }
        public int Dept { get; set; }
        public string? Description { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime? Deleted_at { get; set; }
    }
}
