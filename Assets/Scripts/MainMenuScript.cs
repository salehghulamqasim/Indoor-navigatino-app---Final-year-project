using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Call this function to load the main gameplay scene
    public void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Call this function to load the settings scene
    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }

    // Call this function to load the about us scene
    public void LoadAboutUsScene()
    {
        SceneManager.LoadScene("AboutUs");
    }
}
