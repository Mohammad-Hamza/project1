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
         StudentContext _ORM = null;
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