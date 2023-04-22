using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShow : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float time = 1f;
    private float speed = 1f;
    public bool isShow = false;
    public void Appear(Vector3 targetStartPos, Vector3 targetEndPos)
    {
        startPos = targetStartPos;
        endPos = targetEndPos;
        transform.localPosition = startPos;
        transform.localScale = Vector3.zero;
        speed = 1f / time;
        isShow = true;
    }
    public void Disappear()
    {
        isShow = false;
    }
    void Start()
    {
        startPos = transform.localPosition;
        endPos = transform.localPosition;
        speed = 1f / time;
    }
    void Update()
    {
        if (isShow)
        {
            if(transform.localPosition.x < endPos.x)
            {
                transform.localPosition += Vector3.Lerp(Vector3.zero, endPos - startPos, speed * Time.deltaTime);
                transform.localScale += Vector3.Lerp(Vector3.zero, new Vector3(1, 1, 1), speed * Time.deltaTime);
            }
            else
            {
                transform.localPosition = endPos;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            if (transform.localPosition.x > startPos.x)
            {
                transform.localPosition += Vector3.Lerp(Vector3.zero, startPos - endPos, speed * Time.deltaTime);
                transform.localScale += Vector3.Lerp(Vector3.zero, new Vector3(-1, -1, -1), speed * Time.deltaTime);
            }
            else
            {
                transform.localPosition = startPos;
                transform.localScale = Vector3.zero;
            }
        }
    }
}
