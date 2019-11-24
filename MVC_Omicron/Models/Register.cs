using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MVC_Omicron.Views.Shared;

namespace MVC_Omicron.Models
{
    public class Register
    {

        public int EnrollmentID { get; set; }

        
        public Nullable<int> StudentID { get; set; }

      
        public Nullable<int> CourseID { get; set; }
        //public Nullable<int> CourseID { get; set; }
        public Nullable<double> StudentFees { get; set; }
        public string RegisterDate { get; set; }

        //[Display(Name = "StudentName", ResourceType = typeof(SiteResource))]
        public string StudentName { get; set; }

        public string CourseName { get; set; }
        
      
        //public virtual Course Cours { get; set; }
        //public virtual Studentlist Student { get; set; }
    }
}