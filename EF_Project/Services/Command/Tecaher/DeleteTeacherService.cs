using EF_Project.Concrets;
using EF_Project.Context;
using M = EF_Project.Entity;
using EF_Project.Extensions;
using EF_Project.Services.Query.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EF_Project.Services.Command.Tecaher
{
    public class DeleteTeacherService
    {
        private CourseContext _context;

        public DeleteTeacherService(CourseContext context)
        {
            _context = context;
        }

        public void DeleteTeacher()
        {
        IdLable: Messages.InputMessages("Teacher id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            M.Teacher? teacher = _context.Teachers.Find(id);

            if (teacher is null)
            {
                Messages.NotFound("Teacher");
                return;
            }

        OpinionLabel: Messages.Opinion("teacher", "delete");
            string? input = Console.ReadLine();
            isSucceded = char.TryParse(input, out char choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionLabel;
            }

            if (choice.Equals('y'))
            {
                teacher.IsDelete = true;
                _context.Teachers.Update(teacher);

                IQueryable<M.Group> groups = _context.Groups.Where(x => x.TeacherId == id);

                foreach (M.Group group in groups)
                {
                    group.Teacher = null;
                }

                _context.Groups.UpdateRange(groups);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Messages.ErrorOcured();
                }

                Messages.SuccessMessage("Teacher", "deleted");
            }
        }
    }
}
