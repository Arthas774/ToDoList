using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ToDoList
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MyViewModel();
        }
    }

    public class MyViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MyTask> Items { get; }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        private string _entryText;
        public string EntryText
        {
            get => _entryText;
            set { _entryText = value; OnPropertyChanged(); }
        }

        public MyViewModel()
        {
            Items = new ObservableCollection<MyTask>();
            AddCommand = new Command<object>(addTask);
            RemoveCommand = new Command<MyTask>(removeFromItems);
        }

        void addTask(object p)
        {
            if (p is string s && !string.IsNullOrWhiteSpace(s))
            {
                Items.Add(new MyTask { Name = s });
                EntryText = string.Empty;
            }
        }
        void removeFromItems(MyTask t) => Items.Remove(t);

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string n = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }

    public class MyTask : INotifyPropertyChanged
    {
        private bool _isDone;
        private string _name = string.Empty;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }
        public bool IsDone
        {
            get => _isDone;
            set { _isDone = value; OnPropertyChanged(); OnPropertyChanged(nameof(Stroke)); }
        }
        public TextDecorations Stroke => IsDone ? TextDecorations.Strikethrough : TextDecorations.None;

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string n = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }
}