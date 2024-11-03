using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMAUIApp.Entities;

public class Student
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string MSSV { get; set; }
    public required string Name { get; set; }
    public required string DoB { get; set; }

    //Class navigation purpose
    public string ClassId { get; set; }
    public Class Class { get; set; } = null!;
}
