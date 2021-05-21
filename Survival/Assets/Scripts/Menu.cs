using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{

    //  public int speed;
    public AudioClip SelectClip;





    public void LoadGame()
    {
        //SceneManager.LoadScene(2);
        StartCoroutine(Wait());
      //  yield return new WaitForSeconds(5);

        SoundManager.instance.PlaySoundFX(SelectClip);


    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }


        public void QuitGame()
    {
        Application.Quit();
        SoundManager.instance.PlaySoundFX(SelectClip);
    }


    private void Update()
    {
   //     transform.Rotate(0, speed, 0);
    }



}
