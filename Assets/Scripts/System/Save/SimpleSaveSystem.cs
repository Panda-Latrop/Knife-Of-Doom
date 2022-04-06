using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

public class SimpleSaveSystem : FileMaster
{
    [System.Serializable]
    public class SimpleData
    {
        public int score = 0;
        public int maxStage = 1;
        public bool vibrate;
        public string currentSkin = string.Empty;
        public List<string> skins = new List<string>();

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }

        //public Dictionary<string, bool> skins = new Dictionary<string, bool>();

        public override string ToString()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("Score: ").Append(score).Append(", Max Stage: ").Append(maxStage).Append("\n");
            foreach (var item in skins)
            {
                strb.Append("   ").Append(item);
            }
            return strb.ToString();
            
        }
    }
    protected SimpleData data = new SimpleData();
    protected string directory => Application.persistentDataPath + "/saves/";

    public SimpleData Data => data;
    public void Save()
    {
        WriteTo(directory + "save.sv", data);
    }
    public void Load()
    {
        data = ReadFrom<SimpleData>(directory + "save.sv");
        if(data == null)
            data = new SimpleData();
    }

}
