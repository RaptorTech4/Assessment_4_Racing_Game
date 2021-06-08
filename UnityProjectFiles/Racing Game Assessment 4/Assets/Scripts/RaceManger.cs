using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManger : MonoBehaviour
{

    public Text _LapText;
    public Text _placeText;
    public int _MaxLaps;

    int _MaxRacers;

    List<Kart_Controller> karts = new List<Kart_Controller>();
    Kart_Controller playerKart;

    void Start()
    {
        GameObject[] racingKarts = GameObject.FindGameObjectsWithTag("Kart");

        for (int i = 0; i < racingKarts.Length; i++)
        {
            if(racingKarts[i].GetComponent<Kart_Controller>())
            {
                karts.Add(racingKarts[i].GetComponent<Kart_Controller>());
            }
        }

        for (int i = 0; i < karts.Count; i++)
        {
            if(karts[i].gameObject.GetComponent<Player_Input>())
            {
                playerKart = karts[i];
            }
        }

        _MaxRacers = karts.Count;
    }

    void Update()
    {
        for (int i = 0; i < karts.Count; i++)
        {
            for (int j = 0; j < i+1; j++)
            {
                if(karts[j]._CurrentLap >= karts[i]._CurrentLap)
                {
                    if(karts[j]._CurrentChekpoint>= karts[i]._CurrentChekpoint)
                    {
                        Kart_Controller tmp = karts[i];
                        karts[i] = karts[j];
                        karts[j] = tmp;
                    }
                }
            }

            karts[i]._CurrntPlace = i + 1;
        }

        _placeText.text = "" + playerKart._CurrntPlace + "/" + _MaxRacers;
        _LapText.text = "" + playerKart._CurrentLap + "/" + _MaxLaps;
    }
}
