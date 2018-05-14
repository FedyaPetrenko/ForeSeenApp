using System;
using System.Collections.Generic;
using System.Linq;
using ForeSeen.DataLayer.Entities;
using ForeSeen.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForeSeen.DataLayer.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        private readonly ApplicationDbContext _dbcontext;

        public MessageRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Message> GetAll()
        {
            return _dbcontext.Messages;
        }

        public Message Get(string id)
        {
            return _dbcontext.Messages.Find(Int32.Parse(id));
        }

        public void Create(Message item)
        {
            _dbcontext.Messages.Add(item);
        }

        public void Update(Message item)
        {
            _dbcontext.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Message> Find(Func<Message, bool> predicate)
        {
            return _dbcontext.Messages.Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            var message = _dbcontext.Messages.Find(Int32.Parse(id));
            if (message != null)
                _dbcontext.Messages.Remove(message);
        }
    }
}
