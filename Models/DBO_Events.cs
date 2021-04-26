using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{
    public class DBO_Events
    {
        public long EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventType { get; set; }
        public int EventCapacity { get; set; }
        public bool HasFinished { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public DBO_User EventCreator { get; set; }
        public List<DBO_User> EventParticipants { get; set; }
    }
}