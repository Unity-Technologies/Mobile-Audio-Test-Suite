using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject canvas;

    public void BeginTests()
    {
        canvas.SetActive(true);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

}
