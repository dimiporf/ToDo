using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Task> Tasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<Task>();
            lstTasks.ItemsSource = Tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string newTaskTitle = txtNewTask.Text.Trim();
            if (!string.IsNullOrWhiteSpace(newTaskTitle))
            {
                Tasks.Add(new Task { Title = newTaskTitle });
                txtNewTask.Clear();
            }
        }

        private void TxtNewTask_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Enter new task...")
            {
                textBox.Text = string.Empty;
            }
        }

        private void TxtNewTask_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Enter new task...";
            }
        }
    }

    public class Task
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}