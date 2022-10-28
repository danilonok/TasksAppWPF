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
    public class AssignmentsController
    {
        public ApplicationContext db = new ApplicationContext();
        public AssignmentsController()
        {
            assignments = new List<Assignment>();
            DBInit();

        }
        public List<Assignment> assignments;


        public int Incompleted = 0;
        private void DBInit()
        {
            db.Database.EnsureCreated();
            db.Assignments.Load();
            assignments = GetAll();
            Sort();
        }
        public void AddAssignment(Assignment assignment)
        {
            assignments.Insert(0, assignment);
            db.Assignments.Add(assignment);
            db.SaveChanges();
            Incompleted++;
        }

        public Assignment Find(int id)
        {
            return assignments.Find(x => x.Id == id);
        }
        public void DeleteAssignment(Assignment assignment)
        {
            if(!assignment.isDone)
                Incompleted--;
            assignments.Remove(assignment);
            db.Assignments.Remove(assignment);
            db.SaveChanges();

        }
        public void CompleteAssignment(Assignment assignment)
        {
            assignments.Remove(assignment);
            assignment.isDone = true;
            assignments.Insert(assignments.Count, assignment);
            db.SaveChanges();
            Incompleted--;
        }
        public void Sort()
        {

            assignments = assignments.OrderByDescending(x => x.Date).ToList();
            assignments =  assignments.OrderBy(u => !u.isDone ? 0 : 1).ToList();
            foreach (var assignment in assignments)
            {
                if (!assignment.isDone)
                {
                    Incompleted++;
                }
            }

        }
        public void Clear()
        {
            assignments.Clear();
            Incompleted = 0;
            db.Assignments.RemoveRange(db.Assignments);
            db.SaveChanges();

        }

        private List<Assignment> GetAll()
        {
            return db.Assignments.ToList();
        }

    }
}
