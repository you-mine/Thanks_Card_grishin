using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {
        
        public int Id { get; set; }

        public string CD { get; set; }

        public virtual Department Department { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Hurigana { get; set; }

        public bool IsAdmin { get; set; }
    }
}
