using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Utlities
{    
        public enum MessageTypes
        {
            None = 0,
            Success = 1,
            Warning = 2,
            Error = 3,
            Information = 4,
        }

        public class Message
        {
            public string CurrentMessage { get; set; }
            public MessageTypes MessageType { get; set; }
            public List<string> MessageList { get; set; }
            public string RedirectUrl { get; set; }

            public Message()
            {
                CurrentMessage = string.Empty;
                MessageType = MessageTypes.None;
                MessageList = new List<string>();
                RedirectUrl = string.Empty;
            }
        }

        public class SetMessages
        {
            public static Message SetSuccessMessage(string message = "Data has been saved", List<string> lst = null)
            {
                var ret = new Message();

                ret.CurrentMessage = message;
                ret.MessageType = MessageTypes.Success;
                if (lst != null)
                {
                    ret.MessageList = lst;
                }

                return ret;
            }

            public static Message SetErrorMessage(string message = "An error Occurred", List<string> lst = null)
            {
                var ret = new Message();

                ret.CurrentMessage = message;
                ret.MessageType = MessageTypes.Error;
                if (lst != null)
                {
                    ret.MessageList = lst;
                }

                return ret;
            }

            public static Message SetWarningMessage(string message = "An error Occurred", List<string> lst = null)
            {
                var ret = new Message();

                ret.CurrentMessage = message;
                ret.MessageType = MessageTypes.Warning;
                if (lst != null)
                {
                    ret.MessageList = lst;
                }

                return ret;
            }

            public static Message SetInformationMessage(string message = "Information", List<string> lst = null)
            {
                var ret = new Message();

                ret.CurrentMessage = message;
                ret.MessageType = MessageTypes.Information;
                if (lst != null)
                {
                    ret.MessageList = lst;
                }

                return ret;
            }            
        }
    }
