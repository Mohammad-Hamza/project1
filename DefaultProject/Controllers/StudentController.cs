using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using DefaultProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DefaultProject.Controllers
{
    public class StudentController : Controller
    {
         StudentContext _ORM = null;
         IHostingEnvironment _ENV = null;
        public StudentController(StudentContext ORM, IHostingEnvironment ENV)
        {
            _ORM = ORM;
            _ENV=ENV;
        }


        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(Students S, IFormFile Resume)
        {
            string wwwRootPath = _ENV.WebRootPath;
           
          

            string CVPath = "/WebData/CVs/" + Guid.NewGuid().ToString() + Path.GetExtension(Resume.FileName);
            FileStream CVS = new FileStream(wwwRootPath + CVPath, FileMode.Create);
            Resume.CopyTo(CVS);
            CVS.Close();
            S.CV = CVPath;


            _ORM.Students.Add(S);
            _ORM.SaveChanges();

            //
            //Email object

            MailMessage oEmail = new MailMessage();
            oEmail.From = new MailAddress("butthamza189@gmail.com");
            oEmail.To.Add(new MailAddress(S.Email));
            oEmail.CC.Add(new MailAddress("XXXX@XXXX.com"));
            oEmail.Subject = "Welcome to ABC";
            oEmail.Body = "Dear " + S.Name + ",<br><br>" +
                "Thanks for registering with ABC, We are glad to have you in our system." +
                "<br><br>" +
                "<b>Regards</b>,<br>ABC Team";
            oEmail.IsBodyHtml = true;
            if (!string.IsNullOrEmpty(S.CV))
            {
                oEmail.Attachments.Add(new Attachment(wwwRootPath + S.CV));
            }

           
        

            //smtp object
            SmtpClient oSMTP = new SmtpClient();
            oSMTP.Host = "smtp.gmail.com";
            oSMTP.Port = 587; //465 //25
            oSMTP.EnableSsl = true;
            oSMTP.Credentials = new System.Net.NetworkCredential("XXXXX@gmail.com", "XXXXX");

            try
            {
                oSMTP.Send(oEmail);
            }
            catch (Exception ex)
            {

            }


            //

            _ORM.Students.Add(S);
                _ORM.SaveChanges();
            ViewBag.Message = "Done Successfully";
                return View();
        }


        public IActionResult StudentDetail(int Id)
        {
            Students S = _ORM.Students.Where(m => m.Id == Id).FirstOrDefault<Students>();
            return View(S);
        }

        [HttpGet]
        public IActionResult EditStudent(int Id)
        {

            Students S = _ORM.Students.Where(m => m.Id == Id).FirstOrDefault<Students>();
            return View(S);
        }
        [HttpPost]
        public IActionResult EditStudent(Students S)
        {
            _ORM.Students.Update(S);
            _ORM.SaveChanges();
            
            return RedirectToAction("AllStudent");
        }

        public IActionResult DeleteStudent(Students S)
        {
            
            _ORM.Students.Remove(S);
            _ORM.SaveChanges();
            
            return RedirectToAction("AllStudent");
        }

        [HttpGet]
        public IActionResult AllStudent()
        {
            IList<Students> AllStudents = _ORM.Students.ToList<Students>();
            return View(AllStudents);
        }
        [HttpPost]
        public IActionResult AllStudent(string SearchByName, string SearchByDepartment, string SearchByRollNo)
        {

            IList<Students> AllStudent = _ORM.Students.Where(m => m.Name.Contains(SearchByName) || m.Department.Contains(SearchByName) || m.RollNo.Contains(SearchByName)).ToList<Students>();
            return View(AllStudent);
        }



    }
}