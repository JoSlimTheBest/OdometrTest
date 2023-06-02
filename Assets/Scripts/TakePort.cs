using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TakePort : MonoBehaviour
{
    public TextMeshProUGUI port;
    public TextMeshProUGUI server;
    public List<string> adress;
    private void Start(){  
        TextAsset config = (TextAsset)Resources.Load("Config", typeof(TextAsset));
        adress = new List<string>(config.text.Split('\n'));

        server.text = adress[0];
        port.text = adress[1];
       
    }
}
