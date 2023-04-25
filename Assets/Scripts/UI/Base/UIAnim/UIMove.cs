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
            if (speed < maxSpeed - acceralation * Time.deltaTime && counter < acceralationTime - Time.deltaTime)
            {
                speed += acceralation * Time.deltaTime;
            }
            else if (speed < maxSpeed && counter < sumTime - acceralationTime - Time.deltaTime)
            {
                speed = maxSpeed;
            }
            else if (speed > acceralation * Time.deltaTime && counter < sumTime - Time.deltaTime)
            {
                speed -= acceralation * Time.deltaTime;
            }
            else
            {
                transform.localPosition = destination;
                speed = 0;
                isMoving = false;
            }
            transform.localPosition += direction * speed * Time.deltaTime;
            counter += Time.deltaTime;
        }
    }
}