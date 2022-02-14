using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCurve : MonoBehaviour
{
    public AnimationCurve curve;

    //tower starts here currently, this value is subject to change
    private float currentTowerPos = 2.5f;
    private float ascendTower = 1;
    private float descendTower = 1;
    void Start()
    {
        curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        curve.preWrapMode = WrapMode.PingPong;
        curve.postWrapMode = WrapMode.PingPong;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, curve.Evaluate(Time.time)/* + currentTowerPos*/, transform.position.z);
    }
    /*
    public void AscendTower()
    {
        Debug.LogError("up");
        currentTowerPos += ascendTower;
    }
    public void DescendTower()
    {
        Debug.LogError("Down");
        currentTowerPos -= descendTower;
    }
    */
}
