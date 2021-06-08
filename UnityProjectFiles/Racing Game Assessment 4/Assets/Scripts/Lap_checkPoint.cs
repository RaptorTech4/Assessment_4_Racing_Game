using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lap_checkPoint : MonoBehaviour
{

    public int _TotalCheckPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Kart_Controller>())
        {
            Kart_Controller kart = other.GetComponent<Kart_Controller>();

            if(kart._CurrentChekpoint == _TotalCheckPoints)
            {
                kart._CurrentChekpoint = 0;
                kart._CurrentLap++;
            }

        }
    }

}
