using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinChecker : MonoBehaviour
{
    public static WinChecker Instance { get; private set; }

    [SerializeField] private List<GameObject> _playPiles = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public bool CheckIfWon() 
    {
        foreach(GameObject pile in _playPiles) 
        {
            if(pile.transform.childCount <= 0) { continue; }
            if (pile.transform.GetChild(0).Find("Reverse").gameObject.activeSelf) 
            {
                return false;
            }
            continue;
        }

        return true;
    }
}
