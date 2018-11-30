using System;

namespace Xalendar.Models
{
    public enum TypeEvent
    {
        Birthday, Sport, Meeting, Health, Others
    }
    public class Event
    {
        public String Id { get; set; }
        public String Title { get; set; }
        public DateTime Date { get; set; }
        public TypeEvent TypeEvt { get; set; }
        public String Description { get; set; }

    }
}