using System;
using SQLite;
namespace Xalendar.Models
{
    public enum TypeEvent
    {
        Birthday, Sport, Meeting, Health, Others
    }
    public class Event
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public String Title { get; set; }
        public DateTime Date { get; set; }
        public TypeEvent TypeEvt { get; set; }
        public String Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}