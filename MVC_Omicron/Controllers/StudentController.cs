using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using MVC_Omicron.Models;
using Newtonsoft.Json;
using PagedList;
using System.Threading;
using System.Globalization;
using MVC_Omicron.Helper;
using MVC_Omicron.Views.Shared;
namespace MVC_Omicron.Controllers
{



    public class StudentController : HomeController
    {

        // GET: Student

        public ActionResult ChangeCurrentCulture(int id)
        {
            //  
            // Change the current culture for this user.  
            //  
            CultureHelper.CurrentCulture = id;
            //  
            // Cache the new current culture into the user HTTP session.   
            //  
            Session["CurrentCulture"] = id;
            //  
            // Redirect to the same page from where the request was made!   
            //  
            return Redirect(Request.UrlReferrer.ToString());
        }


        [HttpGet]
        public ActionResult StudentList()
        {
           
            StudentBAL bal = new StudentBAL();
            var result = bal.Get_All_Student().Select(s => new Studentlist
            {
                StudentID = s.StudentID,
                StudentName = s.StudentName,
                StudentEmail = s.StudentEmail,
                StudentMobile = s.StudentMobile,
                StudentAddress = s.StudentAddress,
                Birthday =s.Birthday,
                Age = s.Age
            });
            
            return View(result.ToList());
        }

        [HttpPost]
        public ActionResult StudentList(Studentlist stu,string searchString)
        {   
            StudentBAL bal = new StudentBAL();
            var result = bal.GetAllStudentOnSearch(searchString).Select(s => new Studentlist
            {
                StudentID = s.StudentID,
                StudentName = s.StudentName,
                StudentEmail = s.StudentEmail,
                StudentMobile = s.StudentMobile,
                StudentAddress = s.StudentAddress,
                Birthday = s.Birthday,
                Age = s.Age
               
            });
            return View(result.ToList());
          
        }

        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            
            StudentBAL stu = new StudentBAL();
            var Students = stu.GetOneStudent(id).Select(s=>new Studentlist{
                               StudentName = s.StudentName,
                               StudentID = s.StudentID,
                               StudentEmail = s.StudentEmail,
                               StudentMobile = s.StudentMobile,
                               StudentAddress = s.StudentAddress,
                               Birthday = s.Birthday,
                               Age = s.Age
                           }).FirstOrDefault();

            return View(Students);
        }

        [HttpPost]
        public ActionResult EditStudent(Studentlist stu)
        {

            
            if (ModelState.IsValid)
            {
                StudentBAL studentBAL = new StudentBAL();
                studentBAL.Update_Student(stu.StudentID,stu.StudentName,stu.StudentEmail,stu.StudentMobile,stu.StudentAddress,stu.Birthday);
                return RedirectToAction("StudentList");

            }

            return View(stu);
        }

        [HttpGet]
        public ActionResult add_new_student()
        {
          
            return View();
        }

        [HttpPost]
        public ActionResult add_new_student(Studentlist stu )
        {
           

            if (ModelState.IsValid)
            {
                StudentBAL bal = new StudentBAL();
                if (bal.Check_user_input(stu.StudentName, stu.StudentEmail))
                {
                    bal.Add_Student(stu.StudentName, stu.StudentEmail, stu.StudentMobile, stu.StudentAddress, stu.Birthday);
                }
                else
                { ModelState.AddModelError("StudentName", SiteResource.ExistsStudent);
                    return View(stu);
                }
                return RedirectToAction("StudentList");
            }
           
            return View(stu);
            
   
        }

        public ActionResult DeleteStudent(int id)
        {
                StudentBAL bal = new StudentBAL();
                bal.Delete_student(id);
                return RedirectToAction("StudentList");
           
         

        }        
    }
}