using UnityEngine;

public class Lap_CheckPont : MonoBehaviour
{

    public int _Index;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Kart_Controller>())
        {
            Kart_Controller kart = other.GetComponent<Kart_Controller>();

            if (kart._CurrentChekpoint == _Index + 1 || kart._CurrentChekpoint == _Index - 1)
            {
                kart._CurrentChekpoint = _Index;
            }

        }
    }
}
