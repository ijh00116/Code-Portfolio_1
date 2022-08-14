using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIDisplay : MonoBehaviour
{
    [HideInInspector]
    public ItemSlot slot;

    [SerializeField] Text itemid;
    [SerializeField] Text itemname;
    [SerializeField] Text itemLv;
    [SerializeField] Text itemAmount;

    [SerializeField] Button levelupButton;
    [SerializeField] Button amountupButton;

    public void Init(ItemSlot _slot)
    {
        slot = _slot;

        slot.onAfterUpdated += SlotUIUpdate;
        slot.onBeforeUpdated += SlotUIBeforUpdate;

        itemid.text = slot.item.idx.ToString();
        itemname.text = slot.item.characterItemInfo.name;
        itemLv.text = slot.item.Level.ToString();
        itemAmount.text = slot.item.amount.ToString();

        levelupButton.onClick.AddListener(Levelup);
        amountupButton.onClick.AddListener(AmountUp);
    }

    public void SlotUIUpdate()
    {
        itemid.text = slot.item.idx.ToString();
        itemname.text = slot.item.characterItemInfo.name;
        itemLv.text = slot.item.Level.ToString();
        itemAmount.text = slot.item.amount.ToString();
    }

    public void SlotUIBeforUpdate()
    {

    }

    void Levelup()
    {
        slot.AddLevel(1);
    }

    void AmountUp()
    {
        slot.AddAmount(1);
    }
}
