using FCM.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TibiaMobileWeb
{
    public class PushNotification
    {
        public static string KEY = string.Empty;

        public async Task<int> Send(string registrationId, string title, string message)
        {
            if (string.IsNullOrEmpty(KEY))
            {
                return 0;
            }

            using (var sender = new Sender(KEY))
            {
                var _message = new FCM.Net.Message
                {
                    To = registrationId,
                    Notification = new Notification
                    {
                        Title = title,
                        Body = message,
                        Sound = "chime",
                    }
                };

                var result = await sender.SendAsync(_message);

                Console.WriteLine("[Notification Push] => " + result.MessageResponse.Success, ConsoleColor.White);
                return result.MessageResponse.Success;
            }
        }

        public async Task<HttpStatusCode> Send(List<string> registrationId, string title, string message)
        {
            if (string.IsNullOrEmpty(KEY))
            {
                Console.WriteLine("[FCMService] KEY EMPTY" + KEY, ConsoleColor.Red);
                return HttpStatusCode.NotImplemented;
            }

            if (registrationId.Count == 0)
            {
                Console.WriteLine("[FCMService] TOKENS EMPTY" + KEY, ConsoleColor.Red);
                return HttpStatusCode.NotImplemented;
            }

            using (var sender = new Sender(KEY))
            {
                var _message = new FCM.Net.Message
                {
                    RegistrationIds = registrationId,
                    Notification = new Notification
                    {
                        Title = title,
                        Body = message,
                        Sound = "chime",
                    }
                };

                var result = await sender.SendAsync(_message);

                return result.StatusCode;
            }
        }
    }
}
