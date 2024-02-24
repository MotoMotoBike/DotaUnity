using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            float Angle = -Mathf.Atan2(hit.point.z-transform.position.z, hit.point.x-transform.position.x)/Mathf.PI*180f;
            float DeltaAng = Mathf.DeltaAngle(transform.eulerAngles.y, Angle + 90);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Angle + 90, transform.eulerAngles.z);
      
        }
    }
}
