using FirstMAUIApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMAUIApp.Interfaces;

public interface IClassRepository
{
    Task<IEnumerable<Class>> GetClassesAsync();
    Task AddClassAsync(Class classroom);
    Task DeleteClassAsync(string classId);
    void UpdateClass(Class classroom);
}
