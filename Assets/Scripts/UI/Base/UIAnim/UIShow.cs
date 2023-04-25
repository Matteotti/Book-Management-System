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
        transform.position = startPos;
        transform.localScale = Vector3.zero;
        speed = 1f / time;
        isShow = true;
    }
    public void Disappear()
    {
        transform.position = endPos;
        isShow = false;
    }
    void Start()
    {
        startPos = transform.position;
        endPos = transform.position;
        speed = 1f / time;
    }
    void Update()
    {
        if (isShow)
        {
            if(transform.localScale.x < (Vector3.one - Vector3.Lerp(Vector3.zero, new Vector3(1, 1, 1), speed * Time.deltaTime)).x)
            {
                transform.position += Vector3.Lerp(Vector3.zero, endPos - startPos, speed * Time.deltaTime);
                transform.localScale += Vector3.Lerp(Vector3.zero, new Vector3(1, 1, 1), speed * Time.deltaTime);
            }
            else
            {
                transform.position = endPos;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            if (transform.localScale.x > Vector3.Lerp(Vector3.zero, new Vector3(1, 1, 1), speed * Time.deltaTime).x)
            {
                transform.position += Vector3.Lerp(Vector3.zero, startPos - endPos, speed * Time.deltaTime);
                transform.localScale += Vector3.Lerp(Vector3.zero, new Vector3(-1, -1, -1), speed * Time.deltaTime);
            }
            else
            {
                transform.position = startPos;
                transform.localScale = Vector3.zero;
            }
        }
    }
}
