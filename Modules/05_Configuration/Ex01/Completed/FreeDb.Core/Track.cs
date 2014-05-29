using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeDb
{
    [Serializable]
    public class Track
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }

        //public TrackInfo ToTrackInfo()
        //{
        //    return new TrackInfo() {Album = Album.Title, Artist = Artist.Name, Title = Title, Genre = Genre.Name};
        //}
    }
}
