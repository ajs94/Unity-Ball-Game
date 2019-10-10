using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;
    public Vector3 pointB;

    private Vector3 pointA;

    void Start () {
		pointA = transform.position;
    }
	
	void Update () {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.SmoothStep(0f, 1f, Mathf.PingPong( speed * (Time.time / 5f), 1f)));
    }
}
