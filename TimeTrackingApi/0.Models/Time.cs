using System.ComponentModel.DataAnnotations;

namespace TimeTrackingApi._0.Models
{
    public class Time
    {
        private int _hours;
        private int _minutes;
        private int _seconds;

        public Time()
        {
            _hours = 0;
            _minutes = 0;
            _seconds = 0;
        }

        [Range(0,24)]
        public int Hours { get => _hours; set => _hours = value; }

        [Range(0, 59)]
        public int Minutes { get => _minutes; set => _minutes = value; }

        [Range(0, 59)]
        public int Seconds { get => _seconds; set => _seconds = value; }
    }
}
