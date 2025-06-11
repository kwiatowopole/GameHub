using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
        public virtual ICollection<FavGame> FavGames { get; set; }
    }
}