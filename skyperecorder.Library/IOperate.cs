﻿#region UsingDirectores
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace skyperecorder.Library
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

        //delete
        bool DeleteMessage(ChatMessage oneMessage, TypeConversation typeConversation);
    }
}
