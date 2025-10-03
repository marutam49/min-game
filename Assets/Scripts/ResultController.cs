using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    public void OnClickResetButton()
    {
        SceneManager.LoadScene("Title");
    }
}