using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClick : MonoBehaviour
{
    public GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopupMap()
    {
        Time.timeScale = 0;
        Panel.SetActive(true);
    }
}
