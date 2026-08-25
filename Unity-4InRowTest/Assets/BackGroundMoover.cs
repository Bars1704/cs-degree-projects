using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMoover : MonoBehaviour
{
    public GameObject first;
    public GameObject second;
    public float speed = 1;
    Vector3 step;
    Plane[] planes;
    Vector3 speedVector;
    SpriteRenderer firstSprite;
    SpriteRenderer secondSprite;
    void Start()
    {
        speedVector = new Vector3(speed, 0, 0);
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        firstSprite = first.GetComponent<SpriteRenderer>();
        secondSprite = second.GetComponent<SpriteRenderer>();
        step = first.transform.position - second.transform.position;
    }
    void Update()
    {
        first.transform.position += speedVector * Time.deltaTime;
        second.transform.position += speedVector * Time.deltaTime;
        if (!GeometryUtility.TestPlanesAABB(planes, firstSprite.bounds))
        {
            first.transform.position = second.transform.position - step;
        }
        if (!GeometryUtility.TestPlanesAABB(planes, secondSprite.bounds))
        {
            secondSprite.transform.position = first.transform.position - step;
        }
    }
}
