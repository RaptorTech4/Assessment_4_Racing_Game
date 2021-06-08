using UnityEngine;

public class Player_Input : MonoBehaviour
{

    public string _ForwardAxes = "Vertical";
    public string _TurnAxes = "Horizontal";
    public string _BrakeAxes = "Brake";

    Kart_Controller kart;

    [HideInInspector]
    public bool StopMoving = false;

    private void Start()
    {
        kart = GetComponent<Kart_Controller>();
    }

    private void FixedUpdate()
    {

        kart._forward = (StopMoving) ? 0.0f : Input.GetAxis(_ForwardAxes);
        kart._turn = (StopMoving) ? 0.0f : Input.GetAxis(_TurnAxes);
        kart._brake = (StopMoving) ? 1.0f : Input.GetAxis(_BrakeAxes);

    }
}
