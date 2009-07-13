#region UsingDirectives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
#endregion

namespace skyperecorder.Library
{
    /// <summary>
    /// Class to manage file operation
    /// </summary>
    /// <remarks>
    /// example: adding, edit or delete messages
    /// </remarks>
    public class FileDatabase: IOperate
    {
        /// <summary>
        /// Number of successfull save messages
        /// </summary>
        public static int CountChatMessage = 0; 
        /// <summary>
        /// last ErrorCode
        /// </summary>
        public string ErrorCode { get; set; }

        #region Interface IOperate Members
        /// <summary>
        /// Add Message
        /// </summary>
        /// <param name="oneMessage">One message (text, voice, video)</param>
        /// <param name="typeConversation">Type conversation (Enum TypeConversation)</param>
        /// <returns>true if add successfull</returns>
        bool IOperate.AddMessage(ChatMessage oneMessage, TypeConversation typeConversation)
        {
            bool result = false;
            try
            {
                //Chat
                if (typeConversation == TypeConversation.Chat)
                {
                    FileStream fs = new FileStream(Manager.tempChatDirectory + "\\fileDatabase.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter m_streamWriter = new StreamWriter(fs);
                    m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);

                    //sender
                    m_streamWriter.WriteLine("Sender: " + oneMessage.Sender);
                    
                    //recipients
                    string recipients = string.Empty;
                    foreach (string oneRecipient in oneMessage.Recipients)
                    {
                        if (!string.IsNullOrEmpty(recipients))
                        {
                            recipients += ", ";
                        }
                        recipients += oneRecipient;
                    }
                    m_streamWriter.WriteLine("Recipients: " + recipients);
                    
                    //information
                    m_streamWriter.WriteLine("Date: " + oneMessage.Date.ToString());
                    m_streamWriter.WriteLine("Message: " + oneMessage.Message);
                    m_streamWriter.WriteLine("");
                    m_streamWriter.Flush();
                    m_streamWriter.Close();

                    CountChatMessage++;
                    result = true;
                }

                //Voice
                else if (typeConversation == TypeConversation.Audio)
                {
                    //not implement
                }

                //Video
                else if (typeConversation == TypeConversation.Video)
                {
                    //not implement
                }
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Delete Message
        /// </summary>
        /// <param name="oneMessage">One message (text, voice, video)</param>
        /// <param name="typeConversation">Type conversation (Enum TypeConversation)</param>
        /// <returns>true if add successfull</returns>
        bool IOperate.DeleteChatMessage(ChatMessage oneMessage, TypeConversation typeConversation)
        {
            bool result = false;
            try
            {
                //Chat
                if (typeConversation == TypeConversation.Chat)
                {
                    //not implement
                    //CountChatMessage--;
                    //result = true;
                }

                //Voice
                else if (typeConversation == TypeConversation.Audio)
                {
                    //not implement
                }

                //Video
                else if (typeConversation == TypeConversation.Video)
                {
                    //not implement
                }
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            return result;
        }
        #endregion
    }
}
