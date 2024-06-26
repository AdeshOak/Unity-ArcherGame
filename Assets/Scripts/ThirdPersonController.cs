using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    Animator anim;
    public float maxSpeed = 5f;
    public float acceleration = 5f; 
    public float deceleration = 5f;
    public Transform cam;
    public float turnSpeedSmooth = .5f;
    float turnSmoothVelocity;

    [HideInInspector]
    public float speed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;

        if (input.magnitude >= .05f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSpeedSmooth);
            
            if (speed < maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
            else
            {

                // ensure slow speeds do not make the character jitter
                speed = Mathf.Lerp(speed, maxSpeed, .1f);
            }
          //transform.forward = direction;
          transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        else
        {
            speed -= deceleration * Time.deltaTime;
        }
        anim.SetFloat("speed", speed);
        if (speed < 0) speed =0;
    }
}
