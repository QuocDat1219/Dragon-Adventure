using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class video : MonoBehaviour
{
    public void quaman(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
        
}
