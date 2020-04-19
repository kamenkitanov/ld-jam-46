using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public GameObject Path;
    int CurrentPathNode = 0;
    public float Speed = 1.0f;
    public State CurrentState = State.Idle;

    public float SightRadius = 1.0f;

    public GameObject Player;

    public GameObject ExclamationObject;
    public GameObject SightPoly;

    float StartSpottedTime = 0;

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
    void Update()
    {
        if ( HasLineOfSightToPlayer())
        {
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
                transform.position = Vector3.MoveTowards(transform.position, GetTargetPoint(), Speed * Time.deltaTime);

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
                if (Time.time - StartSpottedTime > 3)
                {
                    CurrentState = State.Chase;
                }
                break;
            // Try to get close to the player and kill him
            case State.Chase:
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 6 * Speed * Time.deltaTime);
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
