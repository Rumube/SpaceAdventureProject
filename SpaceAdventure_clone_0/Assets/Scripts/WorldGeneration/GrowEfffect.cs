using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowEfffect : MonoBehaviour
{
    Vector3 _finalScale;
    Vector3 _currentScale;
    public float _velocity;

    // Start is called before the first frame update
    void Start()
    {
        _finalScale = transform.localScale;
        transform.localScale = new Vector3(0, 0, 0);
        _currentScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentScale.x < _finalScale.x)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            _currentScale = transform.localScale;
        }
    }
}
