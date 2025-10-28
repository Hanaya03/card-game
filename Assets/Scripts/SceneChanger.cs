using UnityEngine;
using UnityEngine.SceneManagement;

/*
/Simple script for changing scenes.
*/

public class SceneChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("CardTest", LoadSceneMode.Single);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScene", LoadSceneMode.Single);
    }
}
