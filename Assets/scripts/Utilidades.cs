using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.NetworkInformation;
public class Utilidades : MonoBehaviour
{
    public Slider slider;
    public Button button;
    public int clicks, multiplier = 1, premios, premiosLimite = 10;
    public float cooldwonSecs, priceChance, bonus, sliderValue;
    public bool cooldown = false;
    public string info;

    // Minijuego clicker para dar limite de 10 premios con una posibilidad del 5 porciento al hacer reinicios de 4 sets
    //el contador se reinicia en dificultad y aumenta en 1 la potencia de la posibilidad de obtener premio
    void Start()
    {
        
        slider.value = 0f;
        button.onClick.AddListener(clicker);
        // dificultad- reduce la barra y el numero de clicks del usuario cada 0.5 segs
        InvokeRepeating("Timer", 0, 0.5f);
        // evita que el usuario reciba mas de un premio cada 15 segundos
        InvokeRepeating("cooldownFnc", 0, 1);
        // presenta la direccion MAC de la interfaz WIFI del dispositivo
        this.ShowNetworkInterfaces();
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    void FixedUpdate()
    {


        if (1 <= clicks && clicks <= 350)
        {
            if (clicks <= 20)
            {
                Debug.Log("x1");
                sliderValue = 0.008f;
                bonus = 0.004f;
                this.Price((10f) * multiplier, "premio muy bajo");


            }
            else if (clicks <= 30)
            {
                Debug.Log("x2");
                sliderValue = 0.004f;
                bonus = 0.006f;
                this.Price((5f) * multiplier, "premio bajo");

            }
            else if (clicks <= 50)
            {
                Debug.Log("x3");
                sliderValue = 0.002f;
                bonus = 0.008f;
                this.Price((4f) * multiplier, "premio medio");

            }
            else if (clicks <= 100)
            {
                Debug.Log("x4");
                sliderValue = 0.001f;
                bonus = 0.009f;
                this.Price((1f) * multiplier, "premio alto");

            }
            else
            {
                Debug.Log("RESET!");
                clicks = 0;
                if (multiplier < 10)
                {
                    multiplier = multiplier + 1;
                }

            }
        }


    }
    void clicker()
    {
        clicks++;
        slider.value += sliderValue;

    }
    void Timer()
    {

        if (clicks > 0) clicks = clicks - 1;
        slider.value = slider.value - bonus;

    }
    void Price(float chance, string mensaje)
    {

        if (chance > Random.Range(1f, 100f) && (cooldown == false))
        {
            cooldown = true;
            if (premios <= premiosLimite) 
            {
                cooldwonSecs = cooldwonSecs + 10;
                Debug.Log(mensaje);
                premios = premios + 1;
            }

        }
    }
    void cooldownFnc()
    {
        cooldwonSecs = cooldwonSecs - 1;
        if (cooldwonSecs <= 0)
        {
            cooldwonSecs = 0;
            cooldown = false;
            if (clicks == 0)
            {
                multiplier = 1;
            }
        }
    }

     public void ShowNetworkInterfaces()
    {
        IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

        foreach (NetworkInterface adapter in nics)
        {
            PhysicalAddress address = adapter.GetPhysicalAddress();
            byte[] bytes = address.GetAddressBytes();
            string mac = null;
            for (int i = 0; i < bytes.Length; i++)
            {
                mac = string.Concat(mac + (string.Format("{0}", bytes[i].ToString("X2"))));
                if (i != bytes.Length - 1)
                {
                    mac = string.Concat(mac + "-");
                }
            }
            info += mac + "\n";

            info += "\n";
        }
        Debug.Log(info);
    }
}