using FirstMAUIApp.Entities;
using FirstMAUIApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FirstMAUIApp.Data;

public class ClassRepository(DataContext context) : IClassRepository
{
    public async Task<IEnumerable<Class>> GetClassesAsync()
    {
        return await context.Classes.ToListAsync();
    }

    public async Task AddClassAsync(Class classroom)
    {
        context.Classes.Add(classroom);
        await context.SaveChangesAsync();
    }

    public async Task DeleteClassAsync(string classId)
    {
        var classToDelete = await context.Classes.FindAsync(classId);
        if (classToDelete != null)
        {
            context.Classes.Remove(classToDelete);
            await context.SaveChangesAsync();
        }
    }

    public void UpdateClass(Class classroom)
    {
        context.Classes.Update(classroom);
        context.SaveChanges();
    }
}