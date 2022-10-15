using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public Action onTimeTick;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        GameManager.instance.timeController.Subscribe(this);
    }

    public void Invoke()
    {
        onTimeTick?.Invoke();
    }

    private void OnDestroy()
    {
        GameManager.instance.timeController.Unsubscribe(this);
    }

}
