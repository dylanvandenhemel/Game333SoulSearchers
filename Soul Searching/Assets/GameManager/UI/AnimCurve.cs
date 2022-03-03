using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCurve : MonoBehaviour
{
    public AnimationCurve curve;
    private Vector3 startPos;
    private Vector3 targetPos;
    public float speed;
    private float rate;
    [HideInInspector]public bool bActive;

    //[HideInInspector] public bool bIsMoving;
    /*
    private void Start()
    {
        startPos = transform.position;
        //so the tower does not move anywhere until imput is added
        targetPos = startPos;
    }

    private void Update()
    {
        if(transform.position != targetPos)
        {
            bIsMoving = true;
        }
        else
        {
            bIsMoving = false;
        }
        rate = Mathf.Clamp(curve.Evaluate(Time.deltaTime * speed), 0, 1);
        transform.position = Vector3.Lerp(transform.position, targetPos, rate);
    }
    */
    IEnumerator TowerMove()
    {
        rate = 0;
        while (rate < 1)
        {
            rate += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPos, targetPos, curve.Evaluate(rate));
            yield return new WaitForEndOfFrame();
        }
        bActive = false;
    }

    public void MoveTower(float direction)
    {
        if (bActive) return;
        bActive = true;
        targetPos = new Vector3(transform.position.x, transform.position.y + direction, transform.position.z);
        startPos = transform.position;
        StartCoroutine(nameof(TowerMove));
    }
}
