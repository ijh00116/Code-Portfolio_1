using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [System.NonSerialized]
    public CharacterItem characterItemInfo;

    public int Level = 1;
    public int amount = 0;
    public int idx;

    public Item()
    {
        idx = -1;
        Level = 1;
    }

    public Item(int _idx)
    {
        idx = _idx;
        Level = 1;
        amount = 0;
    }

    public void AddLevel(int value)
    {
        Level += value;
    }
}
