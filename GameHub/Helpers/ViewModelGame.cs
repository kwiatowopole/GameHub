using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameHub.Models;

namespace GameHub.Helpers
{
    public class ViewModelGame
    {
        public Game Game { get; set; }
        public string Description { get; set; }
        public string GifUrl { get; set; }
    }
}