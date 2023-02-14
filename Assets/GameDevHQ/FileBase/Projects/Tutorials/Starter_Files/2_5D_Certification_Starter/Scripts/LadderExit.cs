using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderExit : MonoBehaviour
{
    private Vector3 _standPos;
    [SerializeField] private GameObject _standPosTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge_Grab_Checker"))
        {
            var player = other.transform.parent.GetComponent<Player>();
            if (player != null )
            {
                player.ExitLadder();
            }
        }
    }

    public Vector3 GetLadderStandPos()
    {
        _standPos = _standPosTarget.transform.position;
        return _standPos;
    }
}
