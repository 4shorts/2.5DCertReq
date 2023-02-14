using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private GameObject _respawnPoint;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
            }

            other.transform.position = _respawnPoint.transform.position;
            StartCoroutine(CCEnableRoutine(cc));
            
        }
    }

    IEnumerator CCEnableRoutine(CharacterController controller)
    {
        yield return new WaitForSeconds(.5f);
        controller.enabled = true;
    }
}
