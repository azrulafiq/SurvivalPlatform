using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    public UnityEvent OnGameStart;
    //private bool m_IsGameActive = false;
    private SpawnManager m_SpawnManager;
    void Start()
    {
        m_SpawnManager = FindObjectOfType<SpawnManager>();
        var elevators = FindObjectsOfType<Elavator>();

        for (int i = 0; i < elevators.Length; i++)
        {
            OnGameStart.AddListener(elevators[i].OnGameStart);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        OnGameStart.Invoke();
        m_SpawnManager.StartSpawning();
        startScreen.SetActive(false);
    }

}
