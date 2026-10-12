using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TextData
{
    public string text;
    public float startTime;
    public float endTime;

    public TextData(string text, float startTime, float endTime)
    {
        this.text = text;
        this.startTime = startTime;
        this.endTime = endTime;
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
        }
    }

    public void AddText(string text)
    {
        float now = Time.time;
        TextData textData = new TextData(text, now + fadeDelay, now + fadeDelay + fadeLength);
        currentText.Add(textData);
        bottomText.text += text + "\n";
    }
}
