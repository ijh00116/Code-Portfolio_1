using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testb;

public class IDialog : MonoBehaviour
{
    protected RectTransform rt;
    protected string typeName;
    public GameObject DialogView;

    protected bool IsEnter = false;

    protected virtual void Awake()
    {
        if(DialogView==null)
            throw new System.NullReferenceException(string.Format("{0} dialogview Null", typeName));
        typeName = GetType().Name;
        rt = GetComponent<RectTransform>();
    }

    protected virtual void OnDestroy()
    {

    }

    public void Load()
    {
        typeName= GetType().Name;
        rt= GetComponent<RectTransform>();

        Message.AddMessage<ShowDialogMsg>(Enter,typeName);
        Message.AddMessage<HideDialogMsg>(Exit, typeName);

        OnLoad();
        DialogView.SetActive(false);
    }

    protected virtual void OnLoad()
    {

    }

    public void Unload()
    {
        Message.RemoveMessage<ShowDialogMsg>(Enter, typeName);
        Message.RemoveMessage<HideDialogMsg>(Exit, typeName);

        OnExit();
        OnUnload();
    }

    protected virtual void OnUnload()
    {

    }

    private void Enter(ShowDialogMsg msg)
    {
        if (DialogView == null)
        {
#if UNITY_EDITOR
            Debug.LogError(string.Format("{0}'s DialogView is Null", typeName));
#endif
            return;
        }
        DialogView.SetActive(true);
        OnEnter();
    }



    private void Exit(HideDialogMsg msg)
    {
        if (DialogView == null)
        {
#if UNITY_EDITOR
            Debug.LogError(string.Format("{0}'s DialogView is Null", typeName));
#endif
            return;
        }
        DialogView.SetActive(false);
        OnExit();
    }

    protected virtual void OnEnter()
    {

    }

    protected virtual void OnExit()
    {

    }
    public static void RequestDialogEnter<T>() where T : IDialog
    {
        Message.Send<ShowDialogMsg>(new ShowDialogMsg(),typeof(T).Name);
    }

    public static void RequestDialogExit<T>() where T : IDialog
    {
        Message.Send<HideDialogMsg>(new HideDialogMsg(), typeof(T).Name);
    }
}
