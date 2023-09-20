using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicCheck : MonoBehaviour
{
    public Vector2 upPoint, donwPoint, leftPoint, rightPoint;
    public float radius;
    public bool isUp, isDonw, isLeft, isRight, onWall;
    public LayerMask checkLayer;

    private void Start()
    {
        checkLayer = 1 << 6;
    }

    private void Update()
    {
        isUp = Physics2D.OverlapCircle((Vector2)transform.position + upPoint, radius, checkLayer);
        isDonw = Physics2D.OverlapCircle((Vector2)transform.position + donwPoint, radius, checkLayer);
        isLeft = Physics2D.OverlapCircle((Vector2)transform.position + leftPoint, radius, checkLayer);
        isRight = Physics2D.OverlapCircle((Vector2)transform.position + rightPoint, radius, checkLayer);
        onWall = isUp || isDonw || isRight || isLeft;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + upPoint, radius);
        Gizmos.DrawWireSphere((Vector2)transform.position + donwPoint, radius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftPoint, radius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightPoint, radius);
    }

}
