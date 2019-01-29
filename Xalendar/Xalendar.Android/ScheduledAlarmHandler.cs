using System.IO;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.Support.V4.App;

namespace Xalendar.Droid
{
    /// <summary>
    /// Broadcast receiver
    /// </summary>
    [BroadcastReceiver(Enabled = true, Label = "Local Notifications Plugin Broadcast Receiver")]
    public class ScheduledAlarmHandler : BroadcastReceiver
    {
        /// <summary>
        /// 
        /// </summary>
        public const string LocalNotificationKey = "LocalNotification";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="intent"></param>
        public override void OnReceive(Context context, Intent intent)
        {
            var extra = intent.GetStringExtra(LocalNotificationKey);
            new NotificationAndroid().Show(
                DeserializeNotification(extra)
                );

        }

        private MyNotification DeserializeNotification(string notificationString)
        {
            var xmlSerializer = new XmlSerializer(typeof(MyNotification));
            using (var stringReader = new StringReader(notificationString))
            {
                var notification = (MyNotification)xmlSerializer.Deserialize(stringReader);
                return notification;
            }
        }
    }
}