using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameHub.Models
{
    public class User
    {

        [Key]
        public int userId { get; set ; }

        [Required]
        [MaxLength(30)]
        public string username { get; set; }
        [EmailAddress] public string emailAddress { get; set; }

        [Required]
        [MaxLength(20)]
        public string password { get; set; }

        public virtual ICollection<Score> Scores { get; set; }

        public bool isAdmin { get; set; }

        public virtual ICollection<FavGame> FavGames { get; set; }
    }
}