using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chim : MonoBehaviour
{
    public GameObject player;
    private Transform playerPos;
    private Vector2 currenPos;
    public float distane;
    public float speedEnemy;
    private Animator anim;

    private void Start()
    {
        playerPos = player.GetComponent<Transform>();
        currenPos = GetComponent<Transform>().position;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, playerPos.position) < distane)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speedEnemy * Time.deltaTime);
            anim.SetBool("Fly", true);
        }
        else
        {
            if (Vector2.Distance(transform.position, currenPos) <= 0)
            {
                anim.SetBool("Fly", false);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, currenPos, speedEnemy * Time.deltaTime);
                anim.SetBool("Fly", true);
            }

        }
        Flip();
    }
    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
