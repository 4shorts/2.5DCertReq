using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Vector3 _snapPoint;
    [SerializeField] private GameObject _snapPointTarget;

    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge_Grab_Checker"))
        {
            var player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                _snapPoint = _snapPointTarget.transform.position;
                player.GrabLadder(_snapPoint, this);
            }
        }
    }
}
