using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    public bool QuitOnEscape = false; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (QuitOnEscape)
            {
                DoQuit();
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }

        }
    }

    public void DoQuit()
    {
        Application.Quit();
    }
}
