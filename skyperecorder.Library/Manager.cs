#region UsingDirectives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
#endregion

namespace skyperecorder.library
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
        /// not availabe difference don't distrub
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
        string tempChatDirectory = System.Environment.GetEnvironmentVariable("TEMP") + "\\tempChat";
        string tempVoiceDirectory = System.Environment.GetEnvironmentVariable("TEMP") + "\\tempVoice";
        string tempVideoDirectory = System.Environment.GetEnvironmentVariable("TEMP") + "\\tempVideo";

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Manager()
        {
        }
    }
}
