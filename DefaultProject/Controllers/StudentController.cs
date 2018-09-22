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
        public StudentContext _ORM = null;
        public StudentController(StudentContext ORM)
        {
            _ORM = ORM;
        }


        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(Students S)
        {
            _ORM.Students.Add(S);
                _ORM.SaveChanges();
            ViewBag.Message = "Done Successfully";
                return View();
        }


        [HttpPost]
        public IActionResult Index()
        {

            return View();
        }
    }
}