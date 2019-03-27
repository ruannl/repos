using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Google.Apis.Calendar.v3.Data;

namespace Ruann.Linde.Models {

    public class CalendarEventGroup {
        /// <summary>
        ///     Gets or sets a sequence of calendar events to show under the group title.
        /// </summary>
        [Required]
        public IEnumerable<Event> Events { get; set; }
        /// <summary>
        ///     Gets or sets a string to show above the group of events.
        /// </summary>
        [Required]
        public string GroupTitle { get; set; }
    }

    public class UpcomingEventsViewModel {
        /// <summary>
        ///     Gets or sets a sequence of event groups to display.
        /// </summary>
        [Required]
        public IEnumerable<CalendarEventGroup> EventGroups { get; set; }
    }
}