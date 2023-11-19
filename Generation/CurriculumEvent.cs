using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Curriculum {
    public class CurriculumEvent {
        public string Name { get; }
        public string Description { get; }
        public DateTime startTime { get; }
        public DateTime endTime { get; }

        [JsonConstructor]
        public CurriculumEvent(string Name, DateTime startTime, DateTime endTime, string Description) {
            this.Name = Name;
            this.startTime = startTime;
            this.endTime = endTime;
            this.Description = Description;
        }
    }
}
