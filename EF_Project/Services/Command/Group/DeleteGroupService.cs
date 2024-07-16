using EF_Project.Concrets;
using EF_Project.Context;
using M = EF_Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Project.Extensions;
using System.Xml;

namespace EF_Project.Services.Command.Group
{
    public class DeleteGroupService
    {
        private CourseContext _courseContext;

        public DeleteGroupService(CourseContext courseContext)
        {
            _courseContext = courseContext;
        }

        public void DeleteGroup()
        {
        IdLable: Messages.InputMessages("Group id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            M.Group? group = _courseContext.Groups.Find(id);

            if (group is null)
            {
                Messages.NotFound("Group");
                return;
            }

        OpinionLabel: Messages.Opinion("group", "delete");
            string? input = Console.ReadLine();
            isSucceded = char.TryParse(input, out char choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionLabel;
            }

            if (choice.Equals('y'))
            {
                group.IsDelete = true;

                IQueryable<M.Student> students = _courseContext.Students.Where(x => x.GroupId == id);

                foreach (M.Student student in students)
                {
                    student.IsDelete = true;
                }

                _courseContext.Groups.Update(group);
                _courseContext.Students.UpdateRange(students);

                try
                {
                    _courseContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Messages.ErrorOcured();
                }

                Messages.SuccessMessage("group", "deleted");
            }
        }
    }
}
