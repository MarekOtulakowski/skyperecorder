#region UsingDirectores
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace skyperecorder.library
{
    /// <summary>
    /// type of kind skype communication
    /// </summary>
    public enum TypeConversation
    {
        /// <summary>
        /// text conversation
        /// </summary>
        Chat,

        /// <summary>
        /// audio conversation
        /// </summary>
        Audio,

        /// <summary>
        /// video conversation
        /// </summary>
        Video
    }

    /// <summary>
    /// Interface to different database like as (database, file, etc.)
    /// </summary>
    public interface IOperate
    {
        //add
        bool AddMessage(ChatMessage oneMessage, TypeConversation typeConversation);

        //edit
        bool EditMessage(ChatMessage oneMessage, TypeConversation typeConversation);

        //delete
        bool DeleteChatMessage(ChatMessage oneMessage, TypeConversation typeConversation);

        //information
        int CountMessage(TypeConversation typeConversation);
    }
}
