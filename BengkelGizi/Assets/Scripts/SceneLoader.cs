using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


    public void Replay()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();

        Debug.Log("Quit Aplication");
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }
}
