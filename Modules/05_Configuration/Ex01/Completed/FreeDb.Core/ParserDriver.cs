using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.BZip2;
using System.IO;
using ICSharpCode.SharpZipLib.Tar;
using XmcdParser;

namespace FreeDb
{
    public static class ParserDriver
    {
        private const int BatchSize = 50;
        public static Stopwatch ParseDisks(string path, Action<Disc> addToBatch, Action<string> reportProgress, int limit = Int32.MaxValue)
        {
            int i = 0;
            var parser = new Parser();
            var buffer = new byte[1024 * 1024];// more than big enough for all files

            var sp = Stopwatch.StartNew();

            using (var bz2 = new BZip2InputStream(File.Open(path, FileMode.Open)))
            using (var tar = new TarInputStream(bz2))
            {
                TarEntry entry;
                while ((entry = tar.GetNextEntry()) != null)
                {
                    if (entry.Size == 0 || entry.Name == "README" || entry.Name == "COPYING")
                        continue;
                    var readSoFar = 0;
                    while (true)
                    {
                        var read = tar.Read(buffer, readSoFar, ((int)entry.Size) - readSoFar);
                        if (read == 0)
                            break;

                        readSoFar += read;
                    }
                    // we do it in this fashion to have the stream reader detect the BOM / unicode / other stuff
                    // so we can read the values properly
                    var fileText = new StreamReader(new MemoryStream(buffer, 0, readSoFar)).ReadToEnd();
                    try
                    {
                        var disk = parser.Parse(fileText);
                        addToBatch(disk);
                        if (i++ % BatchSize == 0)
                        {
                            reportProgress(String.Format("{0}, # Records: {1,10}, time elapsed: {2}", entry.Name, i, sp.Elapsed));
                        }
                        if (i > limit) 
                            break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine();
                        Console.WriteLine(entry.Name);
                        Console.WriteLine(e);
                        return sp;
                    }
                }
            }
            return sp;
        }

    }
}
