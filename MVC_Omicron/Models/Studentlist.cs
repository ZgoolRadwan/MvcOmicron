using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.ComponentModel.DataAnnotations;
using MVC_Omicron.Views.Shared;
using System.Threading;
using System.Globalization;
namespace MVC_Omicron.Models
{

    public class Studentlist
    {

        public int StudentID { get; set; }

       
        [Required(ErrorMessageResourceType =(typeof(SiteResource)), ErrorMessageResourceName = "RequiredName")]
        public string StudentName { get; set; }


        //[Display(Name = "StudentEmail", ResourceType = typeof(SiteResource))]
        [Required(ErrorMessageResourceType = (typeof(SiteResource)), ErrorMessageResourceName = "RequiredEmail")]
        [EmailAddress( ErrorMessageResourceType = (typeof(SiteResource)), ErrorMessageResourceName = "validationEmail")]
        public string StudentEmail { get; set; }

        [Required(ErrorMessageResourceType = (typeof(SiteResource)), ErrorMessageResourceName = "RequiredPhone")]
        [RegularExpression(@"^(\+(9627)[7-9]\d{7})$|^(07[7-9]\d{7})$",
                   ErrorMessageResourceType = (typeof(SiteResource)), ErrorMessageResourceName= "validationNumber")]
        public string StudentMobile { get; set; }

        
        public string StudentAddress { get; set; }




        //IFormatProvider eng = new CultureInfo("en").DateTimeFormat;
        //Birthday = Convert.ToDateTime(Birthday, en);
        //var usCulture = new System.Globalization.CultureInfo("en-US");
        //Console.WriteLine(DateTime.Now.ToString(usCulture.DateTimeFormat));

        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceType = (typeof(SiteResource)), ErrorMessageResourceName = "RequiredBirthday")]
        public Nullable<System.DateTime> Birthday { get; set; }
        
        
       

        public Nullable<int> Age { get; set; }

    }
}