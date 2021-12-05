using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [Header("Game Parameters")]
    public float scale = 1.0F;
    private GameObject _ship;
    public float _range;
    public float _spawnRate;
    public GameObject _asteroid;
    public Vector3 _lastPos;

    void Start()
    {
        _ship = GameObject.FindGameObjectWithTag("Player");
        CalcNoise();
    }

    void CalcNoise()
    {
        /*
        GameObject[] asteroidList = GameObject.FindGameObjectsWithTag("Asteroid");

        foreach (GameObject currentAsteroid in asteroidList)
        {
            Destroy(currentAsteroid);
        }
        */
        for (int x = (int)(_ship.transform.position.x - _range); x < _ship.transform.position.x + _range; x++)
        {
            for (int y = (int)(_ship.transform.position.y - _range); y < _ship.transform.position.y + _range; y++)
            {
                for (int z = (int)(_ship.transform.position.z - _range); z < _ship.transform.position.z + _range; z++)
                {
                    if (Perlin3D(x, y, z) <= _spawnRate)
                    {
                        GameObject newAsteroid = Instantiate(_asteroid, transform);
                        newAsteroid.transform.position = new Vector3(x, y, z);
                    }
                }
            }
        }
        _lastPos = _ship.transform.position;
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
        if (NeedUpdate())
            CalcNoise();
    }

    bool NeedUpdate()
    {
        bool result = false;
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


        return result;
    }
}
