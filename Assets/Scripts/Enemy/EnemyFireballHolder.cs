using UnityEngine;

public class EnemyFireballHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void Update()
    {
        //Hướng đạn lửa bắn ra đúng theo hướng mặt của lính
        transform.localScale = enemy.localScale;
    }
}