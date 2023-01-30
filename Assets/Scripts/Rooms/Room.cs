using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;  //Lấy vào một mảng các game object của các loại bẫy và kẻ địch ở room
    private Vector3[] initialPosition;

    private void Awake()
    {
        // Lưu vị trí ban đầu của các đối tượng bẫy và lính
        initialPosition = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
                initialPosition[i] = enemies[i].transform.position;
        }
        
        // Hủy kích hoạt phòng
        if (transform.GetSiblingIndex() != 0)
            ActivateRoom(false);
    }
    public void ActivateRoom(bool _status)
    {    
        // Kích hoạt / hủy kích hoạt bẫy và kẻ thù
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }
        }
    }
}