﻿using System.Collections.Generic;
using System;
using FullSerializer;

namespace GalacticScale
{

    public class GSFSVeinTypeConverter : fsDirectConverter<GSVeinType>
    {
        public override object CreateInstance(fsData data, Type storageType)
        {
            return new GSVeinType();
        }

        protected override fsResult DoSerialize(GSVeinType model, Dictionary<string, fsData> serialized)
        {
            GS2.Warn("Serializing VeinTypes");
            GS2.LogJson(model.veins);
            List<fsData> list = new List<fsData>();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (var i = 0; i < model.veins.Count; i++)
            {
                string s = model.veins[i].count + "x" + model.veins[i].richness;
                if (!dict.ContainsKey(s)) dict.Add(s, 1);
                else dict[s]++;
            }
            foreach (KeyValuePair<string, int> kvp in dict)
            {
                list.Add(new fsData(kvp.Value + "x" + kvp.Key));
            }
            GS2.Warn("-----"+list.Count);
            //for (var i = 0; i < model.veins.Count; i++)
            //{
            //    list.Add(new fsData(model.veins[i].count + "@" + model.veins[i].richness));
            //}
            SerializeMember(serialized, null, "type", GSVeinType.insaneVeinTypes[model.type]);
            serialized["veins"] = new fsData(list);
            //SerializeMember(serialized, null, "veins", dict);
            SerializeMember(serialized, null, "rare", model.rare);
            // Serialize age using helper methods


            return fsResult.Success;
        }

        protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref GSVeinType model)
        {
            GS2.Log("DoDeserialize");
            var result = fsResult.Success;
            model = new GSVeinType();
            // Deserialize name mainly manually (helper methods CheckKey and CheckType)
            fsData veinData;
            if (CheckKey(data, "veins", out veinData).Succeeded)
            {
                if ((result += CheckType(veinData, fsDataType.Array)).Failed) return result;
                var veins = veinData.AsList;
                model.veins = new List<GSVein>();
                for (var i = 0; i < veins.Count; i++)
                {
                    var d = veins[i].AsString.Split(new[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
                    int groupCount;
                    float richness;
                    int count;
                    if (!int.TryParse(d[0], out groupCount)) return fsResult.Fail("VeinGroupCount Not Int: " + d[0]);
                    if (!float.TryParse(d[2], out richness)) return fsResult.Fail("VeinRichness Not Float: " + d[2]);
                    if (!int.TryParse(d[1], out count)) return fsResult.Fail("VeinCount Not Int: " + d[1]);
                    for (var j=0;j<groupCount;j++) model.veins.Add(new GSVein(count, richness));
                }
            }
            fsData generate;
            if (CheckKey(data, "generate", out generate).Succeeded)
            {
                if (!generate.IsInt64) return fsResult.Fail("generate number not an integer"); 
                var numToGenerate = (int)generate.AsInt64;
                if (numToGenerate < 0) { GS2.Warn("generate number < 0"); numToGenerate = 0; }
                if (numToGenerate > 64) { GS2.Warn("generate number > 64"); numToGenerate = 64; }
                if (numToGenerate < model.veins.Count) { GS2.Warn("generate number < existing vein count"); numToGenerate = 0; }
                numToGenerate -= model.veins.Count;
                for (var i = 0; i < numToGenerate; i++) {
                    model.veins.Add(new GSVein());
                }
            }
            string type;
            if ((result += DeserializeMember(data, null, "type", out type)).Failed) return result;
            model.type = GSVeinType.saneVeinTypes[type];
            if ((result += DeserializeMember(data, null, "rare", out model.rare)).Failed) return result;
            return result;
        }
    }
}