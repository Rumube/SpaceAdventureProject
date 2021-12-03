using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Client : MonoBehaviour
{
    private static Client _instance;
    public static Client Instance { get { return _instance; } }

    WebSocket _ws;
    public string _miNombre;

    // Start is called before the first frame update
    void Start()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        //CLASE
        //_ws = new WebSocket("ws://10.2.96.13:8080");
        //CASA
        _ws = new WebSocket("ws://192.168.0.158:8080");
        _ws.OnMessage += Ws_OnMessage;
        _ws.Connect();
    }

    //COGER DATOS
    private void Ws_OnMessage(object sender, MessageEventArgs e)
    {
        var data = (JObject)JsonConvert.DeserializeObject(e.Data);
        switch (int.Parse(data["type"].Value<string>())){
            case 0:
                print("Posición");
                break;

            case 1:
                print("Mensaje");
                break;
        }
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
            _ws.Send(JsonConvert.SerializeObject(new {type = 1, num = 26, posX = 0, posY = 2, posZ = 0 }));
        }
    }

    public void SendDataPos(GameObject newData)
    {
        ShipData shipData = new ShipData();
        shipData.position = newData.transform.position;
        shipData.health = 1;

        _ws.Send(JsonConvert.SerializeObject(shipData));
    }

    private class ShipData
    {
        public Vector3 position;
        public int health;
    }
}
