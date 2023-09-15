using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public bool jump = false, down = false;
    public float maxJump = 1f;
    float initialPosY;
    public float j = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        initialPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaY = 0;
        if (Input.GetKeyDown(KeyCode.Space)) { jump = true; j = 0.1f; }
        if (jump)
        {
            deltaY = j;
            j *= 0.95f;
        }
        if (transform.position.y >= maxJump+initialPosY) { jump = false; down = true; j = 0.1f; }
        if (down)
        {
            deltaY = -j;
            j *= 1.05f;
        }
        if (transform.position.y <= initialPosY) { down = false; j = 0.1f; }



        transform.Translate(0, deltaY, 0);

    }
}
