using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using MVC_Omicron.Helper;
using MVC_Omicron.Models;
using MVC_Omicron.Views.Shared;
using PagedList;
namespace MVC_Omicron.Controllers
{
    public class RegistrationController : HomeController
    {


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

        // GET: Registration
        [HttpGet]
        public ActionResult RegistrationList()
        {

            RegistrationBal bal = new RegistrationBal();
            var Registrationlist = from r in bal.GetEnrollments()
                                   select new Register
                                   {
                                       EnrollmentID = r.EnrollmentID,
                                       StudentID = r.StudentID,
                                       StudentName = r.StudentName,
                                       CourseName = r.CourseName,
                                       RegisterDate = r.RegisterDate,
                                       StudentFees = r.StudentFees
                                   };
            return View(Registrationlist.ToList());
            
        }

        [HttpPost]
        public ActionResult RegistrationList(Register stu, string searchString,string lang)
        {
            RegistrationBal bal = new RegistrationBal();
            var Registrationlist = from r in bal.GetEnrollmentsOnSearch(searchString)
                                   select new Register
                                   {
                                       EnrollmentID = r.EnrollmentID,
                                       StudentID = r.StudentID,
                                       StudentName = r.StudentName,
                                       CourseName = r.CourseName,
                                       RegisterDate = r.RegisterDate,
                                       StudentFees = r.StudentFees
                                   };
            return View(Registrationlist.ToList());
        }

       //Get search from bal
       [HttpGet]
        public ActionResult EditRegistartion(int id) ////get edit from bal
        {
            /**View Bag for Drop down list**/
            StudentBAL stubal = new StudentBAL();
            CourseBal courseBal = new CourseBal();
            ViewBag.Courses = courseBal.GetCourse();
            ViewBag.Students = stubal.Get_All_Student();

            ///////////////////////////////////////////////////


            RegistrationBal reg = new RegistrationBal();
            var result = reg.edit_enrollment(id).Select(r=>new Register {
                EnrollmentID = r.EnrollmentID,
                StudentID = r.StudentID,
                StudentName = r.StudentName,
                CourseName = r.CourseName,
                RegisterDate = r.RegisterDate,
                StudentFees = r.StudentFees,
                CourseID = r.CourseID
            }).Single(s=>s.EnrollmentID==id);
  
            return View(result);


        }

        [HttpPost]
        public ActionResult EditRegistartion(Register r,int id)
        {
          
            if (ModelState.IsValid) {

                StudentBAL stu = new StudentBAL();
                CourseBal courseBal = new CourseBal();
                RegistrationBal bal = new RegistrationBal();
                DateTime now = DateTime.Now;
                string reg = now.ToString();
                double fees;
                if (r.CourseID == 1)
                    fees = 20;
                else if (r.CourseID == 2)
                    fees = 40;
                else
                    fees = 60;
                if (bal.Check_user_input(r.StudentID, r.CourseID))
                {
                    bal.Update_Registered_Student(r.EnrollmentID, r.StudentID, r.CourseID, reg, fees);
                    return RedirectToAction("RegistrationList");
                }
                else
                {
                    r.EnrollmentID = id;
                    ViewBag.Courses = courseBal.GetCourse();
                    ModelState.AddModelError("CourseID", SiteResource.ExistsRegistered);
                }

            }

            return View(r);

        }

        [HttpGet]
        public ActionResult Add_NewRegistration()
        {
            
            StudentBAL stu = new StudentBAL();
            CourseBal courseBal = new CourseBal();
            ViewBag.Courses = courseBal.GetCourse();
            ViewBag.Students = stu.Get_All_Student();
            return View();
        }

        [HttpPost]
        public ActionResult Add_NewRegistration(Register r)
        {

            
            if (ModelState.IsValid)
            {
                StudentBAL stu = new StudentBAL();
                CourseBal courseBal = new CourseBal();
                RegistrationBal bal = new RegistrationBal();
                DateTime now = DateTime.Now;
                string reg = now.ToString();
                double fees;
                if (r.CourseID == 1)
                    fees = 20;
                else if (r.CourseID == 2)
                    fees = 40;
                else
                    fees = 60;
                if (bal.Check_user_input(r.StudentID, r.CourseID))
                {
                    bal.Registr_new_student(r.StudentID, r.CourseID, reg, fees);
                    return RedirectToAction("RegistrationList");
                }
                else
                {
                    ViewBag.Students = stu.Get_All_Student();
                    ViewBag.Courses = courseBal.GetCourse();
                    ModelState.AddModelError("StudentID", SiteResource.ExistsRegistered);
                }
            }
            return View(r);
        }


        
        public ActionResult DeleteRegistered(int id)
        {
           
                RegistrationBal bal = new RegistrationBal();
                bal.Delete_Registered(id);
                return RedirectToAction("RegistrationList");
           
        }




        }
}