using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImagePop : MonoBehaviour
{
    private bool isPop = false;
    private Vector3 startPos;
    public float acceralationTime;
    public float stopTime;
    public float popDistance;
    public float initSpeed;
    public float maxImageTransparency = 0.7f;
    private float acceralation;
    private float maxSpeed;
    private float counter = 0;
    private float speed;
    private float imageTransparentSpeed;
    private Vector3 direction = Vector3.up;
    private Image image;
    public void Pop(Vector3 targetPos)
    {
        isPop = true;
        counter = 0;
        maxSpeed = 2 * popDistance / acceralationTime - initSpeed;
        acceralation = (maxSpeed - initSpeed) / acceralationTime;
        image.color = new Color(1, 1, 1, 0);
        speed = maxSpeed;
        transform.position = targetPos;
        imageTransparentSpeed = maxImageTransparency / acceralationTime;
    }
    private void Start()
    {
        image = GetComponent<Image>();
        imageTransparentSpeed = maxImageTransparency / acceralationTime;
    }
    void Update()
    {
        if (isPop)
        {
            counter += Time.deltaTime;
            if (counter < acceralationTime)
            {
                speed -= acceralation * Time.deltaTime;
                image.color += new Color(0, 0, 0, imageTransparentSpeed * Time.deltaTime);
            }
            else if(counter < acceralationTime + stopTime)
            {
                speed = 0;
                image.color = new Color(1, 1, 1, 1);
            }
            else if(counter < acceralationTime * 2 + stopTime)
            {
                speed = maxSpeed - acceralation * (counter - acceralationTime - stopTime);
                image.color -= new Color(0, 0, 0, imageTransparentSpeed * Time.deltaTime);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
                isPop = false;
                speed = 0;
            }
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}