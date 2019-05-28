using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {
        [MaxLength(6)]
        public int Id { get; set; }

        [MaxLength(6)]
        public string CD { get; set; }

        [MaxLength(4)]
        public string DepartmentId { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }

        [MaxLength(20)]
        public string UserName { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(40)]
        public string Hurigana { get; set; }

        public bool IsAdmin { get; set; }
    }
}
