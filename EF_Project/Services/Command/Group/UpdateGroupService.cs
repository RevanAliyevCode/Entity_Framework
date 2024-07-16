using EF_Project.Concrets;
using EF_Project.Context;
using M = EF_Project.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Project.Extensions;
using EF_Project.Services.Query.Teacher;

namespace EF_Project.Services.Command.Group
{
    public class UpdateGroupService
    {
        private CourseContext _courseContext;
        private ShowTeacherService _showTeacherService;

        public UpdateGroupService(CourseContext courseContext, ShowTeacherService showTeacherService)
        {
            _courseContext = courseContext;
            _showTeacherService = showTeacherService;
        }

        public void UpdateGroup()
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

        OpinionLimitLabel: Messages.Opinion("limit", "change");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionLimitLabel;
            }

            int newLimit = 0;

            if (choice.Equals('y'))
            {
            NewLimitLabel: Messages.InputMessages("new limit");
                isSucceded = int.TryParse(Console.ReadLine(), out newLimit);

                if (!isSucceded || newLimit < group.Students.Count)
                {
                    Messages.InvalidInput();
                    goto NewLimitLabel;
                }
            }

        OpinionTeacherLabel: Messages.Opinion("teacher", "change");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionTeacherLabel;
            }

            int newTeacherId = 0;

            if (choice.Equals('y'))
            {
                _showTeacherService.ShowTeachers();
            NewTeacherLabel: Messages.InputMessages("new teacher id");
                isSucceded = int.TryParse(Console.ReadLine(), out newTeacherId);

                if (!isSucceded)
                {
                    Messages.InvalidInput();
                    goto NewTeacherLabel;
                }

                if (!_courseContext.Teachers.Any(x => x.Id == newTeacherId))
                {
                    Messages.NotFound("Teacher");
                    goto NewTeacherLabel;
                }
            }

            DateTime newBeginDate = default;
            DateTime newEndDate = default;

        OpinionBeginLabel: Messages.Opinion("begin date", "change");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionBeginLabel;
            }


            if (choice.Equals('y'))
            {
            NewBeginLabel: Messages.InputMessages("new begin date");
                isSucceded = DateTime.TryParse(Console.ReadLine(), out newBeginDate);

                if (!isSucceded)
                {
                    Messages.InvalidInput();
                    goto NewBeginLabel;
                }
            }

        OpinionEndLabel: Messages.Opinion("end date", "change");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionEndLabel;
            }


            if (choice.Equals('y'))
            {
            NewEndLabel: Messages.InputMessages("new end date");
                isSucceded = DateTime.TryParse(Console.ReadLine(), out newEndDate);

                if (!isSucceded)
                {
                    Messages.InvalidInput();
                    goto NewEndLabel;
                }
            }


            if (newName != "")
                group.Name = newName;

            if (newLimit != 0)
                group.Limit = newLimit;

            if (newTeacherId != 0)
                group.TeacherId = newTeacherId;

            if (newBeginDate != default)
            {
                if (newBeginDate.AddMonths(6) > (newEndDate != default ? newEndDate : group.EndDate))
                    goto OpinionBeginLabel;
                group.BeginDate = newBeginDate;
            }

            if (newEndDate != default)
            {
                if ((newBeginDate != default ? newBeginDate : group.BeginDate) > newEndDate)
                    goto OpinionEndLabel;
                group.EndDate = newEndDate;
            }

            _courseContext.Groups.Update(group);

            try
            {
                _courseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Messages.ErrorOcured();
            }

            Messages.SuccessMessage("Group", "updated");
        }
    }
}
