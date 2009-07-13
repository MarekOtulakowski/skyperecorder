#region UsingDirectives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
#endregion

namespace skyperecorder.Library
{
    /// <summary>
    /// Class to manage MSSQL operation
    /// </summary>
    /// <remarks>
    /// example: adding, edit or delete messages
    /// </remarks>
    public class MSSQLDatabase: IOperate
    {
        #region Interface IOperate Members
        bool IOperate.AddMessage(ChatMessage oneMessage, TypeConversation typeConversation)
        {
            return true;
        }

        bool IOperate.DeleteChatMessage(ChatMessage oneMessage, TypeConversation typeConversation)
        {
            return true;
        }
        #endregion
    }
}
