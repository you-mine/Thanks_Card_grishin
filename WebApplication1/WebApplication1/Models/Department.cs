using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class Department
    {
        [MaxLength(4)]
        public int Id { get; set; }

        [MaxLength(6)]
        public String CD { get; set; }

        [MaxLength(20)]
        public String DepartmentName{ get; set; }
         [MaxLength(20)]
        public String SectionName { get; set; }


    }
}
