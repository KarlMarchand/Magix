using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magix_api.Dtos.FactionDto
{
    public class GetFactionDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}