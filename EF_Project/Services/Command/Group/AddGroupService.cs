using EF_Project.Concrets;
using EF_Project.Context;
using M = EF_Project.Entity;
using EF_Project.Services.Query.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Project.Services.Command.Group
{
    public class AddGroupService
    {
        private CourseContext _courseContext;
        private ShowTeacherService _showTeacherService;

        public AddGroupService(CourseContext courseContext, ShowTeacherService teacherService)
        {
            _courseContext = courseContext;
            _showTeacherService = teacherService;
        }

        public void AddGroup()
        {
        NameLabel: Messages.InputMessages("name");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Messages.InvalidInput();
                goto NameLabel;
            }

            var group = _courseContext.Groups.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            if (group is not null)
            {
                Messages.Exist("Group", name);
                goto NameLabel;
            }

        LimitLabel: Messages.InputMessages("limit");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int limit);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto LimitLabel;
            }

        BeginDateLabel: Messages.InputMessages("begin date (dd.MM.yyyy)");
            isSucceded = DateTime.TryParse(Console.ReadLine(), out DateTime beginDate);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto BeginDateLabel;
            }

        EndDateLabel: Messages.InputMessages("end date (dd.MM.yyyy)");
            isSucceded = DateTime.TryParse(Console.ReadLine(), out DateTime endDate);

            if (!isSucceded || beginDate.AddMonths(6) > endDate)
            {
                Messages.InvalidInput();
                goto EndDateLabel;
            }

            _showTeacherService.ShowTeachers();
        TeacherIdLabel: Messages.InputMessages("teacher id");
            isSucceded = int.TryParse(Console.ReadLine(), out int teacherId);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto TeacherIdLabel;
            }

            var teacher = _courseContext.Teachers.Find(teacherId);

            if (teacher is null)
            {
                Messages.NotFound("teacher");
                goto TeacherIdLabel;
            }

            M.Group newGroup = new()
            {
                Name = name,
                Limit = limit,
                BeginDate = beginDate,
                EndDate = endDate,
                TeacherId = teacherId,
            };

            _courseContext.Groups.Add(newGroup);

            try
            {
                _courseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Messages.ErrorOcured();
            }

            Messages.SuccessMessage("group", "added");
        }
    }
}
