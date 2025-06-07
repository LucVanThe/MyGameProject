using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void chonman()
    {
        SceneManager.LoadScene("Manchoi");
    }
    public Button[] levelButtons; // Kéo các nút vào danh sách này trong Inspector

    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1; // Bắt đầu từ Level 1
            levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
        }
    }

    void LoadLevel(int levelIndex)
    {
        Debug.Log("Loading Level: " + levelIndex);
        SceneManager.LoadScene(levelIndex);
    }
}
