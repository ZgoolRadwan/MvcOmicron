using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Omicron.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public int CourseNumber { get; set; }
        public string CourseName { get; set; }
        public Nullable<int> InstructorID { get; set; }
    }
}


