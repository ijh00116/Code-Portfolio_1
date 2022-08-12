using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Testb;



public class IScene : MonoBehaviour
{
    public SceneName sceneName;

    public List<string> UIList = new List<string>();
    public List<string> EnterUIList = new List<string>();

    Action _onLoadComplete = null;
    bool _resourceLoadComplete = false;
    int _loadingContentsCount = 0;

    public void LoadAssets(Action onComplete)
    {
        _onLoadComplete = onComplete;
        StartCoroutine(LoadContents());
    }

    IEnumerator LoadContents()
    {
        OnLoadStart();

        Application.targetFrameRate = -1;

        while (_resourceLoadComplete == false)
            yield return null;

        _loadingContentsCount = UIList.Count;

        for (int i = 0; i < UIList.Count; ++i)
        {
            yield return StartCoroutine(UIManager.Instance.Load(UIList[i],
                c =>
                {
                    _loadingContentsCount--;
                    OnContentLoadComplete(c); 
                }));
        }



        EnterContents();
        OnLoadComplete();

        Application.targetFrameRate = 50;

        if (_onLoadComplete != null)
            _onLoadComplete();
    }

    protected void SetResourceLoadComplete()
    {
        _resourceLoadComplete = true;
    }
    protected virtual void OnLoadStart()
    {
        SetResourceLoadComplete();
    }

    void EnterContents()
    {
        for (int i = 0; i < EnterUIList.Count; i++)
        {
            Message.Send<ShowDialogMsg>(new ShowDialogMsg(), EnterUIList[i]);
        }
    }

    protected virtual void OnLoadComplete()
    {
        /* BLANK */
    }

    protected virtual void OnContentLoadComplete(GameObject ui)
    {
        ui.SetActive(true);
        var dialog = ui.GetComponent<IDialog>();
        dialog.Load();
    }

    public void Unload(bool ExitGame)
    {
        OnUnload();

        if (ExitGame == false)
        {
            if (UIManager.Instance!= null)
            {
                for (int i = 0; i < UIList.Count; ++i)
                    UIManager.Instance.Unload(UIList[i]);
            }
        }
        // Destroy(ContentLoader.Instance.gameObject);

        Destroy(gameObject);
    }

    protected virtual void OnUnload()
    {
        /* BLANK */
    }
}
