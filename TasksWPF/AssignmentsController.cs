using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksWPF
{
    public class AssignmentsController
    {
        
        public AssignmentsController()
        {
            assignments = new List<Assignment>();
        }
        public List<Assignment> assignments;

        public int Incompleted = 0;
        public void AddAssignment(Assignment assignment)
        {
            assignments.Insert(0, assignment);
            Incompleted++;
        }
        public void DeleteAssignment(Assignment assignment)
        {
            if(!assignment.isDone)
                Incompleted--;
            assignments.Remove(assignment);
            
        }
        public void CompleteAssignment(Assignment assignment)
        {
            
           
            assignments.Remove(assignment);
            assignment.isDone = true;
            assignments.Insert(assignments.Count, assignment);
            Incompleted--;
        }
        
    }
}
