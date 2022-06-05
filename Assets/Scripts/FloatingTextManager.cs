using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> _floatingTexts = new List<FloatingText>();

    private void Update()
    {
        foreach (FloatingText txt in _floatingTexts)
            txt.UpdateFloatingText();
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.text.text = msg;
        floatingText.text.fontSize = fontSize;
        floatingText.text.color = color;
        // Transfer world space to screen space so we can use it in the UI
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.motion = motion;
        floatingText.duration = duration;
        
        floatingText.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = _floatingTexts.Find(t => !t.active);
        if (txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab, textContainer.transform);
            txt.text = txt.go.GetComponent<Text>();
            
            _floatingTexts.Add(txt);
        }

        return txt;
    }
}
