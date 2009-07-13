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
    /// Kind of store
    /// </summary>
    /// <remarks>
    /// file, database(MSSQL), etc...
    /// </remarks>
    public enum KindOfStore
    {
        /// <summary>
        /// File
        /// </summary>
        KoS_File,

        /// <summary>
        /// Database MSSQL
        /// </summary>
        KoS_DatabaseMSSQL
    }

    /// <summary>
    /// Class to manage function and action Skype
    /// </summary>
    public class Manager
    {
        #region Variable
        public static string tempChatDirectory = System.Environment.GetEnvironmentVariable("TEMP") + "\\tempChat";
        public static string tempVoiceDirectory = System.Environment.GetEnvironmentVariable("TEMP") + "\\tempVoice";
        public static string tempVideoDirectory = System.Environment.GetEnvironmentVariable("TEMP") + "\\tempVideo";
        public string ErrorCode { get; set; } 
        public static SKYPE4COMLib.Skype skype = new SKYPE4COMLib.Skype();
        private IOperate interfaceToOperate = null;
        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="kindOfStore">Enum KindOfStore</param>
        /// <param name="pathOrConnectionString">Path to file or ConnectionString to MSSQL</param>
        public Manager(KindOfStore kindOfStore, string pathOrConnectionString)
        {
            //set temporary paths
            tempChatDirectory = pathOrConnectionString + "\\tempChat";
            tempVoiceDirectory = pathOrConnectionString + "\\tempVoice";
            tempVideoDirectory = pathOrConnectionString + "\\tempVideo";

            //set store
            switch (kindOfStore)
            {
                case KindOfStore.KoS_File:
                    interfaceToOperate = new FileDatabase();
                    break;
                case KindOfStore.KoS_DatabaseMSSQL:
                    interfaceToOperate = new MSSQLDatabase();
                    break;
                default:
                    interfaceToOperate = new FileDatabase();
                    break;
            }

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
                    string uniqueLabel = string.Empty;
                    uniqueLabel = DateTime.Now.ToString();
                    uniqueLabel = uniqueLabel.Replace(":", "");
                    uniqueLabel = uniqueLabel.Replace(" ", "");
                    uniqueLabel = uniqueLabel.Replace("-", "");

                    call.set_CaptureMicDevice(TCallIoDeviceType.callIoDeviceTypeFile, tempVoiceDirectory + "\\" + uniqueLabel + "speaker.wav");
                    call.set_CaptureMicDevice(TCallIoDeviceType.callIoDeviceTypeFile, tempVoiceDirectory + "\\" + uniqueLabel + "microfon.wav");
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
                    List<string> listOfRecipients = new List<string>();
                    if (chatmessage.Chat.Type == TChatType.chatTypeMultiChat)
                    {
                        //conferenc talk > 2 users
                        UserCollection collectionOfUsers = chatmessage.Chat.Members as UserCollection;
                        foreach (User oneUser in collectionOfUsers)
                        {
                            listOfRecipients.Add(oneUser.Handle);
                        }
                    }
                    else
                    {
                        //pair talk < 3 users
                        listOfRecipients.Add(skype.CurrentUser.Handle);
                    }
                    ChatMessage receiveChatMessage = new ChatMessage(chatmessage.FromHandle,
                                                                     listOfRecipients,
                                                                     chatmessage.Body,
                                                                     chatmessage.Timestamp,
                                                                     chatmessage.Chat.Blob);
                    interfaceToOperate.AddMessage(receiveChatMessage, TypeConversation.Chat);
                }

                //chat send
                else if ((status == TChatMessageStatus.cmsSending) &&
                         (chatmessage.Type == TChatMessageType.cmeSaid))
                {
                    List<string> listOfRecipients = new List<string>();
                    string recipient = string.Empty;
                    if (chatmessage.Chat.Type == TChatType.chatTypeMultiChat)
                    {
                        //conferenc talk > 2 users
                        UserCollection collectOfUsers = chatmessage.Chat.Members as UserCollection;
                        foreach (User oneUser in collectOfUsers)
                        {
                            if (oneUser.Handle != skype.CurrentUser.Handle)
                            {
                                //if itsn't me
                                listOfRecipients.Add(oneUser.Handle);
                            }
                        }
                    }
                    else
                    {
                        //talk in one person
                        recipient = chatmessage.Chat.DialogPartner;
                        listOfRecipients.Add(recipient);
                    }
                    ChatMessage receiveChatMessage = new ChatMessage(chatmessage.FromHandle,
                                                                     listOfRecipients,
                                                                     chatmessage.Body,
                                                                     chatmessage.Timestamp,
                                                                     chatmessage.Chat.Blob);
                    interfaceToOperate.AddMessage(receiveChatMessage, TypeConversation.Chat);
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
