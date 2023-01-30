using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public float fallingTime;

    private TargetJoint2D target;
    private BoxCollider2D bxColl;

    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        bxColl = GetComponent<BoxCollider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Đúng thời gian đặt thì chạy hàm fallings
        if (collision.gameObject.tag == "Player")
        {
            Invoke("Fallings", fallingTime);
        }
    }
    //Rớt cái cục xuống rồi nó mất
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            Destroy(gameObject);
        }
    }
    
    //
    void Fallings()
    {
        target.enabled = false;     //Cho rơi xuống
        bxColl.isTrigger = true;    //Sử dụng istrigger
    }
}
    

