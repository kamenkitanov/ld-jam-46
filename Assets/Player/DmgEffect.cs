using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class DmgEffect : MonoBehaviour
{
    float startedTime;
    Vector2 Vel;
    // Start is called before the first frame update
    void Start()
    {
        startedTime = Time.time;
        Vel = Random.insideUnitCircle*4;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsed = Time.time - startedTime;
        Color c = GetComponent<SpriteShapeRenderer>().color;
        c.a = 1.0f - elapsed / 2;
        GetComponent<SpriteShapeRenderer>().color = c;

        transform.position += new Vector3(Vel.x, Vel.y, 0) * Time.deltaTime;

        if ( c.a <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
