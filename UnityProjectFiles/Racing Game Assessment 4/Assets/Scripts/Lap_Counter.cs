using UnityEngine;

public class Lap_Counter : MonoBehaviour
{

    public int _TotalCheckPoints;

    int LastHighestLap = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Kart_Controller>())
        {
            Kart_Controller kart = other.GetComponent<Kart_Controller>();

            if (kart._CurrentChekpoint == _TotalCheckPoints)
            {
                kart._CurrentChekpoint = 0;
                kart._CurrentLap++;
                if (LastHighestLap < kart._CurrentLap)
                {
                    LastHighestLap++;
                    RaceManger.singleton._RemovingKarts = true;
                    RaceManger.singleton.RemoveKart();
                }
            }

        }
    }

}
