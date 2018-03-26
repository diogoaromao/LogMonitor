using LogMonitor.Utils;
using System.Collections.Generic;

namespace LogMonitor.Domain.Timer
{
    public abstract class StatusTimerMonitor : TimerMonitor
    {
        protected Dictionary<string, int> _pageHits;
        protected Dictionary<string, List<string>> _sections;
        protected bool _isCumulative;

        public StatusTimerMonitor(long time, string file, bool isCumulative) : base(time, file)
        {
            _pageHits = new Dictionary<string, int>();
            _sections = new Dictionary<string, List<string>>();
            _isCumulative = isCumulative;
        }

        protected override void parseContent(string file)
        {
            if (!_isCumulative)
            {
                _sections.Clear();
                _pageHits.Clear();
            }

            var lines = _logParser.ParseContent(file);
            foreach (var line in lines)
            {
                if (isLineInvalid(line))
                    continue;

                _sections.AddOrUpdate(line.Website, new List<string> { line.Section }, (site, sections) =>
                {
                    if (!sections.Contains(line.Section))
                        sections.Add(line.Section);

                    return sections;
                });

                _pageHits.AddOrUpdate(line.Website, 1, (id, count) => count + 1);

                /*if (_sections.ContainsKey(line.Website))
                {
                    var sections = _sections[line.Website];
                    if (!sections.Contains(line.Section))
                    {
                        sections.Add(line.Section);
                        _sections[line.Website] = sections;
                    }
                }
                else
                {
                    var sections = new List<string>();
                    sections.Add(line.Section);
                    _sections.Add(line.Website, sections);
                }

                if(_pageHits.ContainsKey(line.Website))
                {
                    _pageHits[line.Website] = ++_pageHits[line.Website];
                }
                else
                {
                    _pageHits.Add(line.Website, 1);
                }*/
            }

            printTopHits();
        }

        protected abstract void printTopHits();
    }
}
