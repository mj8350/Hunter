using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("mainScene");
    }

    public void GameExit() // ∞‘¿” ≤Ù±‚
    {
        Application.Quit();
    }
}
