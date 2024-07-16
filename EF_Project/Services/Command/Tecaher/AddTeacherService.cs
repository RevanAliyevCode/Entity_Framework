using EF_Project.Concrets;
using EF_Project.Context;
using EF_Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Project.Services.Command.Tecaher
{
    public class AddTeacherService
    {
        private CourseContext _context;

        public AddTeacherService(CourseContext context)
        {
            _context = context;
        }

        public void AddTeacher()
        {
        NameLabel: Messages.InputMessages("name");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Messages.InvalidInput();
                goto NameLabel;
            }

        SurnameNameLabel: Messages.InputMessages("surname");
            string? surname = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(surname))
            {
                Messages.InvalidInput();
                goto SurnameNameLabel;
            }

            Teacher teacher = new Teacher() { Name = name, Surname = surname };
            _context.Teachers.Add(teacher);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Messages.ErrorOcured();
            }
                Messages.SuccessMessage("Teacher", "added");
        } 
    }
}
