using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FSMSceneController : MonoBehaviour
{
    [SerializeField] Button idlebutton;
    [SerializeField] Button movebutton;

    [SerializeField] Character _character;
    // Start is called before the first frame update
    void Start()
    {
        idlebutton.onClick.AddListener(() => _character._state.ChangeState(eActorState.Idle));
        movebutton.onClick.AddListener(() => _character._state.ChangeState(eActorState.Move));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
