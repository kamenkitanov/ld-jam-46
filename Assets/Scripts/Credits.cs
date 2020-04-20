using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public AnimationCurve Curve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = GetComponent<RectTransform>().position;
        pos.x = Curve.Evaluate(Time.timeSinceLevelLoad)*Screen.width;
        GetComponent<RectTransform>().position = pos;
    }
}
