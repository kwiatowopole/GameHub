using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameHub.Models;

namespace GameHub.Helpers
{
    public class ViewModelScore
    {
        public string GameName { get; set; }
        public string Username { get; set; }
        public int Points { get; set; }
        public DateTime Date { get; set; }
    }
}