using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ToDo
{
    public partial class MainWindow : Window
    {
        // Collection to store tasks
        public ObservableCollection<TaskItem> Tasks { get; set; }

        // Constructor
        public MainWindow()
        {
            InitializeComponent();

            // Initialize the tasks collection and bind it to the ListBox
            Tasks = new ObservableCollection<TaskItem>();
            lstTasks.ItemsSource = Tasks;

            // Subscribe to the CollectionChanged event to update background colors
            Tasks.CollectionChanged += Tasks_CollectionChanged;
        }

        // Event handler for the LostFocus event of the new task text box
        private void TxtNewTask_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Enter new task...";
            }
        }

        // Event handler for the GotFocus event of the new task text box
        private void TxtNewTask_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Enter new task...")
            {
                textBox.Text = string.Empty;
            }
        }

        // Event handler for the KeyDown event of the new task text box
        private void TxtNewTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddTask_Click(sender, e);
            }
        }

        // Event handler for adding a new task
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            // Get the title of the new task from the text box
            string newTaskTitle = txtNewTask.Text.Trim();

            // Check if the title is not empty
            if (!string.IsNullOrWhiteSpace(newTaskTitle))
            {
                // Add a new task to the collection
                Tasks.Add(new TaskItem { Title = newTaskTitle });

                // Clear the text box for the next input
                txtNewTask.Clear();
            }
        }

        // Event handler for the Checked event of the checkbox
        private void Task_Checked(object sender, RoutedEventArgs e)
        {
            // Get the checkbox and corresponding task item
            CheckBox checkBox = (CheckBox)sender;
            TaskItem task = checkBox.DataContext as TaskItem;

            // Check if the task item exists
            if (task != null)
            {
                // Update the IsCompleted property of the task
                task.IsCompleted = checkBox.IsChecked.Value;

                // Adjust the opacity of the task text
                task.TextOpacity = 0.5; // You can adjust the opacity level as needed
            }
        }

        // Event handler for the Unchecked event of the checkbox
        private void Task_Unchecked(object sender, RoutedEventArgs e)
        {
            // Get the checkbox and corresponding task item
            CheckBox checkBox = (CheckBox)sender;
            TaskItem task = checkBox.DataContext as TaskItem;

            // Check if the task item exists
            if (task != null)
            {
                // Update the IsCompleted property of the task
                task.IsCompleted = checkBox.IsChecked.Value;

                // Restore the opacity of the task text
                task.TextOpacity = 1.0; // Set it back to full opacity
            }
        }

        // Event handler for the Click event of the delete button
        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            // Get the delete button and corresponding task item
            Button deleteButton = (Button)sender;
            TaskItem task = deleteButton.DataContext as TaskItem;

            // Check if the task item exists
            if (task != null)
            {
                // Get the task item container
                FrameworkElement container = (FrameworkElement)deleteButton.Parent;

                // Animate the collapse of the task item
                DoubleAnimation collapseAnimation = new DoubleAnimation
                {
                    From = container.ActualHeight,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.3)
                };
                collapseAnimation.Completed += (s, _) => Tasks.Remove(task);
                container.BeginAnimation(HeightProperty, collapseAnimation);
            }
        }


        // Method to update background colors of ListBox items
        private void Tasks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateBackgroundColors();
        }

        // Method to update background colors of ListBox items
        private void UpdateBackgroundColors()
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                TaskItem task = Tasks[i];
                task.BackgroundColor = i % 2 == 0 ? Brushes.LightGray : Brushes.DarkGray;
            }
        }
    }

    // Class representing a task item
    public class TaskItem : INotifyPropertyChanged
    {
        // Event to notify when property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Title of the task
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        // Flag indicating whether the task is enabled
        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }

        // Background color of the task item
        private Brush _backgroundColor = Brushes.White;
        public Brush BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundColor)));
            }
        }

        // Flag indicating whether the task is completed
        private bool _isCompleted;
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                _isCompleted = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCompleted)));
            }
        }

        // Opacity of the task text
        private double _textOpacity = 1.0;
        public double TextOpacity
        {
            get { return _textOpacity; }
            set
            {
                _textOpacity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextOpacity)));
            }
        }

        // Text color of the task item
        public Brush TextColor => BackgroundColor == Brushes.DarkGray ? Brushes.White : Brushes.Black;
    }
}
