using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1.0f;

    int _health = 5;

    public GameObject DmgEffect;

    public Vector2 ExternalVeloctiy;
    public float ExternalForceDamp = 1;

    public AnimationCurve BreathCurve;

    float LastTakenDmgTime = 0;
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        Vector2 vel = new Vector2(horizontalAxis, verticalAxis).normalized;

        GetComponent<Rigidbody2D>().velocity = (vel * Speed) + ExternalVeloctiy;
        ExternalVeloctiy *= ExternalForceDamp;

        transform.localScale = Vector3.one * BreathCurve.Evaluate(Time.time % 1) * Mathf.Min(1, (12 + (float)Health) / 17.0f);
    }

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            if (value >= 0)
            {

                if ( value < _health)
                {
                    if ( Time.time - LastTakenDmgTime > 0.5f)
                    {
                        LastTakenDmgTime = Time.time;

                        // We are taking dmg
                        for (int i = 0; i < 5; i++)
                        {
                            Vector3 rand = Random.insideUnitCircle;
                            GameObject.Instantiate(DmgEffect, transform.position, Quaternion.identity);
                        }

                        _health = value;
                    }
                }
                else
                {
                    _health = value;
                }
            }
            else
            {
                SceneManager.LoadScene("End");
            }

        }
    }
}
