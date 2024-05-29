using Stiffiner_Inspection.Contexts;

namespace Stiffiner_Inspection.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;

        public DataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
