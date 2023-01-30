using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private bool triggered; //khi người chơi đi vào bẫy
    private bool active; //Khi bẫy hoạt động làm mất máu người chơi

    private Health playerHealth;    //Máu nhân vật

    private void Awake()
    {
        //trừ máu của người chơi khi bẫy lửa được kích hoạt
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerHealth != null && active)
            playerHealth.TakeDamage(damage);
    }

     //Bắt sự kiện nhân vật chạm vào bẫy khi kích hoạt gây sát thương cho nhân vật khi ở trên bẫy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            if (!triggered)
                StartCoroutine(ActivateFiretrap());

            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    //Bắt sự kiện khi nhân vật đi ra ngoài bẫy
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerHealth = null;
    }
    private IEnumerator ActivateFiretrap()
    {
        //chuyển bẫy qua màu đỏ cảnh báo nguy hiểm
        triggered = true;
        spriteRend.color = Color.red;

        // Chờ một thời gian, kích hoạt bẫy, bật hoạt ảnh, màu trở lại bình thường
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white; //Tắt cảnh báo
        active = true;
        anim.SetBool("activated", true);

        //Chờ một thời gian x để reset lại tất cả trạng thái của bẫy về ban đầu
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}