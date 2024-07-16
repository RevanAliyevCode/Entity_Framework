using EF_Project.Concrets;
using EF_Project.Context;
using EF_Project.Extensions;
using M = EF_Project.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Project.Services.Command.Student
{
    public class DeleteStudentService
    {
        private CourseContext _courseContext;

        public DeleteStudentService(CourseContext courseContext)
        {
            _courseContext = courseContext;
        }

        public void DeleteStudent()
        {
        IdLable: Messages.InputMessages("student id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            M.Student? student = _courseContext.Students.Find(id);

            if (student is null)
            {
                Messages.NotFound("Student");
                return;
            }

        OpinionLabel: Messages.Opinion("student", "delete");
            string? input = Console.ReadLine();
            isSucceded = char.TryParse(input, out char choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionLabel;
            }

            if (choice.Equals('y'))
            {
                student.IsDelete = true;
                _courseContext.Students.Update(student);

                try
                {
                    _courseContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Messages.ErrorOcured();
                }

                Messages.SuccessMessage("Student", "deleted");
            }
        }
    }
}
