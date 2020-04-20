using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string ExitToLevel;

    public static string LastLevelName = "Level0";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.tag == "Player")
        {
            if ( ExitToLevel != "")
            {
                LastLevelName = ExitToLevel;
                SceneManager.LoadScene(ExitToLevel);
            }
        }

    }
}
