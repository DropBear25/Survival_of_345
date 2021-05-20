using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{

    public int speed;
  
public void LoadGame()
    {
        SceneManager.LoadScene(0);
    }



    public void QuitGame()
    {
        Application.Quit();
    }


    private void Update()
    {
        transform.Rotate(0, speed, 0);
    }



}
