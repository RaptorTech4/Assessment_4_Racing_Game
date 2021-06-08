using UnityEngine;

public class Player_Input : MonoBehaviour
{

    public string _ForwardAxes = "Vertical";
    public string _TurnAxes = "Horizontal";
    public string _BrakeAxes = "Brake";

    Kart_Controller kart;

    private void Start()
    {
        kart = GetComponent<Kart_Controller>();
    }

    private void FixedUpdate()
    {
        kart._forward = Input.GetAxis(_ForwardAxes);
        kart._turn = Input.GetAxis(_TurnAxes);
        kart._brake = Input.GetAxis(_BrakeAxes);


    }
}
