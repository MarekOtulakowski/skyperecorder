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
        public string tempChatDirectory = System.Environment.GetEnvironmentVariable("TEMP") + "\\tempChat";
        public string tempVoiceDirectory = System.Environment.GetEnvironmentVariable("TEMP") + "\\tempVoice";
        public string tempVideoDirectory = System.Environment.GetEnvironmentVariable("TEMP") + "\\tempVideo";
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

            //create temp directory
            CreateTempDirectory();

            //attach to skype events
            ConnectToSkypeEvents();
        }

        /// <summary>
        /// Create temp directory if not exist
        /// </summary>
        /// <returns>true if all success</returns>
        private bool CreateTempDirectory()
        {
            bool result = false;
            try
            {
                if (!Directory.Exists(tempChatDirectory))
                {
                    Directory.CreateDirectory(tempChatDirectory);
                }
                if (!Directory.Exists(tempVoiceDirectory))
                {
                    Directory.CreateDirectory(tempVoiceDirectory);
                }
                if (!Directory.Exists(tempVideoDirectory))
                {
                    Directory.CreateDirectory(tempVideoDirectory);
                }
                result = true;
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            return result;
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
                //chat events
                ((_ISkypeEvents_Event)skype).MessageStatus -= ChangeMessageStatus;

                //voice event
                ((_ISkypeEvents_Event)skype).CallStatus -= ChangeCallStatus;

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
            try
            {
                ((_ISkypeEvents_Event)skype).CallStatus += ChangeCallStatus;
                listOfResult.Add(true);
            }
            catch (Exception ex)
            {
                ErrorCode += ";" + ex.Message;
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
        /// Change status call (voice)
        /// </summary>
        /// <remarks>
        /// Voice message
        /// </remarks>
        /// <param name="call">talk to person</param>
        /// <param name="status">status of talk</param>
        private void ChangeCallStatus(Call call, TCallStatus status)
        {
            try
            {
                //call is now calling
                if (status == TCallStatus.clsRinging)
                {
                    //this person calling -> call.PartnerDisplayName;
                }

                //press disallow
                else if (status == TCallStatus.clsRefused)
                {

                }

                //press stop
                else if (status == TCallStatus.clsCancelled)
                {

                }

                //talk is now progress
                else if (status == TCallStatus.clsInProgress)
                {
                    //record voice
                    call.set_CaptureMicDevice(TCallIoDeviceType.callIoDeviceTypeFile, tempVoiceDirectory + "\\" + "out.wav");
                    call.set_OutputDevice(TCallIoDeviceType.callIoDeviceTypeFile, tempVoiceDirectory + "\\" + "in.wav");
                    call.set_CaptureMicDevice(TCallIoDeviceType.callIoDeviceTypeFile, tempVoiceDirectory + "\\" + "capture.wav");
                }

                //call is finish (now can wav convert to mp3)
                else if (status == TCallStatus.clsFinished)
                {

                }
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
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
