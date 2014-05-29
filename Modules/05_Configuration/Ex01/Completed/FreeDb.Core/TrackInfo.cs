﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeDb
{
    [Serializable]
    public class TrackInfo
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Title { get; set; }
    }
}
