using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform _targetA, _targetB;
    [SerializeField] float _speed = 3.0f;
    private bool _switching = false;
   

    // Update is called once per frame
    void Update()
    {
        if (_switching == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);
        }
        else if (_switching == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
        }

        if (transform.position == _targetA.position)
        {
            _switching = true;
        }
        else if (transform.position == _targetB.position)
        {
            _switching = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
