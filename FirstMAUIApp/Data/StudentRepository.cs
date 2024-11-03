using FirstMAUIApp.Data;
using FirstMAUIApp.Entities;
using FirstMAUIApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FirstMAUIApp.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly DataContext _context;

    public StudentRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Student> GetStudentByIdAsync(string studentId)
    {
        return await _context.Students.FindAsync(studentId);
    }

    public async Task AddStudentAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStudent(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(string studentId)
    {
        var student = await GetStudentByIdAsync(studentId);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Student>> GetStudentsByClassIdAsync(string classId)
    {
        return await _context.Students.Where(s => s.ClassId == classId).ToListAsync();
    }
}