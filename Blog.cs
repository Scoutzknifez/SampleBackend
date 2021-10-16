using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleBackend
{
    public class Blog
    {
        public string ID { get; set; }
        public long Time { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public Blog(string id, long time, string title, String text)
        {
            this.ID = id;
            this.Time = time;
            this.Title = title;
            this.Text = text;
        }
    }
}
