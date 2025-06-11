using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GameHub.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Game> Game { get; set; }

        public static implicit operator Category(string v)
        {
            throw new NotImplementedException();
        }

    }
}