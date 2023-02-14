using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Transform _targetA, _targetB;
    [SerializeField] private float _speed = 3f;
    private bool _goingDown;

    private void Update()
    {
        if (_goingDown == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
        }
        else if (_goingDown == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);
        }
    }

    public void CallElevator()
    {
        _goingDown = !_goingDown;
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
