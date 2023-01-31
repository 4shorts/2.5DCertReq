using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    private Vector3 _handPos, _standPos;
    [SerializeField] private GameObject _handPosTarget;
    [SerializeField] private GameObject _standPosTarget;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge_Grab_Checker"))
        {
            var player = other.transform.parent.GetComponent<Player>();
            
            if (player != null)
            {
                _handPos = _handPosTarget.transform.position;
                player.GrabLedge(_handPos, this);
            }
        }
    }

    public Vector3 GetStandPos()
    {
        _standPos = _standPosTarget.transform.position;
        return _standPos;
    }
}
