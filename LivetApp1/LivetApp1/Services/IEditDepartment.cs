using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivetApp1.Models;

namespace LivetApp1.Services
{
    interface IEditDepartment
    {
        Task<String> EditDepartment(Department department);
    }
}
