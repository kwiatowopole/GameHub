using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameHub.Models;
using GameHub.Helpers;

namespace GameHub.Helpers
{
    public class ViewModelGameCat
    {
        public Category Category { get; set; }
        public List<ViewModelGame> Games { get; set; }
    }
}