using System;

namespace Xalendar
{
    public interface INotification
    {
        void Show(int id, string title, string typeEvent,DateTime date);
        void Cancel(int id);
    }
}