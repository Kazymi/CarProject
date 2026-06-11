using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LinqTest : MonoBehaviour
{
    void TesT()
    {
        var buffs = new List<Buff>()
        {
            new Buff { Name = "Сила", Type = BuffType.Permanent, Duration = -1 },
            new Buff { Name = "Ловкость", Type = BuffType.Temporary, Duration = 10 },
            new Buff { Name = "Интеллект", Type = BuffType.Temporary, Duration = 15 },
    
            new Buff { Name = "Броня", Type = BuffType.Permanent, Duration = -1 },
            new Buff { Name = "Скорость", Type = BuffType.Temporary, Duration = 8 },
       
            new Buff { Name = "Крит", Type = BuffType.Temporary, Duration = 20 },
            new Buff { Name = "Выносливость", Type = BuffType.Permanent, Duration = -1 },
   
            new Buff { Name = "Невидимость", Type = BuffType.Temporary, Duration = 5 },
            new Buff { Name = "Благословение", Type = BuffType.EndForDamage, Duration = 30 }
        };

        buffs.Remove(buffs.FirstOrDefault(x => x.Name == "Исцеление"));
    }
}

public class Buff
{
    public string Name;
    public BuffType Type;
    public float Duration;
}

public enum BuffType
{
    Permanent,
    Temporary,
    EndForDamage,
}