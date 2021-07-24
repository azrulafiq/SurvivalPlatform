using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    private bool m_IsGameActive = false;
    private SpawnManager m_SpawnManager;
    void Start()
    {
        m_SpawnManager = FindObjectOfType<SpawnManager>();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        m_IsGameActive = true;
        m_SpawnManager.StartSpawning();
        startScreen.SetActive(false);
    }

}
