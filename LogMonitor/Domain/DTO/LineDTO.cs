namespace LogMonitor.Domain.DTO
{
    public class LineDTO
    {
        private readonly string _website;
        private readonly string _section;
        private readonly double _size;

        public LineDTO(string website, string section, double size)
        {
            _website = website;
            _section = section;
            _size = size;
        }

        public string Website => _website;
        public string Section => _section;
        public double Size => _size;
    }
}
