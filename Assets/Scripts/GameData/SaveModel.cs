using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class SaveModel
{
    public int Score;
    public List<IngridientModel> Ingridients;

    //public string ToJSON()
    //{
    //    StringBuilder sb = new StringBuilder();

    //    sb.Append("{");
    //    sb.Append("\"Score\":" + Score.ToString() + ",");
    //    sb.Append("\"Ingridients\":[");

    //    for (int i = 0; i < Ingridients.Count; i++)
    //    {
    //        sb.Append(JsonUtility.ToJson(Ingridients[i]) + ",");
    //        if (i == Ingridients.Count - 1)
    //            sb.Remove(sb.Length - 1, 1);
    //    }

    //    sb.Append("]}");

    //    return sb.ToString();
    //}
}

