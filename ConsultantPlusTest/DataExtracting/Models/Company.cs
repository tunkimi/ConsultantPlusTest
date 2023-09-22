using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataExtracting.Models
{
    [Table("Company")]
    class Company
    {
        [Key]
        public string INN { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public bool Status{ get; set; }
    }
}
