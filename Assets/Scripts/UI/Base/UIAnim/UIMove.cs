using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMove : MonoBehaviour
{
    public static UIMove instance;
    public bool isMoving = false;
    public Vector3 destination;
    public float sumTime = 0.0f;
    public float acceralationTime = 0.0f;
    private float distance = 0.0f;
    private float speed = 0.0f;
    private float maxSpeed = 0.0f;
    private float acceralation = 0.0f;
    private float counter = 0.0f;
    private Vector3 direction = Vector3.zero;
    public void Move(Vector3 targetDestination)
    {
        destination = targetDestination;
        distance = Vector3.Distance(transform.localPosition, destination);
        direction = (destination - transform.localPosition).normalized;
        maxSpeed = distance / (sumTime - acceralationTime);
        acceralation = maxSpeed / acceralationTime;
        speed = 0;
        counter = 0;
        isMoving = true;
    }
    void Awake()
    {
        if(instance == null)
            instance = this;
    }
    void Update()
    {
        if (isMoving)
        {
            if (counter < acceralationTime)
            {
                speed += acceralation * Time.deltaTime;
            }
            else if (counter < sumTime - acceralationTime)
            {
                speed = maxSpeed;
            }
            else if (counter < sumTime)
            {
                speed -= acceralation * Time.deltaTime;
            }
            else
            {
                transform.localPosition = destination;
                speed = 0;
                isMoving = false;
            }
            float routeRatio = (destination - transform.localPosition).x / direction.x;
            if(routeRatio == 0)
                routeRatio = (destination - transform.localPosition).y / direction.y;
            if(routeRatio == 0)
                routeRatio = (destination - transform.localPosition).z / direction.z;
            if(routeRatio > 0.1f)
                transform.localPosition += direction * speed * Time.deltaTime;
            else
                transform.localPosition = destination;
            counter += Time.deltaTime;
        }
    }
}