using System.ComponentModel;

namespace ToDo
{
    // Class representing a task
    public class Task : INotifyPropertyChanged
    {
        private bool isCompleted;
        public int ID { get; set; } // Unique identifier for the task
        public string Title { get; set; } // Task title
        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                if (isCompleted != value)
                {
                    isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));
                }
            }
        } // Task completion status

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
