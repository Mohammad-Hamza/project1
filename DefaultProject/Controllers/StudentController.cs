using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefaultProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace DefaultProject.Controllers
{
    public class StudentController : Controller
    {
        public StudentContext ORM = null;
        public StudentController(StudentContext ORM)
        {
            this.ORM = ORM;
        }


        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(Students S)
        {
            ORM.Students.Add(S);
                ORM.SaveChanges();
            ViewBag.Message = "Done Successfully";
                return View();
        }


        [HttpPost]
        public IActionResult Index()
        {

            return View();
        }

       
        public IActionResult AllStudent()
        {
            IList<Students> AllStudents = ORM.Students.ToList<Students>();
            return View(AllStudents);
        }

       


    }
}