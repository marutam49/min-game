using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseButtonManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    public void PauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
