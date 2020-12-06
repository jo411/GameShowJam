using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.   
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    Vector3 offset;
    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
      //  originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
           offset = Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            offset = Vector3.zero;
        }
    }

    public void shakeCam()
    {
        shakeCam(.2f, .2f, 1.0f);
    }
    
    public Vector3 getOffset()
    {
        return offset;
    }


    public void shakeCam(float duration, float intensity, float fallOff)
    {
        shakeAmount = intensity;
        shakeDuration = fallOff;
        shakeDuration = duration;
    }
}