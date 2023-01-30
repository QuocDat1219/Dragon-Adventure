using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false);  //dừng di chuyển và tấn công nhân vật
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)    //tọa độ của lính di chuyển đến điểm đánh dấu bên trái
                MoveInDirection(-1);
            else
                DirectionChange();      //thay đổi hướng đi
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)   //tọa độ của lính di chuyển đến điểm đánh dấu bên phải  
                MoveInDirection(1);
            else
                DirectionChange();      //thay đổi hướng đi
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)    //dừng lại 1 khoảng thời gian khi di chuyển đến điểm trái hoặc phải
            movingLeft = !movingLeft;   //Đổi hướng di chuyển
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);

        //Quay mặt của lính theo hướng di chuyển
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}