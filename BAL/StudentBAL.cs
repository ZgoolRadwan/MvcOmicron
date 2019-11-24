using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BAL
{
    public class StudentBAL
    {


        public List<Student> Get_All_Student()
        {


            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {
                return db.Students.ToList();

            }
        }

        public void Add_Student(string name, string email, string mobile, string address, DateTime? birthday)
        {
            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {
                db.Add_Student(name, email, mobile, address, birthday);
            }
        }

        public void Update_Student(int id, string name, string email, string mobile, string address, DateTime? birthday)
        {
            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {
                db.Update_Student(id, name, email, mobile, address, birthday);
            }
        }

        public void Delete_student(int id)
        {
            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {

                var select = db.Enrollments.Where(s => s.StudentID == id).Select(s => s.EnrollmentID);
                var count = select.Count();
                for (int i = 0; i < count; i++)
                {
                    var sel = (from e in db.Enrollments
                               where e.StudentID == id
                               select new { e.EnrollmentID }).FirstOrDefault();
                    db.delete_registered(sel.EnrollmentID);
                }
                db.Delete_Student(id);
               
            }
        }

        public bool Check_user_input(string studentName, string studentEmail)
        {
            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {
                var select = from stu in db.Students
                             where (stu.StudentName == studentName && stu.StudentEmail == studentEmail)
                             select stu;
                int count = select.Count();
                if (count > 0)
                    return false;
                return true;

            }
        }

        public List<Student> GetAllStudentOnSearch(string name)
        {

            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {
                var result = db.Students.Where(s => s.StudentName.Contains(name));

                return result.ToList();
            }
            //return db.Students.Where(s => s.StudentName.Contains(name)).ToList();
        }

        public List<dynamic> GetOneStudent(int id)
        {

            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {
                //var result = db.Students.Where(s=>s.StudentID==id);
                //return result.ToList();
                return db.Get_One_Student(id).ToList<dynamic>();
                
            }
            //return db.Students.Where(s => s.StudentName.Contains(name)).ToList();
        }

    }
}
