using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : Singleton<CollectableManager>
{
    public List<Collectable> AvaibleBuffs;
    public List<Collectable> AvaibleDebuffs;

    [Range(0, 100)]
    public float buffChance;

    [Range(0, 100)]
    public float debufChance;
}
