using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float smoothing;
    public Vector2 maxPos;
    public Vector2 minPos;
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != target.position)
        {
            Vector3 targetposition =  new Vector3(target.position.x, target.position.y, transform.position.z);

            targetposition.x = Mathf.Clamp(targetposition.x, minPos.x, maxPos.x);
            targetposition.y = Mathf.Clamp(targetposition.y , minPos.y, maxPos.y);

            transform.position = Vector3.Lerp(transform.position, targetposition, smoothing);
        }
    }
}
