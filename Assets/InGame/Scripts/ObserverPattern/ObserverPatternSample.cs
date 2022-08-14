using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Testb;

public class ObserverPatternSample : MonoBehaviour
{
    [SerializeField] Text _text;
    int _index;
    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
        Message.AddMessage<TestButtonMessage>(GetEventMessage);
    }

    private void OnDestroy()
    {
        Message.RemoveMessage<TestButtonMessage>(GetEventMessage);
    }

    void GetEventMessage(TestButtonMessage msg)
    {
        _text.text = $"버튼이 {++_index}번 눌렸습니다";
    }
}
