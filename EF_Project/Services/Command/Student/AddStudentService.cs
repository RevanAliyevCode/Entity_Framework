using EF_Project.Concrets;
using EF_Project.Context;
using M = EF_Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Project.Entity;
using Microsoft.EntityFrameworkCore;

namespace EF_Project.Services.Command.Student
{
    public class AddStudentService
    {
        private CourseContext _courseContext;

        public AddStudentService(CourseContext courseContext)
        {
            _courseContext = courseContext;
        }

        public void AddStudent()
        {
        IdLable: Messages.InputMessages("Group id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            M.Group? group = _courseContext.Groups.Include(x => x.Students).FirstOrDefault(x => x.Id == id);

            if (group is null)
            {
                Messages.NotFound("Group");
                return;
            }

            if (group.Students.Count == group.Limit)
            {
                Messages.NoSpace();
                return;
            }

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

        EmailLabel: Messages.InputMessages("email");
            string? email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                Messages.InvalidInput();
                goto EmailLabel;
            }

        BirthDateLabel: Messages.InputMessages("birth date (dd.MM.yyyy student must be 18 or older)");
            isSucceded = DateTime.TryParse(Console.ReadLine(), out DateTime birthDate);
            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto BirthDateLabel;
            }

            if (DateTime.Now.Year - birthDate.Year < 18)
            {
                Console.WriteLine("Student too young for the course");
                goto BirthDateLabel;
            }

            M.Student student = new()
            {
                Name = name,
                Surname = surname,
                Email = email,
                BirthDate = birthDate,
                GroupId = id
            };

            _courseContext.Students.Add(student);

            try
            {
                _courseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Messages.ErrorOcured();
            }

            Messages.SuccessMessage("student", "created");
        }
    }
}
