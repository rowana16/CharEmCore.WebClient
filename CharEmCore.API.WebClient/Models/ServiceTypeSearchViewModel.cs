using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharEmCore.API.WebClient.Models
{
    public class ServiceTypeSearchViewModel
    {
        public ServiceTypeSearchViewModel()
        {
            ServiceTypes = new List<ServiceType>();
        }

        public List<ServiceType> ServiceTypes { get; set; }

    }

    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}


