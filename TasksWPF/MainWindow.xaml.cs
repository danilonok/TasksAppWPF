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
using Microsoft.EntityFrameworkCore;
using System.Windows.Shapes;
using System.Transactions;

namespace TasksWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AssignmentsController assignmentsController;
        public MainWindow()
        {
            InitializeComponent();
            assignmentsController = new AssignmentsController();
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            UpdateList();

        }
        
        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (inputBox.Text.Trim() != "")
                {
                    Assignment task = new Assignment(inputBox.Text.Trim());
                    assignmentsController.AddAssignment(task);
                    inputBox.Text = "";
                }

                UpdateList();
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Delete)
            {
                assignmentsController.Clear();
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
            radioButton.Checked += (sender, e) => RadioButton_Checked(sender, e, assignment.Id);


            smallPanel.Orientation = Orientation.Horizontal;
            Label label = new Label();
            label.FontSize = 12;
            label.Margin = new Thickness(14, 0, 0, 0);
            label.Foreground = new SolidColorBrush(Color.FromRgb(106, 106, 106));
            label.Content = assignment.Date.TimeOfDay;
            Button deleteButton = new Button();
            deleteButton.Background = Brushes.Transparent;
            deleteButton.BorderThickness = new Thickness(0);
            deleteButton.Foreground = new SolidColorBrush(Color.FromRgb(242, 59, 59));
            deleteButton.Content = "Delete";
            deleteButton.Click += (sender, e) => deleteButton_clicked(sender, e, assignment.Id);
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

        private void RadioButton_Checked(object sender, RoutedEventArgs e, int id)
        {
            Assignment task = assignmentsController.Find(id);
            assignmentsController.CompleteAssignment(task);
            UpdateList();
        }
        private void deleteButton_clicked(object sender, RoutedEventArgs e, int id)
        {
            if (MessageBox.Show("Are you sure to delete it?", "Deleting assignment", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Assignment task = assignmentsController.Find(id);
                assignmentsController.DeleteAssignment(task);
            }

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
