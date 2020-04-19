using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public GameObject Path;
    int CurrentPathNode = 0;
    public float Speed = 1.0f;
    public State CurrentState = State.Idle;

    public float SightRadius = 2.0f;

    public GameObject Player;

    public GameObject ExclamationObject;
    public GameObject SightPoly;

    float StartSpottedTime = 0;
    float LastSeenPlayerTime = 0;
    float StartAlertedTime = 0;

    Vector3 LastKnownPlayerPosition;

    public enum State
    {
        Idle,
        Spotted,
        Chase,
        Alerted
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( HasLineOfSightToPlayer())
        {
            LastSeenPlayerTime = Time.time;
            LastKnownPlayerPosition = Player.transform.position;
            SightPoly.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        } 
        else
        {
            SightPoly.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.1f);
        }

        switch (CurrentState)
        {
            // Go between points, looking for player
            case State.Idle:
                transform.position = Vector3.MoveTowards(transform.position, GetTargetPoint(), Speed * Time.fixedDeltaTime);

                if (Vector3.Distance(transform.position, GetTargetPoint()) < Vector3.kEpsilon)
                {
                    CurrentPathNode++;
                    if (CurrentPathNode >= Path.transform.childCount)
                    {
                        CurrentPathNode = 0;
                    }
                }

                if ( Vector3.Distance(Player.transform.position, transform.position) < SightRadius && HasLineOfSightToPlayer())
                {
                    CurrentState = State.Spotted;
                    StartSpottedTime = Time.time;
                }
                break;
            // Player is in range, prepare to chase
            case State.Spotted:
                ExclamationObject.SetActive(true);
                if (Time.time - StartSpottedTime > 1.5f)
                {
                    CurrentState = State.Chase;
                }
                break;
            // Try to get close to the player and kill him
            case State.Chase:
                transform.position = Vector3.MoveTowards(transform.position, LastKnownPlayerPosition, 10 * Speed * Time.fixedDeltaTime);

                if (Vector3.Distance(transform.position, LastKnownPlayerPosition) < Vector3.kEpsilon)
                {
                    if (Time.time - LastSeenPlayerTime > 3)
                    {
                        ExclamationObject.SetActive(false);
                        CurrentState = State.Idle;
                    }
                }

                if (GetComponent<Collider2D>().IsTouching(Player.GetComponent<Collider2D>())){
                    Vector2 dir = Player.transform.position - transform.position;
                    //Player.GetComponent<Rigidbody2D>().AddForce(dir.normalized * 10);
                    //Player.GetComponent<PlayerController>().ExternalVeloctiy = dir.normalized * 10;
                    Player.GetComponent<PlayerController>().Health--;
                    CurrentState = State.Alerted;
                    StartAlertedTime = Time.time;
                }

                break;
            case State.Alerted:
                if ( Time.time - StartAlertedTime > 0.3f)
                {
                    CurrentState = State.Chase;
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

    Vector3 GetTargetPoint()
    {
        return Path.transform.GetChild(CurrentPathNode).position;
    }
}
