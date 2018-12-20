﻿using System;
using System.Linq;
using TrackManagement;

namespace DarkSlidesTrackListParser
{
    class Program
    {
        static string PrintTrackType(TrackType type)
        {
            string value = "Unknown";
            switch(type)
            {
                case TrackType.National:
                    {
                        value = "National";
                        break;
                    }
                case TrackType.Supercross:
                    {
                        value = "Supercross";
                        break;
                    }
                case TrackType.FreeRide:
                    {
                        value = "FreeRide";
                        break;
                    }
            }

            return value;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Processing the tracks from darkslides server. This will take some time, go grab a drink.");
            var beginTime = DateTime.Now;
            Ds19TrackListParser parser = new Ds19TrackListParser();
            var tracks = parser.ParseTracks();

            var validTracks = tracks.Where(t => t.Result == ProcessResult.Success).ToArray();
            Console.WriteLine(string.Format("Listing valid tracks ({0})", validTracks.Length));
            foreach (var track in validTracks)
            {
                Console.WriteLine(string.Format("Name: {0}, Type: {1}, Slot: {2}, Url: {3}", track.TrackName, PrintTrackType(track.TrackType), track.SlotNumber, track.TrackUrl));
            }

            Console.WriteLine("");
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine("");

            var invlidTracks = tracks.Where(t => t.Result != ProcessResult.Success).ToArray();
            Console.WriteLine(string.Format("Listing invlid tracks ({0})", invlidTracks.Length));
            foreach (var track in invlidTracks)
            {
                Console.WriteLine(string.Format("Name: {0}, Type: {1}, Slot: {2}, Reason: {3}", track.TrackName, PrintTrackType(track.TrackType), track.SlotNumber, track.ErrorInfo));
            }

            var endTime = DateTime.Now;
            Console.WriteLine(string.Format("The process took {0} seconds to complete.", endTime.Second - beginTime.Second));
        }
    }
}
