using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManger : MonoBehaviour
{

    public static RaceManger singleton;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else
        {
            Debug.LogError("you have 2 RaceManeger in one sean");
            return;
        }
    }

    public Text _LapText;
    public Text _placeText;
    public int _MaxLaps;

    int _MaxRacers;

    List<Kart_Controller> karts = new List<Kart_Controller>();
    Kart_Controller playerKart;

    public bool _RemovingKarts = false;

    void Start()
    {
        GameObject[] racingKarts = GameObject.FindGameObjectsWithTag("Kart");

        for (int i = 0; i < racingKarts.Length; i++)
        {
            if (racingKarts[i].GetComponent<Kart_Controller>())
            {
                karts.Add(racingKarts[i].GetComponent<Kart_Controller>());
            }
        }

        for (int i = 0; i < karts.Count; i++)
        {
            if (karts[i].gameObject.GetComponent<Player_Input>())
            {
                playerKart = karts[i];
            }
        }

        _MaxRacers = karts.Count;
    }

    void Update()
    {
        if(!_RemovingKarts)
        {
            UpdateRacers();
        }
    }

    void UpdateRacers()
    {
        for (int i = 0; i < karts.Count; i++)
        {
            for (int j = i + 1; j < karts.Count; j++)
            {
                if (karts[j]._CurrentLap >= karts[i]._CurrentLap)
                {
                    if (karts[j]._CurrentChekpoint >= karts[i]._CurrentChekpoint)
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

    public void RemoveKart()
    {
        int allKarts = karts.Count - 1;

        if(allKarts == 1)
        {

        }
        else
        {
            GameObject deathKart = karts[allKarts].gameObject;

            if(deathKart.GetComponent<Player_Input>())
            {
                Player_Input pi = deathKart.GetComponent<Player_Input>();
                pi.StopMoving = true;

                karts.Remove(karts[allKarts]);
            }
            else
            {
                karts.Remove(karts[allKarts]);
                _MaxRacers = _MaxRacers - 1;
                Destroy(deathKart);
            }

            _RemovingKarts = false;
        }
    }
}
