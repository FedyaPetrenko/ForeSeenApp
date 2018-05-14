using System.Collections.Generic;

namespace ForeSeen.BusinessLayer.Models
{
    public class MessageModel
    {
        public string MessageId { get; set; }
        public string Content { get; set; }
        public string SendTime { get; set; }
        public UserModel User { get; set; }
        public ChannelModel Channel { get; set; }
        public List<MessageModel> Parents { get; set; }
    }
}
