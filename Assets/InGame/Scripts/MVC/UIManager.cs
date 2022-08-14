using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UISibling
{
    //main
    MainDialog=1,
    //topdialog(페이드에 가려지는 UI)
    InGameDialog= 500,
}
public class UIManager : Monosingleton<UIManager>
{
    public GameObject MainDialogCanvas;
    public GameObject InGameDialogCanvas;
    public delegate void OnComplete(GameObject ui);

    Dictionary<string, GameObject> Uis=new Dictionary<string, GameObject>();
    Dictionary<string, System.Delegate> OnCompleteEvents=new Dictionary<string, System.Delegate>();

    string ASSET_PATH = "Dialogs";
    public override void Init()
    {
        base.Init();
        
    }

    public IEnumerator Load(string uiName,OnComplete oncomplete)
    {
        AddEvent(uiName, oncomplete);

        GameObject ui=Get(uiName);

        if(ui==null)
        {
            var path = string.Format("{0}/{1}", ASSET_PATH, uiName);
            yield return StartCoroutine(ResourcesLoader.Instance.Load<GameObject>(path,o=> OnPostLoadProcess(o)));
        }
        else
        {
            ui.SetActive(true);

            AttachtoCanvas(ui);
            RaiseEvent(ui);
        }

        yield break;
    }

    void AddEvent(string uiName, OnComplete oncomplete)
    {
        if (oncomplete == null)
            return;

        System.Delegate _event;
        if (OnCompleteEvents.TryGetValue(uiName, out _event))
        {
            OnCompleteEvents[uiName] = (OnComplete)OnCompleteEvents[uiName] + oncomplete;
        }
        else
        {
            OnCompleteEvents.Add(uiName, oncomplete);
        }
    }

    public GameObject Get(string uiname)
    {
        GameObject ui;
        Uis.TryGetValue(uiname, out ui);
        return ui;
    }

    void OnPostLoadProcess(Object o)
    {
        var ui = Instantiate(o) as GameObject;

        ui.SetActive(true);
        ui.name = o.name;

        AttachtoCanvas(ui);
        Uis.Add(ui.name, ui);
        RaiseEvent(ui);
    }

    void AttachtoCanvas(GameObject ui)
    {
        int UienumIdx = EnumExtention.ParseToInt<UISibling>(ui.name);
        var ParentObj = GetUIParentBySiblingIndex(UienumIdx);
        ui.transform.SetParent(MainDialogCanvas.transform, false);
        ui.transform.SetAsLastSibling();
    }

    void RaiseEvent(GameObject ui)
    {
        System.Delegate _event;
        if (OnCompleteEvents.TryGetValue(ui.name, out _event))
        {
            var oncom = (OnComplete)_event;
            oncom(ui);//17. 함수 호출

            OnCompleteEvents.Remove(ui.name);
        }

    }

    public void Unload(string uiName)
    {
        GameObject ui = Get(uiName);
        if (ui != null)
        {
            Destroy(ui);
            ui.GetComponent<IDialog>().Unload();
            Uis.Remove(uiName);
        }
    }

    GameObject GetUIParentBySiblingIndex(int index)
    {
        GameObject obj = null;
        if (index < 500)
        {
            obj = MainDialogCanvas;
        }
        else if (index < 1000)
        {
            obj = InGameDialogCanvas;
        }
        return obj;
    }
}
