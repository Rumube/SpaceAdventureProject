using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //Importaciones
    [Header("Importaciones")]
    Rigidbody _rb;

    [Header("Parametros Movimiento")]
    public float[] _velocity = new float[5];
    public int _currentVelocity = 1;
    public int _currentInc = 0;
    public float _currentGiro = 0;
    public float _velRotation;
    public float _velGiro;
    int _giro;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Client.Instance._connected)
        {
            UpdateControllData();
            CalculeGiro();
            MoveShip();
            Client.Instance.SendShipData(gameObject);
        }

    }

    void CalculeGiro()
    {
        switch (_giro)
        {
            case -1:
                //Izquierda
                _currentGiro = transform.rotation.eulerAngles.y - 10;
                break;
            case 0:
                //No girar

                break;
            case 1:
                //Derecha
                _currentGiro = transform.rotation.eulerAngles.y + 10;
                break;
        }
    }

    void UpdateControllData()
    {
        _currentVelocity = Client.Instance._controllData.currentVel;
        _currentInc = Client.Instance._controllData.currentInc;
        _giro = Client.Instance._controllData.giro;
    }

    void MoveShip()
    {
        _rb.AddRelativeForce(Vector3.forward * _velocity[_currentVelocity] * Time.deltaTime, ForceMode.Impulse);
        Quaternion newQ = Quaternion.Euler(_currentInc, _currentGiro, transform.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, newQ, Time.deltaTime * _velRotation);
    }

    public void setCurrentVelocity(int value)
    {
        _currentVelocity = value;
    }

}
