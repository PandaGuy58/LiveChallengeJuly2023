using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetMove;
    public Transform targetLookAt;
    public Transform objectLookAt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current = transform.position;
        Vector3 target = targetMove.position;

        Vector3 calculate = Vector3.Lerp(current, target, Time.deltaTime);
        transform.position = calculate;

        objectLookAt.LookAt(targetLookAt);
        Vector3 currentRotate = transform.eulerAngles;
        Vector3 targetRotate = objectLookAt.eulerAngles;
        Vector3 calculateRotate = new Vector3();

        calculateRotate.x = Mathf.LerpAngle(currentRotate.x, targetRotate.x, Time.deltaTime);
        calculateRotate.y = Mathf.LerpAngle(currentRotate.y, targetRotate.y, Time.deltaTime);
        calculateRotate.z = Mathf.LerpAngle(currentRotate.z, targetRotate.z, Time.deltaTime);

        transform.eulerAngles = calculateRotate;


    }
}
