using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0f, 0f, 200f * Time.deltaTime, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null )
            {
                player.AddCoins();
            }
            Destroy(this.gameObject);
        }
    }

}
