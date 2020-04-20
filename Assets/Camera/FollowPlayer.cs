using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Vector3 ShakeOffset;
    public GameObject PlayerObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ( PlayerObject)
        {
            Vector3 pp = PlayerObject.transform.position;
            transform.position = new Vector3(pp.x, pp.y, transform.position.z) + ShakeOffset;
        }

    }

    public void Shake()
    {
        StartCoroutine("DoShake");
    }

    IEnumerator DoShake()
    {
        for(int i = 0; i < 5; i++)
        {
            ShakeOffset = Random.insideUnitSphere*0.4f;
            ShakeOffset.z = 0;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
