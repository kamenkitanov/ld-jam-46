using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot1 : MonoBehaviour
{
    public AnimationCurve RotationCurve;
    public AnimationCurve BeamWidthCurve;

    public GameObject ExclamationObject;
    public GameObject SightPoly;
    enum State
    {
        Idle,
        Spotted,
        Fire
    }

    State CurrentState = State.Idle;

    public LineRenderer lineRenderer;
    GameObject Player;

    float StartSpottedTime = 0;
    float StartFiringTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, RotationCurve.Evaluate(Time.time));

        switch(CurrentState)
        {
            case State.Idle:
                if (SightPoly.GetComponent<Collider2D>().IsTouching(Player.GetComponent<Collider2D>())){
                    CurrentState = State.Spotted;
                    StartSpottedTime = Time.time;
                }
                break;
            case State.Spotted:
                ExclamationObject.SetActive(true);
                if ( Time.time - StartSpottedTime > 1.5f)
                {
                    CurrentState = State.Fire;
                    StartFiringTime = Time.time;
                    GetComponent<AudioSource>().Play();
                }
                break;
            case State.Fire:
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformVector(Vector3.up));
                if (hit.collider)
                {
                    Vector3[] pos = { lineRenderer.transform.position, hit.point };
                    lineRenderer.enabled = true;
                    lineRenderer.SetPositions(pos);
                    float width = BeamWidthCurve.Evaluate(Time.time - StartFiringTime);
                    lineRenderer.startWidth = 0.1f;
                    lineRenderer.endWidth = width;

                    if ( hit.collider.gameObject == Player)
                    {
                        Player.GetComponent<PlayerController>().Health--;
                    }
                }

                if (Time.time - StartFiringTime > 3.0f)
                {
                    lineRenderer.enabled = false;
                    CurrentState = State.Idle;
                    ExclamationObject.SetActive(false);
                }
                break;
        }
    }

    bool HasLineOfSightToPlayer()
    {
        Vector3 TowardsPlayer = Player.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, TowardsPlayer, 30);
        if (hit.collider?.gameObject == Player)
        {
            return true;
        }
        return false;
    }
}
