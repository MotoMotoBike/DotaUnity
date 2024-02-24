using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;

    void Update()
    {
        transform.position = target.position;
    }
}
