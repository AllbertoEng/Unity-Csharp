using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;
    const float phaseLenght = 60; // 1 minuto for each action
    const float phasesInDay = 1440; // (secondsInDay/phaseLenght)

    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;

    float time = 43200;
    [SerializeField] float timeScale = 60f;

    [SerializeField] Text text;
    [SerializeField] Light2D globalLight;

    List<TimeAgent> agents;

    private int days;

    float Hours 
    {
        get { return time/3600;}
    }
    float Minutes
    {
        get { return time % 3600 / 60f; }
    }

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    public void Subscribe(TimeAgent timeAgent)
    {
        agents.Add(timeAgent);
    }

    public void Unsubscribe(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;

        TimeValueCalculations();

        Daylight();

        if (time > secondsInDay)
        {
            NextDay();
        }

        TimeAgents();

        if (Input.GetKeyDown(KeyCode.T))
        {
            SkipTime(hours: 4);
        }
    }

    int oldPhase = -1;
    private void TimeAgents()
    {
        if (oldPhase == -1)
        {
            oldPhase = CalculatePhase();
        }

        //At each phaseLenght executes the delegate
        //EX: phaseLenght at 60, means that every minute ingame the delegate will execute
        int currentPhase = CalculatePhase();

        while (oldPhase < currentPhase)
        {
            oldPhase += 1;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }
        }       
    }

    private int CalculatePhase()
    {
        return Convert.ToInt32(time / phaseLenght) + Convert.ToInt32(days * phaseLenght);
    }

    private void Daylight()
    {
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
    }

    private void TimeValueCalculations()
    {
        int hh = (int)Hours, mm = (int)Minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
    }

    private void NextDay()
    {
        time -= secondsInDay;
        days += 1;
    }

    public void SkipTime(float seconds = 0, float minute = 0, float hours = 0)
    {
        float timeToSkip = seconds;
        timeToSkip += minute * 60;
        timeToSkip += hours * 3600;

        time += timeToSkip;
    }
}
