using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChooser : MonoBehaviour
{
    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            SceneManager.LoadScene(2);
        }
        else 
        {
            SceneManager.LoadScene(1);
        }
    }
}
