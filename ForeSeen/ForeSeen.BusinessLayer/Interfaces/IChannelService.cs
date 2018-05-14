using System.Collections.Generic;
using ForeSeen.BusinessLayer.Models;

namespace ForeSeen.BusinessLayer.Interfaces
{
    public interface IChannelService
    {
        bool IsConnected(string id, UserModel user);
        bool IsExistById(string id);
        bool IsExistByName(string name);
        void AddChannel(ChannelModel channel);
        void AddUser(string id, UserModel user);
        void AddMessage(string id, MessageModel message);
        ChannelModel GetChannelById(string id);
        ChannelModel GetChannelByName(string id);
        IEnumerable<UserModel> GetAllUsers(string id);
        IEnumerable<MessageModel> GetAllMessages(string id);
        void EditMessage(string id, MessageModel message);
        void RenameChannel(string id, ChannelModel channel);
        void RemoveChannel(string id);
        void RemoveUser(string id, UserModel user);
        void RemoveMessage(string id, MessageModel message);
    }
}
