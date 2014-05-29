using System;
using System.Collections.Generic;
using System.Linq;
using OrigoDB.Core;
using XmcdParser;
using System.Text.RegularExpressions;

namespace FreeDb
{
    [Serializable]
    public class FreeDbModel : Model
    {


        public IEnumerable<Track> Search(string query)
        {
            HashSet<Track> result = null;
            foreach (string key in ParsePhrase(query))
            {
                if(_index.ContainsKey(key))
                {
                    if (result == null)
                    {
                        result = new HashSet<Track>(_index[key]);
                    }
                    else result.IntersectWith(_index[key]);
                }
            }
            return (result ?? Enumerable.Empty<Track>()).ToArray();
        }

        private List<Track> _tracks;
        
        [NonSerialized]
        private Dictionary<string, Artist> _artists;
        
        [NonSerialized]
        private Dictionary<string, Genre> _genres;

        [NonSerialized] 
        private Dictionary<string, HashSet<Track>> _index;

        public FreeDbModel()
        {
            _tracks = new List<Track>(30000000);
            InitializeNonSerializedCollections();
        }

        private void InitializeNonSerializedCollections()
        {
            _artists = new Dictionary<string, Artist>(1000000, StringComparer.OrdinalIgnoreCase);
            _genres = new Dictionary<string, Genre>(10000, StringComparer.OrdinalIgnoreCase);
            _index = new Dictionary<string, HashSet<Track>>(1000000, StringComparer.OrdinalIgnoreCase);
        }

        protected override void SnapshotRestored()
        {
            InitializeNonSerializedCollections();
            RebuildNonSerializedCollections();
        }

        public void AddDisc(Disc disc)
        {
            Artist artist;
            string artistName = disc.Artist ?? "Unknown";
            //if (!_artists.TryGetValue(artistName, out artist))
            //{
            //    artist = new Artist() { Name = artistName};
            //    _artists[artistName] = artist;
            //}
            Genre genre;
            string genreName = disc.Genre ?? "Unknown";
            //if(!_genres.TryGetValue(genreName, out genre))
            //{
            //    genre = new Genre() { Name = genreName};
            //    _genres[genreName] = genre;
            //}

            //var album = new Album() {Artist = artist, Title = disc.Title, Genre = genre};
            var album = new Album() {Title = disc.Title};
            //artist.Albums.Add(album);

            foreach(string trackName in disc.Tracks)
            {
                var track = new Track{Album = disc.Title, Artist = artistName, Genre = genreName, Title = trackName};
                //album.Tracks.Add(track);
                _tracks.Add(track);
                Index(track);
            }

        }

        private void Index(Track track)
        {
            AddPhraseToIndex(track.Artist, track);
            AddPhraseToIndex(track.Genre, track);
            AddPhraseToIndex(track.Album, track);
            AddPhraseToIndex(track.Title, track);
        }


        private IEnumerable<string> ParsePhrase(string phrase)
        {
            if (String.IsNullOrEmpty(phrase)) yield break;
            foreach (Match match in Regex.Matches(phrase, @"[A-Za-z0-9]+")) 
                yield return match.Value;
        }

        private void AddPhraseToIndex(string phrase, Track track)
        {
            foreach(string key in ParsePhrase(phrase)) AddKeyToIndex(key, track);
        }

        private void AddKeyToIndex(string key, Track track)
        {
            HashSet<Track> tracks;
            if(_index.TryGetValue(key, out tracks))
            {
                tracks.Add(track);
            }
            else _index[key] = new HashSet<Track>{track};
        }

        private void RebuildNonSerializedCollections()
        {
            foreach (Track track in _tracks)
            {
                //if (!_artists.ContainsKey(track.Artist.Name))
                //{
                //    _artists[track.Artist.Name] = track.Artist;
                //}

                //if(!_genres.ContainsKey(track.Genre.Name))
                //{
                //    _genres[track.Genre.Name] = track.Genre;
                //}
                Index(track);
            }
        }

 
    }
}
