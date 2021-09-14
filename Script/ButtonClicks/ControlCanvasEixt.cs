using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCanvasEixt : MonoBehaviour
{

    public GameObject panel;
    public GameObject panel1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Exit()
    {
        Time.timeScale = 1;
        panel.SetActive(true);
        panel1.SetActive(false);
    }
}
