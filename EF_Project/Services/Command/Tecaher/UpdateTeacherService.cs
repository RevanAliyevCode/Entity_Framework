using EF_Project.Concrets;
using EF_Project.Context;
using EF_Project.Entity;
using EF_Project.Extensions;
using EF_Project.Services.Query.Teacher;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EF_Project.Services.Command.Tecaher
{
    public class UpdateTeacherService
    {
        private CourseContext _context;

        public UpdateTeacherService(CourseContext context)
        {
            _context = context;
        }

        public void UpdateTeacher()
        {
        IdLable: Messages.InputMessages("Teacher id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            Teacher? teacher = _context.Teachers.Find(id);

            if (teacher is null)
            {
                Messages.NotFound("Teacher");
                return;
            }

        OpinionLabel: Messages.Opinion("name", "change");
            string? input = Console.ReadLine();
            isSucceded = char.TryParse(input, out char choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionLabel;
            }

            string? newName = "";

            if (choice.Equals('y'))
            {
            NewNameLabel: Messages.InputMessages("new name");
                newName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newName))
                {
                    Messages.InvalidInput();
                    goto NewNameLabel;
                }
            }

        OpinionSurnameLabel: Messages.Opinion("surname", "change");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionSurnameLabel;
            }

            string? newSurname = "";

            if (choice.Equals('y'))
            {
            NewSurnameLabel: Messages.InputMessages("new surname");
                newSurname = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newSurname))
                {
                    Messages.InvalidInput();
                    goto NewSurnameLabel;
                }
            }

            if (newName != "")
                teacher.Name = newName;

            if (newSurname != "")
                teacher.Surname = newSurname;

            _context.Teachers.Update(teacher);
            try
            {
                _context.SaveChanges();
                if (newName != "" || newSurname != "")
                {
                    Messages.SuccessMessage("Tecaher", "updated");
                }
            }
            catch (Exception ex)
            {
                Messages.ErrorOcured();
            }
        }
    }
}
