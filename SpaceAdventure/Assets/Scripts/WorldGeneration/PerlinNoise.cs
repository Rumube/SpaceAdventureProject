using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{

    /*CONFIGURACIÓN
     0.14
     25
     0.3
     */

    
    private GameObject _ship;
    [Header("References")]
    public GameObject _asteroid;
    public GameObject _ChargeRock;

    [Header("Game Parameters")]
    public float scale = 1.0F;
    public float _range;
    public float _spawnRate;
    public Vector3 _lastPos;
    public float _chanceChargeRock;
    public float _timeToUpdate;

    float _nextUpdate;
    System.Random _rng;
    void Start()
    {
        _ship = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartGenerateWorld()
    {
        _nextUpdate = Time.realtimeSinceStartup + _timeToUpdate;
        _rng = new System.Random(GameManager.Instance.getId());
        CalcNoise(true);
    }

    void CalcNoise(bool isStart)
    {

        //X LIMIT
        float xLimitSup;
        float xLimitInf;
        //Y LIMIT
        float yLimitSup;
        float yLimitInf;
        //Z LIMIT
        float zLimitSup;
        float zLimitInf;

        if (isStart)
        {
            //X LIMIT
            xLimitSup = _ship.transform.position.x + _range;
            xLimitInf = _ship.transform.position.x - _range;
            //Y LIMIT
            yLimitSup = _ship.transform.position.y + _range;
            yLimitInf = _ship.transform.position.y - _range;
            //Z LIMIT
            zLimitSup = _ship.transform.position.z + _range;
            zLimitInf = _ship.transform.position.z - _range;
        }else
        {
            //X LIMIT
            xLimitSup = _ship.transform.position.x + _range;
            xLimitInf = _ship.transform.position.x - _range;
            //Y LIMIT
            yLimitSup = _ship.transform.position.y + _range;
            yLimitInf = _ship.transform.position.y - _range;
            //Z LIMIT
            zLimitSup = _ship.transform.position.z + _range;
            zLimitInf = _ship.transform.position.z - _range;
        }

        for (int x = (int)xLimitInf; x < (int)xLimitSup; x++)
        {
            for (int y = (int)yLimitInf; y < yLimitSup + _range; y++)
            {
                for (int z = (int)zLimitInf; z <zLimitSup; z++)
                {
                    if (Perlin3D(x, y, z) <= _spawnRate)
                    {
                        GameObject newObject;
                        if (isChargeRock())
                        {
                            newObject = Instantiate(_ChargeRock, transform);
                        }
                        else
                        {
                            newObject = Instantiate(_asteroid, transform);
                        }
                        newObject.transform.position = new Vector3(x, y, z);
                    }
                }
            }
        }
        _lastPos = _ship.transform.localPosition;
    }

    bool isChargeRock()
    {
        bool result = false;
        int chance = _rng.Next(1, 100);

        if (_chanceChargeRock < chance)
            result = true;

        return result;
    }

    public float Perlin3D(float x, float y, float z)
    {
        x *= scale;
        y *= scale;
        z *= scale;

        float AB = Mathf.PerlinNoise(x, y);
        float BC = Mathf.PerlinNoise(y, z);
        float AC = Mathf.PerlinNoise(x, z);

        float BA = Mathf.PerlinNoise(y, x);
        float CB = Mathf.PerlinNoise(z, y);
        float CA = Mathf.PerlinNoise(z, x);

        float ABC = AB + BC + AC + BA + CB + CA;
        return ABC / 6f;

    }


    void Update()
    {
        if (Client.Instance._connected)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                UpdateAsteroids();

            if (NeedUpdate())
                UpdateAsteroids();
        }

    }

    bool NeedUpdate()
    {
        
        bool result = false;
        /*
        //X LIMIT
        float xLimitSup = _lastPos.x + (_range / 2);
        float xLimitInf = _lastPos.x - (_range / 2);
        //Y LIMIT
        float yLimitSup = _lastPos.y + (_range / 2);
        float yLimitInf = _lastPos.y - (_range / 2);
        //Z LIMIT
        float zLimitSup = _lastPos.z + (_range / 2);
        float zLimitInf = _lastPos.z - (_range / 2);

        Vector3 currentPos = _ship.transform.position;

        if (currentPos.x >= xLimitSup || currentPos.x <= xLimitInf || currentPos.y >= yLimitSup || currentPos.y <= yLimitInf || currentPos.z >= zLimitSup || currentPos.z <= zLimitInf)
            result = true;

        */

        if (Time.realtimeSinceStartup >= _nextUpdate)
        {
            result = true;
            _nextUpdate = Time.realtimeSinceStartup + _timeToUpdate;
        }
        return result;
    }

    void DestroyAllAsteroids()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject currentAsteroid in asteroids)
        {
            Destroy(currentAsteroid);
        }
    }

    void UpdateAsteroids()
    {
        DestroyAllAsteroids();
        CalcNoise(false);
    }
}
