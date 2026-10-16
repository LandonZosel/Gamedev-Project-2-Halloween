using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using System.Linq;

public class TextData
{
    public string text;
    public float startTime;
    public float endTime;
    public bool trampoline;
    public bool ghost;

    public TextData(string text, float startTime, float endTime, bool trampoline, bool ghost)
    {
        this.text = text;
        this.startTime = startTime;
        this.endTime = endTime;
        this.trampoline = trampoline;
        this.ghost = ghost;
    }
}

public class BottomTextController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text bottomText;
    [SerializeField]
    private float fadeLength;
    [SerializeField]
    private float fadeDelay;
    private List<TextData> currentText = new List<TextData>();
    private int trampolineMultiplier = 0;
    private int ghostMultiplier = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bottomText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        bottomText.text = "";
        for (int i = 0; i < currentText.Count; i++)
        {
            float now = Time.time;
            float t = Mathf.Clamp01((now - currentText[i].startTime) / (currentText[i].endTime - currentText[i].startTime));
            
            byte alphaValue = (byte)Mathf.Lerp(255, 0, t);

            string alphaHex = alphaValue.ToString("X2");

            bottomText.text += "<alpha=#" + alphaHex + ">" + currentText[i].text + "\n";

            if (Time.time >= currentText[i].endTime)
            {
                if (currentText[i].trampoline)
                {
                    trampolineMultiplier = 0;
                }
                currentText.Remove(currentText[i]);
            }
        }
    }

    public void AddText(string text)
    {
        float now = Time.time;
        TextData textData = new TextData(text, now + fadeDelay, now + fadeDelay + fadeLength, false, false);
        currentText.Add(textData);
        bottomText.text += text + "\n";
    }

    public int AddTrampolineText()
    {
        string text = "Bounced on a trampoline!";
        TextData textData;
        trampolineMultiplier++;
        if (trampolineMultiplier > 1)
        {
            text += " X" + trampolineMultiplier;
            IEnumerable<TextData> textDataList = currentText.Where(data => data.trampoline == true);
            currentText.Remove(textDataList.First());
        }
        float now = Time.time;
        textData = new TextData(text, now + fadeDelay, now + fadeDelay + fadeLength, true, false);
        currentText.Add(textData);
        bottomText.text += text + "\n";
        return trampolineMultiplier;
    }

    public int AddGhostBurnText(bool fire2)
    {
        string text = "Ghost BURNED";
        TextData textData;
        if (fire2)
        {
            ghostMultiplier++;
        }
        ghostMultiplier++;
        if (ghostMultiplier > 1)
        {
            text += " X" + ghostMultiplier;
            IEnumerable<TextData> textDataList = currentText.Where(data => data.ghost == true);
            currentText.Remove(textDataList.First());
        }
        float now = Time.time;
        textData = new TextData(text, now + fadeDelay, now + fadeDelay + fadeLength, false, true);
        currentText.Add(textData);
        bottomText.text += text + "\n";
        return ghostMultiplier;
    }
}
