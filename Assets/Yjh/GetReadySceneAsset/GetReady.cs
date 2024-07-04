using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetReady : MonoBehaviour
{
    [SerializeField]
    private Image UI;

    private void Awake()
    {
        UI.gameObject.SetActive(false);
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("mainScene");
    }

    public void SetUI()
    {
        UI.gameObject.SetActive(true);
    }

    public void ExitUI()
    {
        UI.gameObject.SetActive(false);
    }

}
