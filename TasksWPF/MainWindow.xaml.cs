using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TasksWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Assignment> shownAssignments;
        AssignmentsController assignmentsController;
        public MainWindow()
        {
            InitializeComponent();
            assignmentsController = new AssignmentsController();
            shownAssignments = new List<Assignment>();
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            UpdateList();
            
        }
        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (inputBox.Text != "")
                {
                    assignmentsController.AddAssignment(new Assignment(inputBox.Text));
                    inputBox.Text = "";
                }
                    
                UpdateList();
            }
        }

       
        private void UpdateList()
        {
            mainPanel.Children.Clear();
            counter.Content = assignmentsController.Incompleted;
            foreach (var i in assignmentsController.assignments)
            {
                AddAssignment(i);
            }
        }

        private void AddAssignment(Assignment assignment)
        {
            

            StackPanel panel = new StackPanel();
            StackPanel smallPanel = new StackPanel();
            panel.Margin = new Thickness(30, 8, 40, 0);
            RadioButton radioButton = new RadioButton();
            radioButton.VerticalContentAlignment = VerticalAlignment.Center;
            radioButton.FontSize = 16;
            radioButton.Content = assignment.Text;
            radioButton.IsChecked = assignment.isDone;
            radioButton.Checked += (sender, e) => RadioButton_Checked(sender, e, assignment.Date);

            
            smallPanel.Orientation = Orientation.Horizontal;
            Label label = new Label();
            label.FontSize = 12;
            label.Margin = new Thickness(14, 0, 0, 0);
            label.Foreground = new SolidColorBrush(Color.FromRgb(106, 106, 106));
            label.Content = assignment.Date.Date;
            Button deleteButton = new Button();
            deleteButton.Background = Brushes.Transparent;
            deleteButton.BorderThickness = new Thickness(0);
            deleteButton.Foreground = new SolidColorBrush(Color.FromRgb(242, 59, 59));
            deleteButton.Content = "Delete";
            deleteButton.Click += (sender, e) => deleteButton_clicked(sender, e, assignment.Date);
            smallPanel.Children.Add(label);
            smallPanel.Children.Add(deleteButton);

            panel.Children.Add(radioButton);
            panel.Children.Add(smallPanel);
            if (assignment.isDone)
            {
                panel.Opacity = 0.5f;
            }
            mainPanel.Children.Add(panel);
            
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e, DateTime date)
        {
            assignmentsController.CompleteAssignment(assignmentsController.assignments.Find(x => x.Date == date));
            UpdateList();
        }
        private void deleteButton_clicked(object sender, RoutedEventArgs e, DateTime date)
        {
            assignmentsController.DeleteAssignment(assignmentsController.assignments.Find(x => x.Date == date));
            UpdateList();
        }

        private void inputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (inputBox.Text.Length == 0)
            {
                inputHint.Visibility = Visibility.Visible;
            }
            else
            {
                inputHint.Visibility = Visibility.Hidden;
            }
        }

    }
}
