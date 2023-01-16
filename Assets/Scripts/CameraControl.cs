using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [Range(0,1)] [SerializeField] private float _lerptime = 0.06f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        velocity = Vector3.zero;
        Vector3 targetoffset= new Vector3 (_target.position.x, _target.position.y, -10);

        Vector3 newposition = Vector3.SmoothDamp(transform.position, targetoffset,ref velocity, _lerptime);
        if (newposition.y >= 0) transform.position = new Vector3(newposition.x, newposition.y, -10);
        else if (newposition.y < 0) transform.position = new Vector3(newposition.x, 0, -10);

    }
    private Vector3 velocity;
}
