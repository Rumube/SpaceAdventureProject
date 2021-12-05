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
        CalcNoise(true);
    }

    void CalcNoise(bool isStart)
    {
        //DestroyAllAsteroids();


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
                        GameObject newAsteroid = Instantiate(_asteroid, transform);
                        newAsteroid.transform.position = new Vector3(x, y, z);
                    }
                }
            }
        }
        _lastPos = _ship.transform.localPosition;
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

    /*
     0.14
     25
     0.3
     */

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            UpdateAsteroids();

        if (NeedUpdate())
            CalcNoise(false);
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
