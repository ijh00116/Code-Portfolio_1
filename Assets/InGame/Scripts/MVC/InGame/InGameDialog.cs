using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Testb;

public class InGameDialog : IDialog
{
    [SerializeField] Button LoadInGame;
    // Start is called before the first frame update
    void Start()
    {
        LoadInGame.onClick.AddListener(() => SceneManager.Instance.LoadScene(SceneName.Main));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
