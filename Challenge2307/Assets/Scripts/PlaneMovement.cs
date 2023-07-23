using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    Rigidbody rb;

    public float frontValue;
    public float frontEvalulate;

    float defaultSpeed = 5;
    float availableAdditionalSpeed = 15;

    public AnimationCurve curve;


    public float timeMouseMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        CalculateDirection();
        CalculateRotation();
    }

    void Inputs()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            frontValue = Mathf.Lerp(frontValue, 1, Time.deltaTime);
        }
        else
        {
            frontValue = Mathf.Lerp(frontValue, 0, Time.deltaTime);
        }


        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            timeMouseMovement = -1;
        }
        else
        {
            if (timeMouseMovement == -1)
            {
                timeMouseMovement = Time.time;
            }
        }

    }

    void CalculateDirection()
    {
        Vector3 finalVector;
        Vector3 defaultVector;
        Vector3 additionalVector;
        Vector3 gravityVector;
        Vector3 forwardVector = transform.forward;


        defaultVector = forwardVector * defaultSpeed;

        frontEvalulate = curve.Evaluate(frontValue);
        additionalVector = forwardVector * frontEvalulate * availableAdditionalSpeed;

        gravityVector = Vector3.down * 0.5f;     // gravity

        finalVector = defaultVector + additionalVector + gravityVector;

        rb.velocity = finalVector;
    }

    void CalculateRotation()
    {
        if (timeMouseMovement != -1)
        {
            if (Time.time > timeMouseMovement + 1)           // return rotation to default
            {
                Vector3 currentRotation = transform.eulerAngles;
                currentRotation.x = Mathf.LerpAngle(currentRotation.x, 0f, Time.deltaTime * 0.5f);
                currentRotation.z = Mathf.LerpAngle(currentRotation.z, 0f, Time.deltaTime * 0.5f);

                transform.eulerAngles = currentRotation;
            }
        }
        else                // calculate new rotation
        {
            transform.Rotate(new Vector3(10 * Input.GetAxis("Mouse Y") * Time.deltaTime, 0, 0));
            transform.Rotate(new Vector3(0, 0, 10 * Input.GetAxis("Mouse X") * Time.deltaTime));
        }
    }
}