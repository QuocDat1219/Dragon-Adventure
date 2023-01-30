using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuScreen;

    //Các hàm tùy chọn game over
    private void Start() {
        menuScreen.SetActive(true);
    }
    public void Options(){
        SceneManager.LoadScene(7);
    }

    public void Quit(){
        Application.Quit(); //Cái này xài được khi nào game đã được build ra
        // UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Batdau(){
        SceneManager.LoadScene(1);
    }
}