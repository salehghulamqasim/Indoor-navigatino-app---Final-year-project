using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NavigationManager : MonoBehaviour
{
    // A stack to keep track of the scene indices
    private Stack<int> sceneHistory = new Stack<int>();

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Ensures that the NavigationManager persists across scene loads
    DontDestroyOnLoad(this.gameObject);
    }

    // Method to call when a new scene is loaded
    public void LoadScene(int sceneIndex)
    {
        // Push the current scene onto the stack
        sceneHistory.Push(SceneManager.GetActiveScene().buildIndex);
        
        // Load the new scene
        SceneManager.LoadScene(sceneIndex);
    }

    // Call this function to go back to the previous scene
    public void GoBack()
    {
        // If there's a previous scene stored in the history
        if (sceneHistory.Count > 0)
        {
            // Pop the last scene index off the stack and load it
            int lastSceneIndex = sceneHistory.Pop();
            SceneManager.LoadScene(lastSceneIndex);
        }
        else
        {
            Debug.Log("No previous scene to load or this is the first scene.");
        }
    }

    // Optional: Call this method to clear the history, for example, when returning to the Main Menu
    public void ClearHistory()
    {
        sceneHistory.Clear();
    }

    // Additional methods for scene navigation can be added here
}
