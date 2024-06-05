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

        // Event handler for handling task completion
        private void Task_Completed(object sender, EventArgs e)
        {
            // Get the task item container
            FrameworkElement container = (FrameworkElement)((Control)sender).Parent;

            // Animate the task item
            DoubleAnimation animation = new DoubleAnimation
            {
                From = container.ActualWidth,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            // Remove the task from the collection when animation completes
            animation.Completed += (s, _) => Tasks.Remove((Task)container.DataContext);
            container.BeginAnimation(WidthProperty, animation);
        }

        // Event handler for handling task checkbox checked event
        private void Task_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.IsChecked == true)
            {
                // Get the task item container
                FrameworkElement container = (FrameworkElement)((Control)sender).Parent;

                // Animate the task item
                DoubleAnimation animation = new DoubleAnimation
                {
                    From = container.ActualWidth,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.3)
                };
                // Remove the task from the collection when animation completes
                animation.Completed += (s, _) => Tasks.Remove((Task)container.DataContext);
                container.BeginAnimation(WidthProperty, animation);
            }
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
                // Add a new task to the collection and clear the text box
                Tasks.Add(new Task { Title = newTaskTitle });
                txtNewTask.Clear();
            }
        }
    }
}
