using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Testb;

public class EventButton : MonoBehaviour
{
    [SerializeField] Button _eventButton;
    // Start is called before the first frame update
    void Start()
    {
        _eventButton.onClick.AddListener(SendeventMessage);
    }

    void SendeventMessage()
    {
        Message.Send<TestButtonMessage>(new TestButtonMessage());
    }
}
