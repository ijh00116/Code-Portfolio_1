using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Testb;
public class MainDialog : IDialog
{
    [SerializeField] Button LoadInGame;
    // Start is called before the first frame update
    void Start()
    {
        LoadInGame.onClick.AddListener(() => SceneManager.Instance.LoadScene(SceneName.InGame));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
