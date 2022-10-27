using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksWPF
{
    public class Assignment
    {
        public Assignment(string text)
        {
            Text = text;
            Date = DateTime.Now;
            isDone = false;
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public bool isDone { get; set;}
        public DateTime Date { get; set; }
    }
}
