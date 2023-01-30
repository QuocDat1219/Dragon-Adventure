using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip pickupSound;

    [Header("Âm thanh hồi máu")]
    [SerializeField] private AudioClip hoimauSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(hoimauSound);
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);    //Tắt không hiển thị gameoject nữa
        }
    }
}