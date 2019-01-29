using System;

namespace Xalendar
{
    public interface INotification
    {
        void Show(string title, string text,string typeEvent, DateTime date);
    }
}