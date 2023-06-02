using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherStates
{
    Clear,
    Rain,
    HeavyRain,
    RainAndThunder
}

public class WeatherManager : TimeAgent
{
    [Range(0f, 1f)] [SerializeField] float chanceToChangeWeather = 0.02f;

    WeatherStates currentWeatherState = WeatherStates.Clear;

    [SerializeField] ParticleSystem rainObject;
    [SerializeField] ParticleSystem heavyRain;
    [SerializeField] ParticleSystem rainAndThunder;

    private void Start()
    {
        Init();
        onTimeTick += RandomWeatherChangeCheck;
        UpdateWeather();
    }

    public void RandomWeatherChangeCheck(DayTimeController dayTimeController)
    {
        if (UnityEngine.Random.value < chanceToChangeWeather)
        {
            RandomWeatherChange();
        }
    }

    private void RandomWeatherChange()
    {
        WeatherStates newWeatherState = (WeatherStates)UnityEngine.Random.Range(0, Enum.GetNames(typeof(WeatherStates)).Length);
        ChangeWeather(newWeatherState);
    }

    private void ChangeWeather(WeatherStates newWeatherState)
    {
        currentWeatherState = newWeatherState;
        UpdateWeather();
    }

    private void UpdateWeather()
    {
        rainObject.gameObject.SetActive(false);
        heavyRain.gameObject.SetActive(false);
        rainAndThunder.gameObject.SetActive(false);

        switch (currentWeatherState)
        {
            case WeatherStates.Clear:
                break;
            case WeatherStates.Rain:
                rainObject.gameObject.SetActive(true);
                break;
            case WeatherStates.HeavyRain:
                heavyRain.gameObject.SetActive(true);
                break;
            case WeatherStates.RainAndThunder:
                rainAndThunder.gameObject.SetActive(true);
                break;
        }
    }
}
