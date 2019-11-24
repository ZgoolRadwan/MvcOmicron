using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BAL
{

    public class RegisterAll
    {
        public int EnrollmentID { get; set; }

      
        public Nullable<int> StudentID { get; set; }

       
        public Nullable<int> CourseID { get; set; }
        public Nullable<double> StudentFees { get; set; }
        public string RegisterDate { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        //public virtual Course Cours { get; set; }
        //public virtual Studentlist Student { get; set; }
    }
    public class RegistrationBal
    {
        public List<RegisterAll> GetEnrollments()
        {
            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {
                //return db.Enrollments.ToList();
                
                StudentBAL stubal = new StudentBAL();
                CourseBal courseBal = new CourseBal();
                var Registrationlist = from s in stubal.Get_All_Student()
                                       join r in db.Enrollments on s.StudentID equals r.StudentID
                                       join c in courseBal.GetCourse() on r.CourseID equals c.CourseID
                                       select new RegisterAll
                                       {
                                           EnrollmentID = r.EnrollmentID,
                                           StudentID = r.StudentID,
                                           StudentName = s.StudentName,
                                           CourseName = c.CourseName,
                                           RegisterDate = r.RegisterDate,
                                           StudentFees = r.StudentFees,
                                           CourseID=r.CourseID
                                           
                                       };
                return Registrationlist.ToList();
            }
        }

        public void Registr_new_student(int? studentId, int? courseId, string regTime, double? courseFee)
        {
            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {

                db.registeration(studentId, courseId, regTime, courseFee);
            }
        }

        public void Update_Registered_Student(int? enrolID, int? studentId, int? courseId, string regTime, double? courseFee)
        {
            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {
                db.update_registered(enrolID, studentId, courseId, regTime, courseFee);
            }
        }

        public void Delete_Registered(int? enrolID)
        {
            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {
                db.delete_registered(enrolID);
            }
        }

        public bool Check_user_input(int? studentID, int? course)
        {
            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {
                var select = from cour in db.Enrollments
                             where (cour.CourseID == course && cour.StudentID == studentID)
                             select cour;
                int count = select.Count();
                if (count > 0)
                    return false;
                return true;

            }
        }

        public List<RegisterAll> GetEnrollmentsOnSearch(string name)
        {
            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {
               
                var result = GetEnrollments().Where(s => s.StudentName.Contains(name));    
                return result.ToList();
            }
        }

        public List<RegisterAll> edit_enrollment(int id)
        {
            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {
                
                 var result=from r in db.Get_single_Registered(id)
                            join s in db.Get_All_Student() on r.StudentID equals s.StudentID
                            join c in db.Courses on r.CourseID  equals c.CourseID
                select new RegisterAll
                {
                    EnrollmentID = r.EnrollmentID,
                    StudentID = r.StudentID,
                    StudentName = s.StudentName,
                    CourseName = c.CourseName,
                    RegisterDate = r.RegisterDate,
                    StudentFees = r.StudentFees,
                    CourseID = r.CourseID

                };

                //var result = GetEnrollments().Select(r=>new RegisterAll
                //{
                //    EnrollmentID = r.EnrollmentID,
                //    StudentID = r.StudentID,
                //    StudentName = r.StudentName,
                //    CourseName = r.CourseName,
                //    RegisterDate = r.RegisterDate,
                //    StudentFees = r.StudentFees,
                //    CourseID = r.CourseID
                //});
                return result.ToList();
            }
        }

        public List<dynamic> Get_One_Registered(int id)
        {
            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {

                return db.Get_One_Registered(id).ToList<dynamic>();
            }

        }


    }
}
