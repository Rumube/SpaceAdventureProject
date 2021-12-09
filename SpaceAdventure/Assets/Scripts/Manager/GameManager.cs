using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    int _idGame;
    float _energy;
    float _health;

    //REFERENCAS
    public GameObject _ship;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _ship = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setIdGame(int value)
    {
        _idGame = value;
        _ship.transform.position = new Vector3(_idGame, 0, 0);
    }

    public int getId()
    {
        return _idGame;
    }
}
