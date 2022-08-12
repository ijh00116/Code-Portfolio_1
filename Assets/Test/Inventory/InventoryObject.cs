using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterItem
{
    public int idx;
    public string icon;
    public string name;
    public string prefab;
    public string job;
    public int skillidx;
    public int max_lv;
    public string sprite;
    public int attack;
    public int hp;
    public int defense;
    public int attack_speed;
    public float skill_value;
    public float Increase_attack;
    public float Increase_hp;
    public float Increase_attack_speed;
    public string gradeType;
}
public class CharacterColor
{
    public string gradeType;
    public string color;
}

public class CharacterLvInfo
{
    public int lv;
    public int Common;
    public int Uncommon;
    public int Rare;
    public int Tremendous;
    public int Legendary;
    public int God;
}

public class CharacterInfo
{
    public List<CharacterItem> CharactersDefaultData;
    public List<CharacterColor> Color;
    public List<CharacterLvInfo> MaxAmount;
    public List<CharacterLvInfo> Gold;
}

public class ItemInfo
{
    public string instanceId;
    public int level;
    public int amount;
    public bool equip;
}

public class ItemSaveData
{
    public string id;
    public string instanceId;
    public int amount;
    public bool equip;
    public int level;
}
public class InventoryObject
{
    CharacterInfo characterinfo;
    // Start is called before the first frame update
    public void Init()
    {
        characterinfo = ReadData<CharacterInfo>("DT_KR_CharactersDefaultData.xlsx");
    }


    T ReadData<T>(string fileName)
    {
        var path = new System.Text.StringBuilder();
        path.Append(fileName);

        TextAsset jsonString = Resources.Load<TextAsset>(path.ToString());

        if (jsonString != null)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString.text);
        }
        return default;
    }

}
