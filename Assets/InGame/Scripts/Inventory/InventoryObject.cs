using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlackTree;
using PlayFab.ClientModels;

public class InventoryObject
{
    [SerializeField]
    private Inventory Container = new Inventory();
    public List<ItemSlot> GetSlots => Container.Slots;

    public void Init()
    {
        for (int i = 0; i < InGameDataTableManager.Instance.characterinfo.CharactersDefaultData.Count; i++)
        {
            ItemSlot slot = new ItemSlot();
            slot.item = new Item(InGameDataTableManager.Instance.characterinfo.CharactersDefaultData[i].idx);
            slot.item.characterItemInfo = InGameDataTableManager.Instance.characterinfo.CharactersDefaultData[i];
           
            GetSlots.Add(slot);
        }
    }
    public bool AddAmount(Item item, int value)
    {
        ItemSlot slot = FindItemOnInventory(item);
        if (slot == null)
            return false;

        slot.AddAmount(value);
        return true;
    }
    public Item GetItemIninventory(int idx)
    {
        var _item = GetSlots.Find(o => o.item.idx == idx);
        
        return _item.item;
    }

    public ItemSlot FindItemOnInventory(Item item)
    {
        var _item = GetSlots.Find(o => o.item.idx ==item.idx);

        return _item;
    }
}

[System.Serializable]
public class Inventory
{
    public List<ItemSlot> Slots = new List<ItemSlot>();
    public Inventory()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            Slots[i] = new ItemSlot();
        }
    }

    public void Clear()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            Slots[i].item = new Item();
        }
    }
}

[System.Serializable]
public class ItemSlot
{
    [System.NonSerialized] public InventoryObject parent;
    [System.NonSerialized] public Action onAfterUpdated;
    [System.NonSerialized] public Action onBeforeUpdated;
    [System.NonSerialized] public ItemUIDisplay display;

    public Item item;

    public void AddAmount(int value)
    {
        item.amount += value;

        PlayfabManager.Instance.SavePlayerData(item.idx, item, OnDataSend);
        UpdateSlot();
    }

    public void AddLevel(int value)
    {
        item.AddLevel(value);

        PlayfabManager.Instance.SavePlayerData(item.idx, item, OnDataSend);
        UpdateSlot();
    }

    private void OnDataSend(UpdateUserDataResult obj)
    {
        var _data = obj.Request.ToJson();

        Debug.Log($"success user data sended : {_data}");
    }

    public void UpdateSlot()
    {
        //서버로드시에 이큅값이 트루면 여기로 들어올것이다.
        onBeforeUpdated?.Invoke();

        onAfterUpdated?.Invoke();
    }

}
