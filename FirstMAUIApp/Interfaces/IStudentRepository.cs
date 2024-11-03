using FirstMAUIApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMAUIApp.Interfaces;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetStudentsByClassIdAsync(string classId);
    Task<Student> GetStudentByIdAsync(string studentId);
    Task AddStudentAsync(Student student);
    Task DeleteStudentAsync(string studentId);
    Task UpdateStudent(Student student);
}