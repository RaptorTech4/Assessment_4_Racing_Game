using UnityEngine;

public class CheckPoints : MonoBehaviour
{

    [SerializeField] bool _FirstCheckPoint;
    [SerializeField] GameObject _NextCheckpoint;

    [SerializeField] int _Lapes;
    [SerializeField] int _LapesDone;



    private void Start()
    {
        if (_FirstCheckPoint)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        _LapesDone = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_FirstCheckPoint)
            {
                if (_LapesDone >= _Lapes)
                {
                    Debug.Log("You Win");
                }
                else
                {
                    
                    _LapesDone++;
                    _NextCheckpoint.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
            else
            {

                _NextCheckpoint.SetActive(true);
                gameObject.SetActive(false);
            }

        }
    }
}
