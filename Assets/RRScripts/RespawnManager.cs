using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform respawnPoint;
    public Transform player;
   
    public void RespawnPlayer(GameObject respawn)
    {
        player.transform.position = respawnPoint.position;
    }
}
