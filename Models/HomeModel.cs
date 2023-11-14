using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication1.Models
{
    public class HomeModel
    {
        public string StdRegNumber { get; set; } = default!;
        public string StdName { get; set; } = default!;
        public string StdContact { get; set; } = default!;
        public string StdEmail { get; set; } = default!;
        public bool isActive { get; set; }


    }
}