using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        GameSession gs = FindObjectOfType<GameSession>();
        if (gs) { gs.ResetGame(); }
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadGameOverCoroutine());
    }

    IEnumerator LoadGameOverCoroutine()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("GameOverScene");
    }

    public void LoadWin()
    {
        StartCoroutine(LoadWinCoroutine());
    }

    IEnumerator LoadWinCoroutine()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("WinScene");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
