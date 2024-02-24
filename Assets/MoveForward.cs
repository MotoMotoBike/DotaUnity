using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float destroyAfter;
    float lifeTime = 0;
    void Start(){
        speed /= 100;
    }
    void FixedUpdate()
    {
        transform.position = transform.position + (speed * this.transform.forward);
        lifeTime += Time.deltaTime;
        if(destroyAfter < lifeTime){
            Destroy(gameObject);
        }
    }
}
