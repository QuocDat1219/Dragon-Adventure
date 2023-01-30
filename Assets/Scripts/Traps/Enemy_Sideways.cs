using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float khoancachdichuyeh;
    [SerializeField] private float speed;       //Tốc độ
    [SerializeField] private float damage;      //Đặt thông số damage
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - khoancachdichuyeh;
        rightEdge = transform.position.x + khoancachdichuyeh;
    }

    private void Update()
    {
        //Máy cưa chạy qua chạy lại giữa 2 điểm
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);        //Tấn công nhân vật khi chạm vào
        }
    }
}