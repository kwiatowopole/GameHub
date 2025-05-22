using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameHub.Models
{
    public class Score
    {
        [Key]
        public int scoreId { get; set; }
        [Required]
        public int points { get; set; }
        public DateTime date { get; set; }

        public int userId { get; set; }
        [ForeignKey("userId")]
        public virtual User user { get; set; }

        public int gameId { get; set; }
        [ForeignKey("gameId")]
        public virtual Game game { get; set; }
    }
}