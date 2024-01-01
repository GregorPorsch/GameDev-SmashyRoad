using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    Transform player;

    public GameObject playerMarker;

    private void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;

        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);

        playerMarker.transform.position = new Vector3(player.position.x, playerMarker.transform.position.y, player.position.z);
        playerMarker.transform.rotation = Quaternion.Euler(0f, player.eulerAngles.y, 0f);
    }
}
