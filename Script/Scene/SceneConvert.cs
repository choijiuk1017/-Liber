using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneConvert : MonoBehaviour
{

    public void SeceneChange()
    {
        SceneManager.LoadScene("Mainscene");
        Time.timeScale = 1;
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
