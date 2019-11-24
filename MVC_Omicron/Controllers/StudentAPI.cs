using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAL;
using MVC_Omicron.Helper;
using MVC_Omicron.Models;

namespace MVC_Omicron.Controllers
{
   

    public class StudentAPI : ApiController
    {
        public List<Studentlist> Get()
        {
            StudentBAL bal = new StudentBAL();
            var result = from b in bal.Get_All_Student()
                         select new Studentlist
                         {
                             StudentID = b.StudentID,
                             StudentEmail = b.StudentEmail,
                             StudentMobile = b.StudentMobile,
                             StudentAddress = b.StudentAddress,
                             StudentName = b.StudentName,
                             Birthday = b.Birthday,
                             Age = b.Age
                         };
                return result.ToList();
           
               
        }
        

    }
}
