using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround_Type01 : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float scrollAmount;
    [SerializeField] float scrollSpeed;
    [SerializeField] Vector3 scrollDir;

    private void Update()
    {
        transform.position += scrollDir * scrollSpeed * Time.deltaTime;

        if (transform.position.x <= -scrollAmount)
        {
            transform.position = target.position - scrollDir * scrollAmount;
        }
    }
}
