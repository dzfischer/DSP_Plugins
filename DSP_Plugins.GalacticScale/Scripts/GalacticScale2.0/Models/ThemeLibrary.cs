﻿using System.Collections.Generic;
using System.Linq;

namespace GalacticScale
{
    public class GSThemeLibrary : Dictionary<string, GSTheme>
    {
        private GS2.Random random = new GS2.Random();
        public GSThemeLibrary()
        {
        }

        public List<string> Hot
        {
            get
            {
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, GSTheme> kv in this) if (kv.Value.Temperature >= 3) list.Add(kv.Key);
                return list;
            }
        }
        public List<string> Warm
        {
            get
            {
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, GSTheme> kv in this) if (kv.Value.Temperature >= 0 && kv.Value.Temperature < 3) list.Add(kv.Key);
                return list;
            }
        }
        public List<string> Temperate
        {
            get
            {
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, GSTheme> kv in this) if (kv.Value.Temperature == 0) list.Add(kv.Key);
                return list;
            }
        }
        public List<string> Cold
        {
            get
            {
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, GSTheme> kv in this) if (kv.Value.Temperature < 0 && kv.Value.Temperature > -3) list.Add(kv.Key);
                return list;
            }
        }
        public List<string> Frozen
        {
            get
            {
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, GSTheme> kv in this) if (kv.Value.Temperature <= -3) list.Add(kv.Key);
                return list;
            }
        }
        public List<string> Habitable
        {
            get
            {
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, GSTheme> kv in this) if (kv.Value.PlanetType == EPlanetType.Ocean) list.Add(kv.Key);
                return list;
            }
        }
        public List<string> Desert
        {
            get
            {
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, GSTheme> kv in this) if (kv.Value.PlanetType == EPlanetType.Desert) list.Add(kv.Key);
                return list;
            }
        }
        public List<string> Volcanic
        {
            get
            {
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, GSTheme> kv in this) if (kv.Value.PlanetType == EPlanetType.Vocano) list.Add(kv.Key);
                return list;
            }
        }
        public List<string> Ice
        {
            get
            {
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, GSTheme> kv in this) if (kv.Value.PlanetType == EPlanetType.Ice) list.Add(kv.Key);
                return list;
            }
        }
        public List<string> GasGiant
        {
            get
            {
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, GSTheme> kv in this) if (kv.Value.PlanetType == EPlanetType.Gas && kv.Value.Temperature > 0) list.Add(kv.Key);
                return list;
            }
        }
        public List<string> IceGiant
        {
            get
            {
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, GSTheme> kv in this) if (kv.Value.PlanetType == EPlanetType.Gas && kv.Value.Temperature < 0) list.Add(kv.Key);
                return list;
            }
        }

        public GSTheme Random()
        {
            int choice = random.Next(0, this.Count);
            return this.ElementAt(choice).Value;
        }
        public GSTheme Random(List<string> themes)
        {
            int choice = random.Next(0, themes.Count);
            return this[themes[choice]];
        }

    }

}