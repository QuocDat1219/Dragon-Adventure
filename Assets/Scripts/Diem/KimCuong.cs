using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KimCuong : MonoBehaviour
{
    private int cherry = 0;
    [SerializeField] private Text TraicayText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Core"))
        {
            Destroy(collision.gameObject);
            cherry++;
            TraicayText.text = "Core: " + cherry; 
        }
    }
}
