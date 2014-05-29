using System.Collections.Generic;
using System;

namespace XmcdParser
{
    [Serializable]
    public class Disc
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int DiskLength { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public List<string> DiscIds { get; set; }

        public List<int> TrackFramesOffsets { get; set; }
        public List<string> Tracks { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public Disc()
        {
            TrackFramesOffsets = new List<int>();
            Tracks = new List<string>();
            DiscIds = new List<string>();
            Attributes = new Dictionary<string, string>();
        }
    }
}