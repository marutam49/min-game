using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelectController : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Samplescene");
    }
}