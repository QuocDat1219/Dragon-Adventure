using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camere : MonoBehaviour
{
    [SerializeField] private Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
