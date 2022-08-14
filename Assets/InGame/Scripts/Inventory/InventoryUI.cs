using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackTree
{
    public class InventoryUI : MonoBehaviour
    {
        InventoryObject inventory;
        [SerializeField]ItemUIDisplay itemSlot;
        [SerializeField] Transform slotparent;

        public Dictionary<ItemUIDisplay, ItemSlot> slotsOnInterface = new Dictionary<ItemUIDisplay, ItemSlot>();

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(GetDBItemInfo());
        }

        IEnumerator GetDBItemInfo()
        {
            yield return new WaitUntil(() => InGameDataTableManager.Instance.tableLoaded== true);

            inventory = new InventoryObject();
            inventory.Init();

            yield return new WaitUntil(()=>PlayfabManager.Instance.isLogin == true);

            Debug.Log($"아이템 로드");

            foreach (var data in inventory.GetSlots)
            {
                var keyidx = data.item.idx.ToString();
                PlayfabManager.Instance.GetPlayerData(keyidx, o=> { OnDataRecieved(o, data); });
            }

            //cloudscript 함수 만들어서 인벤토리의 전체 정보를 로드해야 한다.
            //또한 아이템의 세이브는 아이템 세이브시 유저의 인벤토리 정보를 업데이트 하도록 cloudscript를 작업해야 한다.
            //따라서 인벤토리 전체정보를 받아서 콜백으로 인벤토리 db를 모두 받았을때 다음으로 넘어간다.
            //그러나 지금은 잠시 임시로 2초 뒤에 넘어가서 조정하도록 한다.
            yield return new WaitForSeconds(2.0f);

            CreateSlots();
        }

        public void CreateSlots()
        {
            slotsOnInterface = new Dictionary<ItemUIDisplay, ItemSlot>();
            for (int i = 0; i < inventory.GetSlots.Count; i++)
            {
                var obj = Instantiate(itemSlot);
                obj.transform.SetParent(slotparent.transform, false);
                obj.transform.localPosition = Vector3.zero;
                slotsOnInterface.Add(obj, inventory.GetSlots[i]);
            }

            foreach (var data in slotsOnInterface)
            {
                data.Key.GetComponent<ItemUIDisplay>().Init(data.Value);
                data.Value.display = data.Key.GetComponent<ItemUIDisplay>();
                data.Value.parent = inventory;
                data.Value.UpdateSlot();
            }

        }

        #region DB로드
        private void OnDataRecieved(PlayFab.ClientModels.GetUserDataResult result,ItemSlot itemdata)
        {
            Debug.Log($"Recieved user data!!{result.Data.Count}");
            if(result.Data.ContainsKey(itemdata.item.idx.ToString()))
            {
                var _itemdata = inventory.GetSlots.Find(o => o.item.idx == itemdata.item.idx);
                var dbItem = Newtonsoft.Json.JsonConvert.DeserializeObject<Item>(result.Data[itemdata.item.idx.ToString()].Value);
                _itemdata.item.Level = dbItem.Level;
                _itemdata.item.amount = dbItem.amount;
                _itemdata.item.idx = dbItem.idx;
            }
        }



        #endregion

    }

}
