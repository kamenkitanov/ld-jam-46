using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToSceneScript : MonoBehaviour
{
    public void OnMouseClick(string sceneName) { SceneManager.LoadScene(sceneName); }
    public void RestartLastLevel() { SceneManager.LoadScene(Exit.LastLevelName); }
}

