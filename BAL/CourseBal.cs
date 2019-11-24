using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BAL
{
    public class CourseBal
    {
        public List<Cours> GetCourse()
        {
            using (OmicronUniversityEntities db = new OmicronUniversityEntities())
            {
                return db.Courses.ToList();
            }
        }
    }
}
