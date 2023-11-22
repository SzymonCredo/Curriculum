using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curriculum.Events
{
    public class CurriculumEvent {
        [JsonIgnore] public static Dictionary<DateOnly, List<CurriculumEvent>> events = new();
        public string Name { get; }
        public string Description { get; }
        public DateTime startTime { get; }
        public DateTime endTime { get; }

        [JsonIgnore] public EventRepresentation repres { get => new(this); } 

        [JsonConstructor]
        public CurriculumEvent(string Name, DateTime startTime, DateTime endTime, string Description)
        {
            this.Name = Name;
            this.startTime = startTime;
            this.endTime = endTime;
            this.Description = Description;

            var key = new DateOnly(startTime.Year , startTime.Month , startTime.Day);
            List<CurriculumEvent> evlist; // nie korzystam z niego, jest wykorzystywane tylko by użyć "trygetvalue"
            if(!events.TryGetValue(key, out evlist))
                events.Add(key , new());
            events[key].Add(this);
            key = key.AddDays(1);
            while(key.ToDateTime(TimeOnly.MinValue) < endTime) {
                if(!events.TryGetValue(key , out evlist))
                    events.Add(key , new());
                events[key].Add(this);
                key = key.AddDays(1);
            }
        }
    }
}
