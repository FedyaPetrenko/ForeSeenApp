using System;
using System.Collections.Generic;
using System.Linq;
using ForeSeen.DataLayer.Entities;
using ForeSeen.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForeSeen.DataLayer.Repositories
{
    public class ChannelRepository : IRepository<Channel>
    {
        private readonly ApplicationDbContext _dbcontext;

        public ChannelRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Channel> GetAll()
        {
            return _dbcontext.Channels;
        }

        public Channel Get(string id)
        {
            return _dbcontext.Channels.Find(Int32.Parse(id));
        }

        public void Create(Channel item)
        {
            _dbcontext.Channels.Add(item);
        }

        public void Update(Channel item)
        {
            _dbcontext.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Channel> Find(Func<Channel, bool> predicate)
        {
            return _dbcontext.Channels.Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            var channel = _dbcontext.Channels.Find(Int32.Parse(id));
            _dbcontext.Messages.RemoveRange(channel.Messages);
            _dbcontext.SaveChanges();
            _dbcontext.Channels.Remove(channel);
        }
    }
}
