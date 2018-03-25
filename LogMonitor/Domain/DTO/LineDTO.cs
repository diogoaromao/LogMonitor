using System;

namespace LogMonitor.Domain.DTO
{
    public class LineDTO
    {
        private readonly string _website;
        private readonly string _section;
        private readonly double _size;
        private readonly DateTimeOffset _dateTime;

        public LineDTO(string website, string section, double size, DateTimeOffset dateTime)
        {
            _website = website;
            _section = section;
            _size = size;
            _dateTime = dateTime;
        }

        public string Website => _website;
        public string Section => _section;
        public double Size => _size;
        public DateTimeOffset DateTime => _dateTime;
    }
}
