using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APV._Plugins.WebAPI.Tmdb.Models
{
    public class ProductionCompany
    {
        public int Id { get; set; }
        public string Logo_path { get; set; }
        public string Name { get; set; }
        public string Origin_country { get; set; }
    }
}
