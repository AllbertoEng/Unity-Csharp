using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public Action<DayTimeController> onTimeTick;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        if (GameManager.instance != null)
            GameManager.instance.timeController.Subscribe(this);        
    }

    public void Invoke(DayTimeController dayTimeController)
    {
        onTimeTick?.Invoke(dayTimeController);
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
            GameManager.instance.timeController.Unsubscribe(this);

    }

}
