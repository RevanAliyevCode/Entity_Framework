using EF_Project.Concrets;
using EF_Project.Context;
using EF_Project.Services.Command.Group;
using EF_Project.Services.Command.Student;
using EF_Project.Services.Command.Tecaher;
using EF_Project.Services.Query.Group;
using EF_Project.Services.Query.Student;
using EF_Project.Services.Query.Teacher;

CourseContext context = new();
AddTeacherService addTeacherService = new(context);
ShowTeacherService showTeacherService = new(context);
UpdateTeacherService updateTeacherService = new(context);
DeleteTeacherService deleteTeacherService = new(context);
AddGroupService addGroupService = new(context, showTeacherService);
ShowGroupService showGroupService = new(context);
UpdateGroupService updateGroupService = new(context, showTeacherService);
DeleteGroupService deleteGroupService = new(context);
AddStudentService addStudentService = new(context);
ShowStudentsService showStudentsService = new(context);
UpdateStudentService updateStudentService = new(context, showGroupService);
DeleteStudentService deleteStudentService = new(context);


while (true)
{
    Console.WriteLine("-----MENU-----");
    Console.WriteLine("0.Exit");
    Console.WriteLine("1.Add Teacher");
    Console.WriteLine("2.Show Teachers");
    Console.WriteLine("3.Show details of teacher");
    Console.WriteLine("4.Update Teacher");
    Console.WriteLine("5.Delete Teacher");
    Console.WriteLine("6.Add Group");
    Console.WriteLine("7.Show Groups");
    Console.WriteLine("8.Show details of group");
    Console.WriteLine("9.Update Group");
    Console.WriteLine("10.Delete Group");
    Console.WriteLine("11.Add Student");
    Console.WriteLine("12.Show Students");
    Console.WriteLine("13.Show details of student");
    Console.WriteLine("14.Update Student");
    Console.WriteLine("15.Delete Student");

    Console.Write("Write your choice: ");
    bool canConverte = int.TryParse(Console.ReadLine(), out int choice);


    if (canConverte)
    {
        switch ((Operations)choice)
        {
            case Operations.Exit:
                return;
            case Operations.AddTeacher:
                addTeacherService.AddTeacher();
                break;
            case Operations.ShowTeachers:
                showTeacherService.ShowTeachers();
                break;
            case Operations.ShowDetailsTeacher:
                showTeacherService.ShowDetails();
                break;
            case Operations.UpdateTeacher:
                showTeacherService.ShowTeachers();
                updateTeacherService.UpdateTeacher();
                break;
            case Operations.DeleteTeacher:
                showTeacherService.ShowTeachers();
                deleteTeacherService.DeleteTeacher();
                break;
            case Operations.AddGroup:
                addGroupService.AddGroup();
                break;
            case Operations.ShowGroups:
                showGroupService.ShowGroups();
                break;
            case Operations.ShowDetailsGroup:
                showGroupService.ShowDetails();
                break;
            case Operations.UpdateGroups:
                showGroupService.ShowGroups();
                updateGroupService.UpdateGroup();
                break;
            case Operations.DeleteGroup:
                showGroupService.ShowGroups();
                deleteGroupService.DeleteGroup();
                break;
            case Operations.AddStudent:
                showGroupService.ShowGroups();
                addStudentService.AddStudent();
                break;
            case Operations.ShowStudents:
                showStudentsService.ShowStudents();
                break;
            case Operations.ShowDetailsStudents:
                showStudentsService.ShowDetails();
                break;
            case Operations.UpdateStudent:
                showStudentsService.ShowStudents();
                updateStudentService.UpdateStudent();
                break;
            case Operations.DeleteStudent:
                showStudentsService.ShowStudents();
                deleteStudentService.DeleteStudent();
                break;
            default:
                Console.WriteLine("There is not a option like that");
                break;
        }
    }
    else
        Messages.InvalidInput();
}