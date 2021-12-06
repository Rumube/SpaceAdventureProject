using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //Importaciones
    [Header("Importaciones")]
    Rigidbody _rb;

    [Header("Parametros Movimiento")]
    public int[] _velocity = new int[5];
    public int _currentVelocity = 0;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateControllData();
        MoveShip();
        Client.Instance.SendShipData(gameObject);
    }

    void UpdateControllData()
    {
        _currentVelocity = Client.Instance._controllData.currentVel;
    }

    void MoveShip()
    {
        _rb.AddRelativeForce(Vector3.forward * _velocity[_currentVelocity] * Time.deltaTime, ForceMode.Impulse);
    }

    public void setCurrentVelocity(int value)
    {
        print("VEL" + value);
        _currentVelocity = value;
    }

}
