using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ForeSeen.BusinessLayer.Models;
using ForeSeen.DataLayer.Entities;
using ForeSeen.DataLayer.Interfaces;

namespace ForeSeen.BusinessLayer.Implementations
{
    public class UserService
    {
        private IUnitOfWork Database { get; set; }
        private static readonly ConcurrentBag<string>
            OnlineUsersId = new ConcurrentBag<string>();

        public UserService(IUnitOfWork database)
        {
            Database = database;
        }

        public void Login(string id)
        {
            OnlineUsersId.Add(id);
        }

        public void Logoff(string id)
        {
            OnlineUsersId.TryTake(out id);
        }

        public bool IsConnected(string id)
        {
            return OnlineUsersId.Contains(id);
        }

        public void EditUser(string id, UserModel user)
        {
            ApplicationUser usr = Database.Users.Find(
                us => us.Id == id).First();
            usr.UserName = user.Name;
            usr.LastName = user.LastName;
            usr.Email = user.Email;
            usr.UserImage = user.Image;
            usr.ImageMimeType = user.ImageMimeType;

            Database.Users.Update(usr);
            Database.Save();
        }

        public IEnumerable<ChannelModel> GetUserChannels(string id)
        {
            List<ChannelModel> channels = new List<ChannelModel>();
            foreach (var chl in Database.Users.Find(
                us => us.Id == id).First().Channels)
            {
                ChannelModel chlDTO = new ChannelModel
                {
                    ChannelId = chl.ChannelId.ToString(),
                    Name = chl.Name
                };
                channels.Add(chlDTO);
            }
            return channels;
        }

        public ChannelModel GetUserById(string id)
        {
            if (id == null)
                return new ChannelModel { Name = "Does not exist" };
            ApplicationUser usr = Database.Users.Find(
                 us => us.Id == id).First();
            if (usr == null)
                return new ChannelModel { Name = "Does not exist" };
            var usrDto = new ChannelModel
            {
                Name = usr.UserName,
                LastName = usr.LastName,
                Email = usr.Email,
                Id = usr.Id,
                ImageMimeType = usr.ImageMimeType,
                Image = usr.UserImage
            };
            return usrDto;
        }

        public IEnumerable<MessageModel> GetUserFavouriteMessages(string id)
        {
            List<MessageModel> messages = new List<MessageModel>();

            foreach (var msg in Database.Users.Find(
                us => us.Id == id).First().FavouriteMessages)
            {
                MessageModel msgDTO = new MessageModel
                {
                    Channel = new ChannelModel()
                    {
                        ChannelId = msg.Channel.ChannelId.ToString(),
                        Name = msg.Channel.Name
                    },
                    Content = msg.Content,
                    MessageId = msg.MessageId.ToString(),
                    SendTime = msg.SendTime.ToString("HH:mm:ss"),
                    User = new UserModel
                    {
                        Id = msg.User.Id,
                        Email = msg.User.Email,
                        Name = msg.User.UserName,
                        ImageMimeType = msg.User.ImageMimeType,
                        Image = msg.User.UserImage
                    }
                };
                messages.Add(msgDTO);
            }
            return messages;
        }

        public void RemoveUser(string id)
        {
            Database.Users.Delete(id);
            Database.Save();
        }

        public UserModel GetUserByName(string name)
        {
            ApplicationUser usr = Database.Users.Find(
                us => us.UserName == name).FirstOrDefault();
            UserModel usrDTO = new UserModel
            {
                Name = usr.UserName,
                LastName = usr.LastName,
                Email = usr.Email,
                Id = usr.Id,
                ImageMimeType = usr.ImageMimeType,
                Image = usr.UserImage
            };
            return usrDTO;
        }

        public void AddMessageToFavourites(string userId, string messageId)
        {
            ApplicationUser usr = Database.Users.Find(us => us.Id == userId).FirstOrDefault();
            Message message = Database.Messages.Find(m => m.MessageId.ToString() == messageId).FirstOrDefault();

            if (usr != null)
            {
                if (!usr.FavouriteMessages.Contains(message))
                {
                    usr.FavouriteMessages.Add(message);
                    Database.Users.Update(usr);
                    Database.Save();
                }
            }
        }

        public void SaveUserPhoto(string id, UserModel user)
        {
            ApplicationUser usr = Database.Users.Find(
                us => us.Id == id).First();

            usr.UserImage = user.Image;
            usr.ImageMimeType = user.ImageMimeType;

            Database.Users.Update(usr);
            Database.Save();
        }
    }
}
