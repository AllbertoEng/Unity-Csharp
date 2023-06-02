using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacter : TimeAgent
{
    public NPCDefinition character;

    [Range(0f,1f)]
    public float relationship;
    public bool talkedToday;
    public int talkedOnTheDayNumber = -1;

    private void Start()
    {
        Init();
        onTimeTick += ResetTalkState;
    }

    internal void IncreaseRelationship(float v)
    {
        if (!talkedToday)
        {
            relationship += v;
            talkedToday = true;
        }        
    }

    void ResetTalkState(DayTimeController dayTimeController)
    {
        if (dayTimeController.days != talkedOnTheDayNumber)
        {
            talkedToday = false;
            talkedOnTheDayNumber = dayTimeController.days;
        }
        
    }

}
