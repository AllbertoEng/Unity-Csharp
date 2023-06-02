using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    DisableControl disableControl;
    Character character;
    DayTimeController dayTimeController;

    private void Awake()
    {
        disableControl = GetComponent<DisableControl>();
        character = GetComponent<Character>();
        dayTimeController = GameManager.instance.timeController;
    }
    internal void DoSleep()
    {
        StartCoroutine(SleepRoutine());
    }

    IEnumerator SleepRoutine()
    {
        ScreenTint screenTint = GameManager.instance.screenTint;

        disableControl.DisableControls();

        screenTint.Tint();
        yield return new WaitForSeconds(2f);

        character.FullHeal();
        character.FullRest();
        dayTimeController.SkipToMorning();

        screenTint.UnTint();
        yield return new WaitForSeconds(2f);

        disableControl.EnableControls();

        yield return null;
    }
}
