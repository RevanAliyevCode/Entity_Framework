using EF_Project.Concrets;
using EF_Project.Context;
using M = EF_Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EF_Project.Services.Query.Teacher
{
    public class ShowTeacherService
    {
        private CourseContext _context;

        public ShowTeacherService(CourseContext context)
        {
            _context = context;
        }

        public void ShowTeachers()
        {
            if (!_context.Teachers.Any())
            {
                Console.WriteLine("There is no teacher");
                return;
            }

            Console.WriteLine($"{"Id",-20} {"Name",-20} {"Surname",-20}");
            foreach (var teacher in _context.Teachers)
            {
                Console.WriteLine($"{teacher.Id,-20} {teacher.Name,-20} {teacher.Surname,-20}");
            }
        }

        public void ShowDetails()
        {
        IdLable: Messages.InputMessages("Teacher id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            M.Teacher? teacher = _context.Teachers.Include(x => x.Groups).FirstOrDefault(x => x.Id == id);

            if (teacher is null)
            {
                Messages.NotFound("Teacher");
                return;
            }
            Console.WriteLine($"{"Id",-20} {"Name",-20} {"Surname",-20} {"Groups"}");
            Console.Write($"{teacher.Id,-20} {teacher.Name,-20} {teacher.Surname,-20}");

            if (teacher.Groups.Count == 0)
                Console.WriteLine("No group");

            for (int i = 0; i < teacher.Groups.Count; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine($"{teacher.Groups.ToList()[i].Name}");
                    continue;
                }
                Console.WriteLine($"{teacher.Groups.ToList()[i].Name, 70}");
            }
        }
    }
}
