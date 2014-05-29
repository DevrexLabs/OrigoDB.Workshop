using System;
using System.Collections.Generic;
using System.Diagnostics;
using OrigoDB.Core;
using OrigoDB.Modules.ProtoBuf;
using ProtoBuf.Meta;

namespace FreeDb.App
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length == 0) Usage();
            if (args[0] == "build") Build(args);
            else if (args[0] == "load") Load(args);
            else Usage();
        }

        static EngineConfiguration CreateProtobufConfig()
        {
            var config = new EngineConfiguration();
            var typeModel = TypeModel.Create();

            typeModel.Add(typeof (FreeDbModel), false)
                .Add(1, "_tracks");

            typeModel.Add(typeof (Track), false)
                .Add(1, "Artist")
                .Add(2, "Title")
                .Add(3, "Album")
                .Add(4, "Genre");

            ProtoBufFormatter.ConfigureSnapshots<FreeDbModel>(config, typeModel);
            return config;
        }

        private static void Load(string[] args)
        {
            Console.WriteLine("Loading...");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var config = CreateProtobufConfig();
            config.Location.OfJournal = args[1];
            _engine = Engine.Load<FreeDbModel>(config);
            sw.Stop();
            Console.WriteLine("Load complete, duration: " + sw.Elapsed);
            CommandLoop();
        }

        private static Engine<FreeDbModel> _engine;

        private static void Build(string[] args)
        {
            Assert(args.Length == 3 || args.Length == 4);
            int maxRecords = Int32.MaxValue;
            if (args.Length == 4 && !Int32.TryParse(args[3], out maxRecords)) Usage();
            string sourcePath = args[1];
            string targetPath = args[2];

            var cfg = CreateProtobufConfig();
            cfg.Location.OfJournal = targetPath;
            cfg.MaxBytesPerJournalSegment = 1024*1024*64;
            _engine = Engine.Create<FreeDbModel>(cfg);

            var duration = ParserDriver.ParseDisks(sourcePath,
                                                   d => _engine.Execute(new AddDiscCommand(d)),
                                                   s => Console.Write("\r{0}  ", s),
                                                   maxRecords);
            Console.WriteLine("\nBuild complete, duration: " + duration.Elapsed);
            CommandLoop();
        }


        private static void CommandLoop()
        {
            Console.WriteLine("Type search string or command prefixed with @. type @help for list of commands");
            while(true)
            {
                Console.Write(">");
                string line = Console.ReadLine();
                switch (line)
                {
                    case "@help":
                        Console.WriteLine("Commands: @help, @snapshot, @exit");
                        break;
                    case "@exit":
                        return;
                    case "@snapshot":
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        _engine.CreateSnapshot();
                        sw.Stop();
                        Console.WriteLine("Snapshot completed, duration: " +  sw.Elapsed);
                        break;
                    default:
                        var results = _engine.Execute(db => db.Search(line));
                        const string fmt = "Artist: {0}\nAlbum: {1}\nTitle: {2}\nGenre: {3}\n--------------------------------------------------------";
                        //Console.WriteLine(fmt, "Artist", "Album", "Track", "Genre");
                        foreach (Track track in results)
                        {
                            Console.WriteLine(fmt, track.Artist, track.Album, track.Title, track.Genre);
                        }
                        break;
                }
            }
        }

        private static void Assert(bool condition)
        {
            if (condition == false) Usage();
        }

        private static void Usage()
        {
            Console.WriteLine("syntax:");
            Console.WriteLine("freedb-search build <source-path> <destination-path> [<max-records>]");
            Console.WriteLine("freedb-search load <path>");
            Environment.Exit(-1);
        }
    }
}
