#region UsingDirectives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using SKYPE4COMLib;
using System.Threading;
#endregion

namespace skyperecorder.Library
{
    /// <summary>
    /// Status of Skype
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// connected and fully operate
        /// </summary>
        Online,

        /// <summary>
        /// not availabe difference don't distrub or I'm away
        /// </summary>
        NotAvailable,

        /// <summary>
        /// hidding
        /// </summary>
        Invisibe,

        /// <summary>
        /// not connected
        /// </summary>
        Offline
    }

    /// <summary>
    /// Class to manage function and action Skype
    /// </summary>
    public class Manager
    {
        #region Variable
        string tempChatDirectory = System.Environment.GetEnvironmentVariable("TEMP") + "\\tempChat";
        string tempVoiceDirectory = System.Environment.GetEnvironmentVariable("TEMP") + "\\tempVoice";
        string tempVideoDirectory = System.Environment.GetEnvironmentVariable("TEMP") + "\\tempVideo";
        public string ErrorCode { get; set; } 
        public static SKYPE4COMLib.Skype skype = new SKYPE4COMLib.Skype();
        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Manager()
        {
            //attach to skype API
            ConnectToSkype();

            //attach to skype events
            ConnectToSkypeEvents();
        }

        /// <summary>
        /// Default Desctructor
        /// </summary>
        ~Manager()
        {
            //disconnect from skype status
            DisconnectFromSkypeEvents();

            //free comm api (not required but know)
            FreeSkypeCOM();
        }

        /// <summary>
        /// Disconnect from Skype Events
        /// </summary>
        /// <returns>true if success</returns>
        private bool DisconnectFromSkypeEvents()
        {
            bool result = false;
            try
            {
                ((_ISkypeEvents_Event)skype).MessageStatus -= ChangeMessageStatus;
                result = true;
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Connect to Skype Events
        /// </summary>
        /// <returns>true is all success</returns>
        private bool ConnectToSkypeEvents()
        {
            List<bool> listOfResult = new List<bool>();
            try
            {
                ((_ISkypeEvents_Event)skype).MessageStatus += ChangeMessageStatus;
                listOfResult.Add(true);
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
                listOfResult.Add(false);
            }
            bool result = true;
            foreach (bool oneResult in listOfResult)
            {
                if (!oneResult)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Change status message
        /// </summary>
        /// <remarks>
        /// Chat messages
        /// </remarks>
        /// <param name="chatmessage">talk to person (always) one person</param>
        /// <param name="status">status of talk</param>
        private void ChangeMessageStatus(SKYPE4COMLib.ChatMessage chatmessage, SKYPE4COMLib.TChatMessageStatus status)
        {
            try
            {
                //chat receive
                if ((status == TChatMessageStatus.cmsReceived) &&
                    (chatmessage.Type == TChatMessageType.cmeSaid)) 
                {
                    //to write
                    //create and save (text, chat) message
                }

                //chat send
                else if ((status == TChatMessageStatus.cmsSending) &&
                         (chatmessage.Type == TChatMessageType.cmeSaid))
                {
                    //to write
                    //create and save (text, chat) message
                }
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
        }

        /// <summary>
        /// Disconnect from Skype API
        /// </summary>
        /// <returns>true is success</returns>
        private bool FreeSkypeCOM()
        {
            bool result = false;
            try
            {
                Marshal.ReleaseComObject(skype);
                GC.SuppressFinalize(skype);
                skype = null;
                result = true;
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Connect to Skype API
        /// </summary>
        /// <remarks>
        /// In first using requere allow program acces to Skype program for user operate
        /// </remarks>
        /// <returns>true is success</returns>
        private bool ConnectToSkype()
        {
            bool result = false;
            try
            {
                //check Is skype is running?
                if (!skype.Client.IsRunning)
                {
                    //If not running this
                    skype.Client.Start(true, false);
                }

                //attach to API
                //http://forum.skype.com/index.php?showtopic=165791
                //https://developer.skype.com/Docs/ApiDoc/Skype_protocol
                skype.Attach(skype.Protocol, true);

                result = true;
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            return result;
        }
    }
}
