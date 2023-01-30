using UnityEngine;

public class EnemyProjectile : EnemyDamage      //Gọi qua lớp cha để nhận damage khi bị tấn công
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;

    [Header ("Am thanh khi va cham")]
    [SerializeField] private AudioClip vachamdanlua;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        //tạo các thuộc tính cho đạn lửa
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Đạn lửa của lính bắn ra chạm vào tường hay đất sẽ phát nổ
        hit = true;
        base.OnTriggerEnter2D(collision);   //Chạy hàm ontrigger bên lớp enemydamage trước
        coll.enabled = false;

        if (anim != null){
            anim.SetTrigger("explode");     // Khi đối tượng là một quả cầu lửa, nó sẽ nổ tung
            SoundManager.instance.PlaySound(vachamdanlua);
        }
        else
            gameObject.SetActive(false);    //Mũi tên biến mât khi chạm vào bất kì object nào
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}