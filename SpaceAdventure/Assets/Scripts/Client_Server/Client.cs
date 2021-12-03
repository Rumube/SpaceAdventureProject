using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Client : MonoBehaviour
{
    public bool _enCasa;
    private static Client _instance;
    public static Client Instance { get { return _instance; } }

    WebSocket _ws;
    public string _miNombre;
    public string _msg;

    #region StartEndRegion
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

        if(_enCasa)
            _ws = new WebSocket("ws://192.168.0.158:8080");
        else if(!_enCasa)
            _ws = new WebSocket("ws://10.2.96.13:8080");

        _ws.OnMessage += Ws_OnMessage;
        _ws.Connect();
    }

    private void OnDisable()
    {
        _ws.Close();
    }
    #endregion

    #region Metodos_Enviar

    public void SendShipData(GameObject newData)
    {
        ShipData shipData = new ShipData();
        shipData.positionX = newData.transform.position.x;
        shipData.positionY = newData.transform.position.y;
        shipData.positionZ = newData.transform.position.z;
        shipData.health = 1;

        _ws.Send(JsonConvert.SerializeObject(shipData));
    }

    public void SendNewMessage(string msg)
    {
        Mensaje mensaje = new Mensaje();
        mensaje.msg = msg;
        _ws.Send(JsonConvert.SerializeObject(mensaje));
    }

    #endregion

    #region Clases_Enviar
    //DATOS A ENVIAR

    private class ShipData
    {
        public int type = 0;
        public float positionX;
        public float positionY;
        public float positionZ;
        public int health;
    }

    private class Mensaje
    {
        public int type = 1;
        public string msg;
    }

    #endregion

    #region GET_DATA
    //GET DATOS
    private void Ws_OnMessage(object sender, MessageEventArgs e)
    {
        var data = (JObject)JsonConvert.DeserializeObject(e.Data);
        switch (int.Parse(data["type"].Value<string>()))
        {
            case 0://ShipData
                ShipData ship = JsonConvert.DeserializeObject<ShipData>(e.Data);
                GetShipData(ship);
                break;

            case 1://Mensajes
                Mensaje mensaje = JsonConvert.DeserializeObject<Mensaje>(e.Data);
                GetMessage(mensaje);
                break;

            default://NULL
                print("Tipo no definido");
                break;
        }
    }

    private void GetShipData(ShipData shipData)
    {
        print("Pos: " + shipData.positionX + " " + shipData.positionY + " " + shipData.positionZ);
    }

    private void GetMessage(Mensaje mensaje)
    {
        print("Mensaje: " + mensaje.msg);
    }

    #endregion
}
