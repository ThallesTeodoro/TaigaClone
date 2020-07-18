using Taiga.Core.Entities;

namespace Taiga.Core.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        int CountBySlug(string slug);
    }
}
