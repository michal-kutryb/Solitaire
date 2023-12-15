using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool[] finishPilesCompleted = { false, false, false, false };
    public bool isGameOver = false;

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
            isGameOver = true;
            Debug.Log("YOU WON <3");
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



}
