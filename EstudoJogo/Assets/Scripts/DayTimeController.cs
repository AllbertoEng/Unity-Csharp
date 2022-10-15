using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;
    const float phaseLenght = 900f; // 15 minutos

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
    }

    int oldPhase = 0;
    private void TimeAgents()
    {
        int currentPhase = Convert.ToInt32(time / phaseLenght);

        if (oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }
        }        
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
        time = 0;
        days += 1;
    }
}
