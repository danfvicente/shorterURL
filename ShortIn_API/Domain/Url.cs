using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShortIn_API.Domain
{
    [Table("Url")]
    public class Url
    {
        [Key]
        public int urlId { get; set; }

        [Required]
        public string fullUrl { get; set; }

        public string shortUrl { get; set; }
    }
}
