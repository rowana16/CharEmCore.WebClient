using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharEmCore.API.WebClient.Models
{
    public class County
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CountyViewModel
    {
        public CountyViewModel()
        {
            Counties = new List<County>();
        }

        public List<County> Counties { get; set; }
    }
}
