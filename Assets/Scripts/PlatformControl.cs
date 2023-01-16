using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{
    [SerializeField] Transform _startpoint;
    [SerializeField] Transform _endpoint;
    [SerializeField] float _timeToReach;
    private float _timerMovement;
    private bool isForward = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = _startpoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isForward)
        {
            _timerMovement += Time.deltaTime;
            if (_timerMovement >= _timeToReach) isForward= false;
        }
        else if (!isForward)
        {
            _timerMovement -= Time.deltaTime;
            if (_timerMovement <= 0) isForward = true;
        }

        Vector3 newposition = Vector3.Lerp(_startpoint.position, _endpoint.position, _timerMovement / _timeToReach);
        transform.position = newposition;
    }
}
