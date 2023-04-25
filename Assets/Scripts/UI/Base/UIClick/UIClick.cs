using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClick : MonoBehaviour
{
    protected UIMove move;
    protected UITextPop textPop;
    protected UIImagePop imagePop;
    protected List<UIShow> shows;
    protected Vector3 startPosition;
    protected Vector3 targetPosition;
    void Start()
    {
        move = UIMove.instance;
    }
    public void Pop()
    {
        textPop.Pop(targetPosition);
        imagePop.Pop(targetPosition);
    }
    public void MoveToTarget()
    {
        move = UIMove.instance;
        move.Move(targetPosition);
    }
    public void Appear()
    {
        foreach (UIShow show in shows)
        {
            show.Appear(startPosition, targetPosition);
        }
    }
    public void Disappear()
    {
        foreach (UIShow show in shows)
        {
            show.Disappear();
        }
    }
}
