using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FirstMAUIApp.Entities;
using FirstMAUIApp.Interfaces;

namespace FirstMAUIApp.ViewModel;

[QueryProperty(nameof(ClassId), nameof(ClassId))]
public partial class DetailViewModel : ObservableObject
{
    private readonly IStudentRepository _studentRepository;

    public DetailViewModel(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
        Students = new ObservableCollection<Student>();
    }

    [ObservableProperty]
    ObservableCollection<Student> students;

    [ObservableProperty]
    string? studentMssv;

    [ObservableProperty]
    string? studentName;

    [ObservableProperty]
    string? studentDob;

    [ObservableProperty]
    string? classId;

    [RelayCommand]
    async Task AddStudentAsync()
    {
        if (string.IsNullOrWhiteSpace(StudentMssv) || string.IsNullOrWhiteSpace(StudentName) || string.IsNullOrWhiteSpace(StudentDob) || string.IsNullOrWhiteSpace(ClassId))
            return;

        var newStudent = new Student
        {
            MSSV = StudentMssv,
            Name = StudentName,
            DoB = StudentDob,
            ClassId = ClassId
        };

        await _studentRepository.AddStudentAsync(newStudent);

        Students.Add(newStudent);
        StudentMssv = string.Empty;
        StudentName = string.Empty;
        StudentDob = string.Empty;
    }

    [RelayCommand]
    private async Task DeleteStudentAsync(Student student)
    {
        if (student != null)
        {
            await _studentRepository.DeleteStudentAsync(student.Id);
            Students.Remove(student);
        }
    }

    [RelayCommand]
    private async Task UpdateStudentAsync(Student student)
    {
        if (student != null)
        {
            // Set the properties to the selected student's values
            StudentMssv = student.MSSV;
            StudentName = student.Name;
            StudentDob = student.DoB;

            // Navigate to the UpdateDialog and pass the selected student
            var navigationParameter = new Dictionary<string, object>
            {
                { "student", student }
            };
            await Shell.Current.GoToAsync(nameof(UpdateDialog), navigationParameter);
        }
    }

    public async void LoadStudentsAsync()
    {
        if (string.IsNullOrWhiteSpace(ClassId))
            return;

        var students = await _studentRepository.GetStudentsByClassIdAsync(ClassId);
        Students.Clear();
        foreach (var student in students)
        {
            Students.Add(student);
        }
    }

    public async Task RefreshStudentsAsync()
    {
        if (string.IsNullOrWhiteSpace(ClassId))
            return;

        var students = await _studentRepository.GetStudentsByClassIdAsync(ClassId);
        Students.Clear();
        foreach (var student in students)
        {
            Students.Add(student);
        }
    }

    public void UpdateStudentInCollection(Student updatedStudent)
    {
        var student = Students.FirstOrDefault(s => s.Id == updatedStudent.Id);
        if (student != null)
        {
            var index = Students.IndexOf(student);
            Students[index] = updatedStudent;
        }
    }
}