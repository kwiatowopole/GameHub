using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameHub.Models
{
    public class Game
    {
        [Key]
        public int gameId { get; set; }
        [Required]
        public string name { get; set; }
        public string Category { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}