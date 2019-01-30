using RequestHelperSample.Data.Context;
using RequestHelperSample.Data.Models;
using RequestHelperSample.Data.Repositories.Base;

namespace RequestHelperSample.Data.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
    }

    public class StudentRepository : EFRespository<Student>, IStudentRepository
    {
        public StudentRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
