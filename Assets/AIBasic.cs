using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasic : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer spri;
    public float speed = 0.5f;
    private float waitTime;
    public float starWaitTime = 2;
    private int i = 0;
    private Vector2 actialPos;
    //Hai điểm trái phải
    public Transform[] move;     //tran 1

   
    void Start()
    {
        waitTime = starWaitTime; 
    }

    void Update()
    {
        StartCoroutine(CheckEnemy()); 
        transform.position = Vector2.MoveTowards(transform.position, move[i].transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, move[i].transform.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                if (move[i] != move[move.Length - 1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
                waitTime = starWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    IEnumerator CheckEnemy()
    {
        actialPos = transform.position;
        yield return new WaitForSeconds(0.5f);

        if(transform.position.x > actialPos.x)
        {
            spri.flipX = true;
            anim.SetBool("Idle", false);
        }
        else if(transform.position.x < actialPos.x)
        {
            spri.flipX = false;
            anim.SetBool("Idle", false);
        }
        else if (transform.position.x==actialPos.x)
        {
            anim.SetBool("Idle", true);
        }
    }
        
}
