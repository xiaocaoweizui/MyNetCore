
namespace MyNetEFCore
{
    public class StudentRepository : Repository<Student, int, StudentContext>, IStudentRepository
    {
        public StudentRepository(StudentContext context) : base(context)
        {
        }
    }
}
