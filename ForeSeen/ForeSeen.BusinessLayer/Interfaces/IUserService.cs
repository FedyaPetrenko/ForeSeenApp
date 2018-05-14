using System.Collections.Generic;
using ForeSeen.BusinessLayer.Models;

namespace ForeSeen.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        bool IsConnected(string id);
        void Login(string id);
        void Logoff(string id);
        void EditUser(string id, UserModel user);
        void RemoveUser(string id);
        UserModel GetUserById(string id);
        UserModel GetUserByName(string name);
        IEnumerable<ChannelModel> GetUserChannels(string id);
        IEnumerable<MessageModel> GetUserFavouriteMessages(string id);
        void AddMessageToFavourites(string id, string message);
        void SaveUserPhoto(string id, UserModel user);
    }
}
