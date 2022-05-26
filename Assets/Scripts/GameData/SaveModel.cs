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
    public SettingsModel Settings;
}

