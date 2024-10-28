namespace Day5_Assignment;
class Program
{
    static void Main(string[] args)
    {
        IUserpermission student = UserFactory.CreateUser("Student");
        LibraryService studentService = new LibraryService(student);
        studentService.PerformBorrowBook(1);  
        studentService.PerformReserveBook(1);
        IUserpermission teacher = UserFactory.CreateUser("Teacher");
        LibraryService teacherService = new LibraryService(teacher);
        teacherService.PerformBorrowBook(1);
        teacherService.PerformReserveBook(1);
        Console.ReadLine();
    }
}

