using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovmentController : MonoBehaviour
{

    public GameObject CameraTarget;
    public float cameraSpeed = 3.0f;
    public AnimationCurve smoothAnimation = AnimationCurve.EaseInOut(0,0,1,1);

    private Vector3 startPosition;
    private Quaternion startRotation;

    private float lerpTimer = 0;
    private float t = 0;

    private void Start()
    {
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        lerpTimer += Time.deltaTime;
        t = lerpTimer / cameraSpeed;

        if (t < .999)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, CameraTarget.transform.position, smoothAnimation.Evaluate(t));
            gameObject.transform.rotation = Quaternion.Lerp(startRotation, CameraTarget.transform.rotation, smoothAnimation.Evaluate(t));

        }
        else
        {
            gameObject.transform.position = CameraTarget.transform.position;
            gameObject.transform.rotation = CameraTarget.transform.rotation;
        }
    }

    public void setCameraTarget(GameObject target)
    {
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;

        CameraTarget = target;
        lerpTimer = 0;
    } 
}
