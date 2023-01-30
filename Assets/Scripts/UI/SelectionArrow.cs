using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] buttons;   //Vị trí của các UI (reset, menu, quit)
    [SerializeField] private AudioClip thaydoi;     //Âm thanh khi thay đổi lựa chọn
    [SerializeField] private AudioClip chon;   //Âm thanh khi chọn
    private RectTransform arrow;    //recttranform giữ thông tin vị trí, kích thước, neo và trục của 1 hình chữ nhật, sử dụng cho UI
    private int currentPosition;

    private void Awake()
    {
        arrow = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        currentPosition = 0;
        ChangePosition(0);
    }
    private void Update()
    {
        //thay đổi lựa chọn các mục sau khi chết
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ChangePosition(-1);
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ChangePosition(1);
        //Tương tác với các tùy chọn menu
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Submit"))
            Interact();
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change != 0)
            SoundManager.instance.PlaySound(thaydoi);

        if (currentPosition < 0)
            currentPosition = buttons.Length - 1;
        else if (currentPosition > buttons.Length - 1)
            currentPosition = 0;

        AssignPosition();
    }

    //Thay đổi trục y (lên hoặc xuống) của cục lửa theo lựa chọn hành động chọn trong mảng options
    private void AssignPosition()
    {
        arrow.position = new Vector3(arrow.position.x, buttons[currentPosition].position.y);
    }
    private void Interact()
    {
        //Truy cập thành phần nút trên từng tùy chọn và gọi chức năng của nó
        SoundManager.instance.PlaySound(chon);
        buttons[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}