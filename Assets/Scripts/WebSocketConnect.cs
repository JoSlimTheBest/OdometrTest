using System.Collections;
using UnityEngine;
using WebSocketSharp;
using TMPro;
using System.Globalization;
using UnityEngine.UI;



public class WebSocketConnect : MonoBehaviour
{
    WebSocket ws;
    public TextMeshProUGUI odomText;
    
    public float currentOdometr;
    public float currentOdometrNew;
    public string str;
    private bool wait;
    public Image lamp;
    public Sprite connectOpen;
    public TakePort portServ;
    private void Start()
    {
        //Debug.Log("ws://" + portServ.adress[0] + ":" + portServ.adress[1] + "/ws");
       // ws = new WebSocket("ws://" + portServ.adress[0] + ":"+ portServ.adress[1] +"/ws");
        ws = new WebSocket(  "ws://185.246.65.199:9090/ws");




        ws.OnMessage += (sender, e) => { 
            Debug.Log(e.Data);
            str = e.Data.ToString();
          
            
        };

        ws.Connect();
        

        lamp.sprite = connectOpen; //change Tmrw

    }
    IEnumerator ChangeFloat(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        wait = true;
        while (elapsed < duration)
        {
            currentOdometr = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        currentOdometr = v_end;
        wait = false;
    }


    private void ChangeOdometrResult()
    {
        if (str.Length < 37)
        {
            return;
        }
        str = str.Remove(0, 37);
        
        int last = 2;

        str = str.RemoveEnd(last);
        Debug.Log(str + "Read");
        currentOdometrNew = float.Parse(str, CultureInfo.InvariantCulture.NumberFormat);

    }

    private void FixedUpdate()
    {
        if(currentOdometr != currentOdometrNew && wait ==false)
        {
            StartCoroutine(ChangeFloat(currentOdometr, currentOdometrNew, 3));
        }
        else
        {
            ws.Send("getCurrentOdometer");
        }
        odomText.text = currentOdometr.ToString();
        ChangeOdometrResult();
    }
   
}


public static class StringExtensions
{
    public static string RemoveEnd(this string str, int len)
    {
        if (str.Length < len)
        {
            return string.Empty;
        }

        return str.Substring(0, str.Length - len);
    }
}
