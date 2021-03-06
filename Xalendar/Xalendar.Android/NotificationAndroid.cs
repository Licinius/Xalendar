﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Xalendar.Droid;
using Xamarin.Essentials;
using Xamarin.Forms;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

[assembly: Dependency(typeof(NotificationAndroid))]
namespace Xalendar.Droid
{

    class NotificationAndroid : INotification
    {
        public void Show(int id,string title, string typeEvent, DateTime date)
        {
            MyNotification myNotif = new MyNotification();
            myNotif.Title = title;
            myNotif.Body = typeEvent + " [" + date.ToString("dd/MM/yyyy") + "]";
            myNotif.NotifyTime = date;
            myNotif.TypeEvent = typeEvent;
            myNotif.Id = id;
           var serializedNotification = serializeNotification(myNotif);
           
            var intent = CreateIntent(id);
            intent.PutExtra(ScheduledAlarmHandler.LocalNotificationKey, serializedNotification);
            var pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, intent, PendingIntentFlags.UpdateCurrent);
            var triggerTime = NotifyTimeInMilliseconds(date);
            var alarmManager = GetAlarmManager();
            alarmManager.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);
        }

        public void Cancel(int id)
        {
            var intent = CreateIntent(id);
            var pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, intent, PendingIntentFlags.UpdateCurrent);
            GetAlarmManager().Cancel(pendingIntent);

        }
        public void Show(MyNotification notification)
        {
            if(Preferences.Get(notification.TypeEvent, false))
            {
                var builder = new NotificationCompat.Builder(Android.App.Application.Context, "1")
                    .SetAutoCancel(true)
                    .SetVibrate(new long[] { 200, 30, 200, 30, 200, 30, 500, 30, 500, 30, 500, 30, 200, 30, 200, 30, 200 })
                    .SetContentTitle(notification.Title)
                    .SetContentText(notification.Body)
                    .SetSmallIcon(Resource.Mipmap.icon);
               
                if (notification.TypeEvent == "Sport")
                {
                    var intent = new Intent(Android.App.Application.Context, typeof(SportActivity));
                    intent.PutExtra("Id", notification.Id);
                    TaskStackBuilder stackBuilder = TaskStackBuilder.Create(Android.App.Application.Context);
                    stackBuilder.AddParentStack(Java.Lang.Class.FromType (typeof(SportActivity)));
                    stackBuilder.AddNextIntent(intent);
                    PendingIntent pendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);
                    builder.SetContentIntent(pendingIntent);

                }
                var notificationManager = NotificationManagerCompat.From(Android.App.Application.Context);
                notificationManager.Notify(notification.GetHashCode(), builder.Build());
            }
        }

        private Intent CreateIntent(int id)
        {
            return new Intent(Android.App.Application.Context, typeof(ScheduledAlarmHandler))
                .SetAction("LocalNotifierIntent" + id);
        }

        private AlarmManager GetAlarmManager()
        {
            var alarmManager = Android.App.Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            return alarmManager;
        }
        
        private string serializeNotification(MyNotification notification)
        {
            var xmlSerializer = new XmlSerializer(notification.GetType());
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, notification);
                return stringWriter.ToString();
            }
        }
        
        private long NotifyTimeInMilliseconds(DateTime notifyTime)
        {
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
            var epochDifference = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;

            var utcAlarmTimeInMillis = utcTime.AddSeconds(-epochDifference).Ticks / 10000;
            return utcAlarmTimeInMillis;
        }
    }
}