using System;

namespace SampleBackend
{
    public class Blog
    {
        public string ID { get; set; }
        public long Time { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public Blog()
        {

        }

        public Blog(string id, long time, string title, String text)
        {
            this.ID = id;
            this.Time = time;
            this.Title = title;
            this.Text = text;
        }

        public override string ToString()
        {
            return "{\"id\":" + ID +
                ",\"time\":" + Time +
                ",\"title\":" + Title +
                ",\"text\":" + Text + "}";
        }
    }
}
