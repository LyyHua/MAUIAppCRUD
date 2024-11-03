using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FirstMAUIApp.Entities;
using FirstMAUIApp.Interfaces;
using System.Threading.Tasks;

namespace FirstMAUIApp.ViewModel;

[QueryProperty(nameof(Student), "student")]
public partial class UpdateDialogViewModel : ObservableObject
{
    private readonly IStudentRepository _studentRepository;
    private readonly DetailViewModel _detailViewModel;

    public UpdateDialogViewModel(IStudentRepository studentRepository, DetailViewModel detailViewModel)
    {
        _studentRepository = studentRepository;
        _detailViewModel = detailViewModel;
    }

    [ObservableProperty]
    string? studentMssv;

    [ObservableProperty]
    string? studentName;

    [ObservableProperty]
    string? studentDob;

    [ObservableProperty]
    string? studentId;

    [ObservableProperty]
    Student? student;

    partial void OnStudentChanged(Student value)
    {
        if (value != null)
        {
            StudentId = value.Id;
            StudentMssv = value.MSSV;
            StudentName = value.Name;
            StudentDob = value.DoB;
        }
    }

    [RelayCommand]
    public async Task SaveStudentAsync()
    {
        if (string.IsNullOrWhiteSpace(StudentMssv) || string.IsNullOrWhiteSpace(StudentName) || string.IsNullOrWhiteSpace(StudentDob) || string.IsNullOrWhiteSpace(StudentId))
            return;

        var student = await _studentRepository.GetStudentByIdAsync(StudentId);
        if (student != null)
        {
            student.MSSV = StudentMssv;
            student.Name = StudentName;
            student.DoB = StudentDob;

            await _studentRepository.UpdateStudent(student);

            // Update the student in the DetailViewModel's collection
            _detailViewModel.UpdateStudentInCollection(student);

            // Navigate back to the previous page
            await Shell.Current.GoToAsync("..");
        }
    }
}