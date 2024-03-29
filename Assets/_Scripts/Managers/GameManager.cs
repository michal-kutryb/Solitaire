using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject _gameOverPanel;
    public bool[] finishPilesCompleted = { false, false, false, false };
    public bool isGameOver = false;
    public bool isDraggingACard = false;
    public bool isDrawing = false;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (CheckIfGameWon())
        {
            GameOver();
        }
    }

    private bool CheckIfGameWon()
    {
        for (int i = 0; i < 4; i++)
        {
            if (finishPilesCompleted[i] != true)
                return false;
        }

        return true;
    }

    public void GameOver() 
    {
        isGameOver = true;
        _gameOverPanel.SetActive(true);
    }
}
