using LogMonitor.Utils;
using System;
using System.Collections.Generic;

namespace LogMonitor.Domain.Timer
{
    public abstract class StatusTimerMonitor : TimerMonitor
    {
        protected Dictionary<string, int> _pageHits;
        protected Dictionary<string, List<string>> _sections;
        protected bool _isCumulative;

        public StatusTimerMonitor(long time, string file, bool isCumulative) : base(time, time, file)
        {
            _pageHits = new Dictionary<string, int>();
            _sections = new Dictionary<string, List<string>>();
            _isCumulative = isCumulative;
        }

        protected override void parseContent(string file)
        {
            DateTimeOffset dateTime = DateTimeOffset.Now;

            if (!_isCumulative)
            {
                _sections.Clear();
                _pageHits.Clear();
            }

            var lines = _logParser.ParseContent(file);
            foreach (var line in lines)
            {
                if (isLineInvalid(line, dateTime))
                    continue;

                _sections.AddOrUpdate(line.Website, new List<string> { line.Section }, (site, sections) =>
                {
                    if (!sections.Contains(line.Section))
                        sections.Add(line.Section);

                    return sections;
                });

                _pageHits.AddOrUpdate(line.Website, 1, (id, count) => count + 1);
            }

            printTopHits();
        }

        protected abstract void printTopHits();
    }
}
