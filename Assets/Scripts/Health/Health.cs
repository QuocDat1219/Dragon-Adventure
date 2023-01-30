using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;  //tạo biến khi mất máu và không thể nhận xác thương tiếp trong 1 khoảng thời gian

    [Header("Death Sound")]
    [SerializeField] private AudioClip amthanhchet;
    [SerializeField] private AudioClip amthanhbithuong;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    //Nhận sát thương
    public void TakeDamage(float _damage)
    {
        //trừ cục máu trên ui
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0) //Còn máu
        {
            //Người chơi mất máu
            anim.SetTrigger("hurt");    
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(amthanhbithuong);
        }
        else
        {
            //Người chơi hi sinh
            if (!dead)
            {
                //Hủy kích hoạt tất cả các lớp thành phần đính kèm của nhân vật và lính khi chết
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                dead = true;
                SoundManager.instance.PlaySound(amthanhchet);
            }
        }
    }

    //Cộng thêm máu cho nhân vật
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    
    private IEnumerator Invunerability()
    {
        invulnerable = true;    //Bật không nhận sát thương
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            //Chớp chớp nhấp nháy đỏ trắng 1 khoảng thời gian
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));  
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;   //Tắt bất tử để có thể nhận sát thương
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    //Reset lại tất cả trạng thái của nhân vật
    public void Respawn()
    {
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());   //Bật không thể nhận sát thương sau khi hồi sinh 1 khoảng thời gian
        dead = false;

        //Kích hoạt lại tất cả các lớp thành phần đính kèm của nhân vật và lính khi chết
        foreach (Behaviour component in components)
            component.enabled = true;
    }
}