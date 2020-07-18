using Taiga.Core.Entities;
using Taiga.Core.Interfaces;
using Taiga.Infrastructure.Data;
using System.Linq;

namespace Taiga.Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(TaigaContext context) : base(context){}

        public int CountBySlug(string slug)
        {
            return dbSet.Where(p => p.Slug == slug)
                        .Count();
        }
    }
}
