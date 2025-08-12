using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TestWPF.Commands;
using TestWPF.Models;

namespace TestWPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TaskItem> Tasks { get; set; } = new ();
        public ICommand AddTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; }

        private string _newTaskTitle = string.Empty;
        public string NewTaskTitle
        {
            get => _newTaskTitle;
            set
            {
                _newTaskTitle = value;
                OnPropertyChanged(nameof(NewTaskTitle));
            }
        }

        public MainViewModel()
        {
            AddTaskCommand = new RelayCommand(_ => AddTask(), _ => !string.IsNullOrWhiteSpace(NewTaskTitle));
            RemoveTaskCommand = new RelayCommand(task => RemoveTask((TaskItem)task));
        }

        private void AddTask()
        {
            Tasks.Add(new TaskItem { Title = NewTaskTitle });
            NewTaskTitle = string.Empty; // Clear the input after adding
        }

        private void RemoveTask(TaskItem task)
        {
            if (task != null)
            {
                Tasks.Remove(task);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}