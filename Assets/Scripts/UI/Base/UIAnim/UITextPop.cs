using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextPop : MonoBehaviour
{
    private bool isPop = false;
    private Vector3 startPos;
    public float acceralationTime;
    public float stopTime;
    public float popDistance;
    public float initSpeed;
    private float acceralation;
    private float maxSpeed;
    private float counter = 0;
    private float speed;
    private float imageTransparentSpeed;
    private Vector3 direction = Vector3.up;
    private TMP_Text text;
    public void Pop(Vector3 targetPos)
    {
        isPop = true;
        counter = 0;
        maxSpeed = 2 * popDistance / acceralationTime - initSpeed;
        acceralation = (maxSpeed - initSpeed) / acceralationTime;
        text.color = new Color(0, 0, 0, 0);
        speed = maxSpeed;
        transform.localPosition = targetPos;
        imageTransparentSpeed = 1 / acceralationTime;
    }
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        imageTransparentSpeed = 1 / acceralationTime;
    }
    void Update()
    {
        if (isPop)
        {
            counter += Time.deltaTime;
            if (counter < acceralationTime)
            {
                speed -= acceralation * Time.deltaTime;
                text.color += new Color(0, 0, 0, imageTransparentSpeed * Time.deltaTime);
            }
            else if(counter < acceralationTime + stopTime)
            {
                speed = 0;
                text.color = new Color(0, 0, 0, 1);
            }
            else if(counter < acceralationTime * 2 + stopTime)
            {
                speed = maxSpeed - acceralation * (counter - acceralationTime - stopTime);
                text.color -= new Color(0, 0, 0, imageTransparentSpeed * Time.deltaTime);
            }
            else
            {
                text.color = new Color(0, 0, 0, 0);
                isPop = false;
                speed = 0;
                if(transform.parent != null)
                    Destroy(transform.parent.gameObject);
            }
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}