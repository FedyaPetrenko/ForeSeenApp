using System;
using System.Collections.Generic;
using System.Linq;
using ForeSeen.DataLayer.Entities;
using ForeSeen.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForeSeen.DataLayer.Repositories
{
    public class UserRepository : IRepository<ApplicationUser>
    {
        private readonly ApplicationDbContext _dbcontext;

        public UserRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _dbcontext.Users;
        }

        public ApplicationUser Get(string id)
        {
            return _dbcontext.Users.Find(id);
        }

        public void Create(ApplicationUser item)
        {
            _dbcontext.Users.Add(item);
        }

        public void Update(ApplicationUser item)
        {
            _dbcontext.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<ApplicationUser> Find(Func<ApplicationUser, bool> predicate)
        {
            return _dbcontext.Users.Include(u => u.Channels).Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            var user = _dbcontext.Users.Find(id);
            if (user != null)
                _dbcontext.Users.Remove(user);
        }
    }
}
