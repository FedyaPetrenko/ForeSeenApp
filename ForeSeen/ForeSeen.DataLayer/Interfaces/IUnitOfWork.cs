using System.Threading.Tasks;
using ForeSeen.DataLayer.Entities;

namespace ForeSeen.DataLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Channel> Channels { get; }
        IRepository<Message> Messages { get; }
        IRepository<ApplicationUser> Users { get; }
        Task Save();
    }
}
