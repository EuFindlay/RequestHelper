using RequestHelperSample.Data.Context;
using RequestHelperSample.Data.Models;
using RequestHelperSample.Data.Repositories.Base;

namespace RequestHelperSample.Data.Repositories
{
    public interface IGradeRepository : IRepository<Grade>
    {
    }

    public class GradeRepository : EFRespository<Grade>, IGradeRepository
    {
        public GradeRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
