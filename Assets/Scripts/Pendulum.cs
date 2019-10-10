using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour {

    public float angle = 40.0f;
    public float speed = 1.5f;

    public int x_dir;
    public int z_dir;

    private Vector3 dir;

    Quaternion qStart, qEnd;

    void Start()
    {
        dir = new Vector3(x_dir, 0, z_dir);
        qStart = Quaternion.AngleAxis(angle, dir);
        qEnd = Quaternion.AngleAxis(-angle, dir);
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(qStart, qEnd, (Mathf.Sin(Time.time * speed) + 1.0f) / 2.0f);
    }
}
