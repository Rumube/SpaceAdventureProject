using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Client : MonoBehaviour
{

    WebSocket _ws;
    public string _miNombre;

    // Start is called before the first frame update
    void Start()
    {
        _ws = new WebSocket("ws://10.2.96.13:8080");
        _ws.OnMessage += Ws_OnMessage;
        _ws.Connect();
    }

    //COGER DATOS
    private void Ws_OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log(e.Data);
    }

    private void OnDisable()
    {
        _ws.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ENVIAR DATOS
            _ws.Send("Soy " + _miNombre);
        }
    }
}
