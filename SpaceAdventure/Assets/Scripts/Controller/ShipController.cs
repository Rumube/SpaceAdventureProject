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
    [Range(0, 4)]
    public int _currentVelocity;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveShip();
    }

    void MoveShip()
    {
        _rb.AddForce(Vector3.forward * _velocity[_currentVelocity] * Time.deltaTime, ForceMode.Impulse);
    }
}
