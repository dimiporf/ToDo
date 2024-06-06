using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Collection to store tasks
        public ObservableCollection<Task> Tasks { get; set; }
        private int nextTaskID = 1; // To assign unique IDs to tasks

        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            // Initialize tasks collection and set it as the ItemsSource for the ListBox
            Tasks = new ObservableCollection<Task>();
            lstTasks.ItemsSource = Tasks;
        }

        // Event handler for adding a new task
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddNewTask();
        }

        // Event handler for handling focus on the new task text box
        private void TxtNewTask_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            // Clear placeholder text when text box gets focus
            if (textBox.Text == "Enter new task...")
            {
                textBox.Text = string.Empty;
            }
        }

        // Event handler for handling loss of focus on the new task text box
        private void TxtNewTask_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            // Restore placeholder text if text box is empty
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Enter new task...";
            }
        }

        // Event handler for handling task checkbox checked event
        private void Task_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            Task task = (Task)checkBox.DataContext;
            task.IsCompleted = true; // Mark the task as completed
        }

        // Event handler for handling task checkbox unchecked event
        private void Task_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            Task task = (Task)checkBox.DataContext;
            task.IsCompleted = false; // Mark the task as not completed
        }

        // Event handler for handling Enter key press in the new task text box
        private void TxtNewTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddNewTask();
            }
        }

        // Method to add a new task
        private void AddNewTask()
        {
            string newTaskTitle = txtNewTask.Text.Trim();
            if (!string.IsNullOrWhiteSpace(newTaskTitle))
            {
                // Add a new task to the collection with a unique ID and clear the text box
                Tasks.Add(new Task { ID = nextTaskID++, Title = newTaskTitle });
                txtNewTask.Clear();
            }
        }

        // Event handler for deleting a task
        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            FrameworkElement container = (FrameworkElement)deleteButton.Parent;

            // Animate the task item to vanish
            DoubleAnimation animation = new DoubleAnimation
            {
                From = container.ActualHeight,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            animation.Completed += (s, _) =>
            {
                Task taskToDelete = (Task)deleteButton.DataContext;
                Tasks.Remove(taskToDelete);
            };

            container.BeginAnimation(HeightProperty, animation);
        }
    }
}
