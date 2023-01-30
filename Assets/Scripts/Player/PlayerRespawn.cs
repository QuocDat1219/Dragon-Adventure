using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip amthanhluu;  //Âm thanh khi nhân vật chạm vào cột lưu điểm mới
    [SerializeField] private AudioClip amthanhhoisinh;
    private Transform currentCheckpoint;        //Lưu vị trí cuối cùng của nhân vật
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void RespawnCheck()
    {
        //Kiểm tra xem có lưu hay chưa
        if (currentCheckpoint == null) 
        {
            //Hiển thị màn hình kết thúc trò chơi
            uiManager.GameOver();
            return;     //Bỏ qua các chức năng còn lại bên dưới
        }

        playerHealth.Respawn();     //Người chơi di chuyển đến điểm lưu
        transform.position = currentCheckpoint.position;    //Khôi phục lại máu và các trạng thái animation lại ban đầu
        SoundManager.instance.PlaySound(amthanhhoisinh);

        //Di chuyển camera lại điểm lưu trữ khi nhân vật hồi sinh (để hoạt động được, đối tượng điểm kiểm tra phải được đặt làm con của đối tượng phòng)
        //  Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;    //Lưu lại thông tin điểm hiện tại
            SoundManager.instance.PlaySound(amthanhluu);

            //Khi đã được kích hoạt va chạm lần đầu thì sau đó sẽ không cho collider hoạt động để nhận va chạm nữa
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("activate");
        }
    }
}