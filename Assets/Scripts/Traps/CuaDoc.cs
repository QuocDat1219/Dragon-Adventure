using UnityEngine;

public class CuaDoc : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;       //Tốc độ
    [SerializeField] private float damage;      //Đặt thông số damage
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.y - movementDistance;
        rightEdge = transform.position.y + movementDistance;
    }

    private void Update()
    {
        //Máy cưa chạy qua chạy lại giữa 2 điểm
        if (movingLeft)
        {
            if (transform.position.y > leftEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.y < rightEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
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