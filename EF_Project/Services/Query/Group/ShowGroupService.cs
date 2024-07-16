using EF_Project.Concrets;
using EF_Project.Context;
using M = EF_Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EF_Project.Entity;

namespace EF_Project.Services.Query.Group
{
    public class ShowGroupService
    {
        private CourseContext _courseContext;

        public ShowGroupService(CourseContext courseContext)
        {
            _courseContext = courseContext;
        }

        public void ShowGroups()
        {
            if (!_courseContext.Groups.Any())
            {
                Console.WriteLine("There is no course");
                return;
            }

            Console.WriteLine($"{"Id",-20} {"Name",-20} {"Limit",-20} Begin Date");
            foreach (var group in _courseContext.Groups)
            {
                Console.WriteLine($"{group.Id,-20} {group.Name,-20} {group.Limit,-20} {group.BeginDate.ToShortDateString()}");
            }
        }

        public void ShowDetails()
        {
        IdLable: Messages.InputMessages("Group id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            M.Group? group = _courseContext.Groups.Include(x => x.Teacher).Include(x => x.Students.Where(y => !y.IsDelete)).FirstOrDefault(x => x.Id == id);

            if (group is null)
            {
                Messages.NotFound("Group");
                return;
            }

            Console.WriteLine($"{"Id",-20} {"Name",-20} {"Limit",-20} {"Teacher", -20} {"Begin Date", -20} {"End Date", -20} {"Students"}");
            Console.Write($"{group.Id,-20} {group.Name,-20} {group.Limit,-20} {group.Teacher?.Name ?? "No teacher",-20} {group.BeginDate.ToShortDateString(),-20} {group.EndDate.ToShortDateString(),-20}");

            if (group.Students.Count == 0)
                Console.WriteLine("No student");

            for (int i = 0; i < group.Students.Count; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine($"{group.Students.ToList()[i].Name}");
                    continue;
                }
                Console.WriteLine($"{group.Students.ToList()[i].Name,130}");
            }

        }
    }
}
