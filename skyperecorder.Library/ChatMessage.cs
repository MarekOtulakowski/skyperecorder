#region UsingDirectives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace skyperecorder.Library
{
    /// <summary>
    /// Class to store one message chat (text)
    /// </summary>
    public class ChatMessage
    {
        public string Sender { get; set; }
        public List<string> Recipients { get; set; }
        public string Message { get; set; }
        public string IdChat { get; set; }

        /// <summary>
        /// One Chat Message
        /// </summary>
        /// <param name="_sender">Sender (always one)</param>
        /// <param name="_recipients">Recipients (via 1-1 one, conference many)</param>
        /// <param name="_message">Message</param>
        /// <param name="_idChat">Unique Id Chat</param>
        public ChatMessage(string _sender,
                           List<string> _recipients,
                           string _message,
                           string _idChat)
        {
            Sender = _sender;
            Recipients = _recipients;
            Message = _message;
            IdChat = _idChat;
        }
    }
}
