using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")] //Thời gian lơ lửng khi nhảy từ trên cao xuống
    [SerializeField] private float coyoteTime; //Khoảng thời gian ngời chơi lơ lửng trên không trước khi nhảy thêm
    private float coyoteCounter; //Bao nhiêu thời gian trôi qua kể từ khi người chơi chạy ra rìa

    [Header("Multiple Jumps")]  //Nhảy 2 3 4 lần
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jumping")]     //Leo tường
    [SerializeField] private float wallJumpX;   //Lực nhảy tường ra ngoài 1 đoạn rồi dính lại vào tường
    [SerializeField] private float wallJumpY;   ///Lực nhảy tường leo lên cao

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;     //Layer đất
    [SerializeField] private LayerMask wallLayer;       //Layer tường

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;     //Leo tường
    private float horizontalInput;

    private void Awake()
    {
         //Lấy tham chiếu từ các component của đối tượng
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //quay qua quay lại
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Tùy chỉnh các animation
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Nhảy bình thường
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //Chỉnh độ cao khi nhảy (nhảy cao)
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime;         //Đặt lại biến thời gian rơi khi ở trên mặt đất
                jumpCounter = extraJumps;           //Đặt lại biến đếm bước nhảy bằng biến extrajump
            }
            else
                coyoteCounter -= Time.deltaTime;    //Bắt đầu giảm bớt dần thời gian rơi khi không ở trên mặt đất
        }
    }

    private void Jump()
    {
        //Nếu biến thời gian từ 0 trở xuống và k ở trên tường và k nhảy thêm bước nào kh thì k làm gì
        if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) return; 

        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
            WallJump();
        else
        {
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                //Nếu không ở trên mặt đất và biến đếm thời gian lớn hơn 0, nhảy bình thường
                if (coyoteCounter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else
                {
                    if (jumpCounter > 0) //Nếu nhảy thêm 1 lần thì thực hiện nhảy và giảm biến đếm bước nhảy
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            //Reset lại biến đếm thời gian sau khi nhảy 2 lần
            coyoteCounter = 0;
        }
    }

    //Nhảy trên tường
    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

   
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    //Không cho tấn công khi đang chạy
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}