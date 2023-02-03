using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magix_api.Models
{
    public class Deck
    {
        public int Id { get; set; }

        public int[] Cards { get; set; } = new int[30];

        public string Name { get; set; } = "";

        public int Player { get; set; }
    }
}