using EF_Project.Concrets;
using EF_Project.Context;
using M = EF_Project.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Project.Services.Query.Student
{
    public class ShowStudentsService
    {
        private CourseContext _courseContext;

        public ShowStudentsService(CourseContext courseContext)
        {
            _courseContext = courseContext;
        }

        public void ShowStudents()
        {
            if (!_courseContext.Students.Any())
            {
                Console.WriteLine("There is no student");
                return;
            }

            Console.WriteLine($"{"Id",-20} {"Name",-20} {"Surname",-20}");
            foreach (var student in _courseContext.Students)
            {
                Console.WriteLine($"{student.Id,-20} {student.Name,-20} {student.Surname,-20}");
            }
        }

        public void ShowDetails()
        {
        IdLable: Messages.InputMessages("student id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            M.Student? student = _courseContext.Students.Include(x => x.Group).FirstOrDefault(x => x.Id == id);

            if (student is null)
            {
                Messages.NotFound("student");
                return;
            }
            Console.WriteLine($"{"Id",-20} {"Name",-20} {"Surname",-20} {"Email", -20} {"Birth Date", -20} {"Group"}");
            Console.WriteLine($"{student.Id,-20} {student.Name,-20} {student.Surname,-20} {student.Email,-20} {student.BirthDate.ToShortDateString(),-20} {student.Group.Name}");
        }
    }
}
