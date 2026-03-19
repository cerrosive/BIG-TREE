using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour
{
    public GameObject EndScreen;
    public static bool GameisPaused = false;
    void Start()
    {
        EndScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EndScreen.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        SceneManager.LoadScene("MainMenu");
    }

}
