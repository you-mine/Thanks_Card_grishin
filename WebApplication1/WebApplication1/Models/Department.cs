using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Department
    {

        public int Id { get; set; }
        [MaxLength(6)]
        public string CD { get; set; }
        [MaxLength(20)]
        public string DepartmentName { get; set; }
        [MaxLength(20)]
        public string SectionName { get; set; }
    }
}
