using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainButtonClick : MonoBehaviour
{
    public GameObject titulo;
    public bool boole = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void clickButton(){
        boole = !boole;
        titulo.SetActive(boole);

    }
}
