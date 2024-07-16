using EF_Project.Concrets;
using EF_Project.Context;
using EF_Project.Extensions;
using M = EF_Project.Entity;
using EF_Project.Services.Query.Group;
using Microsoft.EntityFrameworkCore;

namespace EF_Project.Services.Command.Student
{
    public class UpdateStudentService
    {
        private CourseContext _courseContext;
        private ShowGroupService _showGroupService;

        public UpdateStudentService(CourseContext courseContext, ShowGroupService showGroupService)
        {
            _courseContext = courseContext;
            _showGroupService = showGroupService;
        }

        public void UpdateStudent()
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
                Messages.NotFound("student");
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

        OpinionEmailLabel: Messages.Opinion("email", "change");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionEmailLabel;
            }

            string? newEmail = "";

            if (choice.Equals('y'))
            {
            NewEmailLabel: Messages.InputMessages("new email");
                newEmail = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newEmail))
                {
                    Messages.InvalidInput();
                    goto NewEmailLabel;
                }
            }

            DateTime newBirthDate = default;

        OpinionBirthLabel: Messages.Opinion("birth date", "change");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionBirthLabel;
            }


            if (choice.Equals('y'))
            {
            NewBirthLabel: Messages.InputMessages("new birth date");
                isSucceded = DateTime.TryParse(Console.ReadLine(), out newBirthDate);

                if (!isSucceded)
                {
                    Messages.InvalidInput();
                    goto NewBirthLabel;
                }

                if (DateTime.Now.Year - newBirthDate.Year < 18)
                {
                    Console.WriteLine("Student too young for the course");
                    goto NewBirthLabel;
                }
            }

        OpinionGroupLabel: Messages.Opinion("group", "change");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionGroupLabel;
            }

            int newGroupId = 0;

            if (choice.Equals('y'))
            {
                _showGroupService.ShowGroups();
            NewGroupLabel: Messages.InputMessages("new group id");
                isSucceded = int.TryParse(Console.ReadLine(), out newGroupId);

                if (!isSucceded)
                {
                    Messages.InvalidInput();
                    goto NewGroupLabel;
                }

                M.Group? group = _courseContext.Groups.Include(x => x.Students).FirstOrDefault(x => x.Id == newGroupId);

                if (group is null)
                {
                    Messages.NotFound("Group");
                    goto NewGroupLabel;
                }

                if (newGroupId != student.GroupId && group.Students.Count == group.Limit)
                {
                    Messages.NoSpace();
                    goto NewGroupLabel;
                }
            }


            if (newName != "")
                student.Name = newName;

            if (newSurname != "")
                student.Surname = newSurname;

            if (newEmail != "")
                student.Email = newEmail;

            if (newBirthDate != default)
                student.BirthDate = newBirthDate;

            if (newGroupId != 0)
                student.GroupId = newGroupId;


            _courseContext.Students.Update(student);

            try
            {
                _courseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Messages.ErrorOcured();
            }

            Messages.SuccessMessage("Student", "updated");
        }
    }
}
