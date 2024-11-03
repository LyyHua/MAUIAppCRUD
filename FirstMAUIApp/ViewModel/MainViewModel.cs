using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FirstMAUIApp.Entities;
using FirstMAUIApp.Interfaces;
using System.Collections.ObjectModel;

namespace FirstMAUIApp.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private readonly IClassRepository _classRepository;

    public MainViewModel(IClassRepository repository)
    {
        _classRepository = repository;
        Items = new ObservableCollection<string>();
        LoadClassesAsync();
    }

    [ObservableProperty]
    ObservableCollection<string> items;

    [ObservableProperty]
    string? text;

    [RelayCommand]
    async Task AddAsync()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;

        var newClass = new Class { Name = Text };
        await _classRepository.AddClassAsync(newClass);

        Items.Add(newClass.Name);
        Text = string.Empty;
    }

    [RelayCommand]
    private async Task DeleteAsync(string className)
    {
        var classToDelete = (await _classRepository.GetClassesAsync()).FirstOrDefault(c => c.Name == className);
        if (classToDelete != null)
        {
            await _classRepository.DeleteClassAsync(classToDelete.Id);
            Items.Remove(className);
        }
    }

    [RelayCommand]
    async Task Tap(string className)
    {
        var classToNavigate = (await _classRepository.GetClassesAsync()).FirstOrDefault(c => c.Name == className);
        if (classToNavigate != null)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?ClassId={classToNavigate.Id}");
        }
    }

    private async void LoadClassesAsync()
    {
        var classes = await _classRepository.GetClassesAsync();
        foreach (var classItem in classes)
        {
            Items.Add(classItem.Name);
        }
    }
}