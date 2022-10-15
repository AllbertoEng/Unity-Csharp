using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] Text targetText;
    [SerializeField] Text nameText;
    [SerializeField] Image portrait;

    DialogContainer currentDialogue;
    int currentTextLine;

    [Range(0f,1f)]
    [SerializeField] float visibleTextPercent;
    [SerializeField] float timePerLetter = 0.05f;
    float totalTimeToType, currentTime;
    string lineToShow;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushText();
        }
        TypeOutText();
    }

    private void TypeOutText()
    {
        if (visibleTextPercent >= 1f)
            return;

        currentTime += Time.deltaTime;
        visibleTextPercent = currentTime / totalTimeToType;
        visibleTextPercent = Mathf.Clamp(visibleTextPercent, 0, 1f);
        UpdateText();
    }

    private void UpdateText()
    {
        int letterCount = Convert.ToInt32(lineToShow.Length * visibleTextPercent);
        targetText.text = lineToShow.Substring(0, letterCount);
    }

    private void PushText()
    {        
        if (visibleTextPercent < 1f)
        {
            visibleTextPercent = 1f;
            UpdateText();
            return;
        }

        if (currentTextLine >= currentDialogue.line.Count)
        {
            Conclude();
        }
        else
        {
            CycleLine();
        }
    }

    public void Initialize(DialogContainer dialogContainer)
    {
        Show(true);
        currentDialogue = dialogContainer;
        currentTextLine = 0;
        CycleLine();
        UpdatePortrait();
    }

    private void UpdatePortrait()
    {
        portrait.sprite = currentDialogue.actor.portrait;
        nameText.text = currentDialogue.actor.Name;
    }

    void CycleLine()
    {
        lineToShow = currentDialogue.line[currentTextLine];
        totalTimeToType = lineToShow.Length * timePerLetter;
        currentTime = 0f;
        visibleTextPercent = 0f;
        targetText.text = " ";

        currentTextLine += 1;
    }

    private void Conclude()
    {
        Debug.Log("dialogue finished");
        Show(false);
    }

    private void Show(bool v)
    {
        gameObject.SetActive(v);
    }
}
