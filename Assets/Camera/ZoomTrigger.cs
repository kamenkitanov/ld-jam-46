using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTrigger : MonoBehaviour
{
    public float ZoomAmount;

    bool DoZoom = false;
    float vel = 0;

    public float Time = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( DoZoom )
        {
            Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, ZoomAmount, ref vel, Time);

            if ( Mathf.Approximately(Camera.main.orthographicSize, ZoomAmount))
            {
                DoZoom = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.tag == "Player")
        {
            DoZoom = true;
        }

    }
}
