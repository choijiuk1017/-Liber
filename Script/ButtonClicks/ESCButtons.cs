using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCButtons : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Panel2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
        Panel.SetActive(false);
    }

    public void Option()
    {
        Panel2.SetActive(true);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Start Scene");
    }

    public void Cancel()
    {
        Panel.SetActive(false);
        Time.timeScale = 1;
    }
}
