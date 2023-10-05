using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;
using UnityEngine.Android;


public class SensorComponent : MonoBehaviour
{
    float altitude;

    float shakeDetectionThreshold = 2f;
    float shakeInterval = 2f;
    Vector3 lastAcceleration;
    float timeSinceLastShake = 0;

    int lastStepCount = 0;
    void ChangeColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        GameObject.Find("cutter").GetComponent<MeshRenderer>().material.SetColor("_Color", randomColor);
    }

    void Awake()
    {
        Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION");
    }

    void Start()
    {
        Input.gyro.enabled = true;
        InputSystem.EnableDevice(AndroidStepCounter.current);
        AndroidStepCounter.current.MakeCurrent();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == UnityEngine.TouchPhase.Began || touch.phase == UnityEngine.TouchPhase.Ended)
            {
                altitude = transform.position.y;
            }
            else if (touch.phase == UnityEngine.TouchPhase.Moved)
            {
                var delta = (touch.position - touch.rawPosition).y;
                transform.position = new Vector3(transform.position.x, delta * 0.05f + altitude, transform.position.z);
                if (transform.position.y > 50)
                {
                    transform.position = new Vector3(transform.position.x, 50, transform.position.z);
                }
                if (transform.position.y < 10)
                {
                    transform.position = new Vector3(transform.position.x, 10, transform.position.z);
                }
            }
        }

        transform.eulerAngles = new Vector3(0, -Input.gyro.attitude.eulerAngles.z, 0);

        var accelerometerReading = Input.acceleration;
        Vector3 deltaAcceleration = accelerometerReading - lastAcceleration;
        float accelerationForce = deltaAcceleration.sqrMagnitude;

        timeSinceLastShake += Time.deltaTime;

        if (accelerationForce >= shakeDetectionThreshold && timeSinceLastShake > shakeInterval)
        {
            ChangeColor();
            timeSinceLastShake = 0;
        }
        lastAcceleration = accelerometerReading;

        if (lastStepCount < AndroidStepCounter.current.stepCounter.ReadValue())
        {
            transform.Translate(Vector3.forward * 10f);
            lastStepCount = AndroidStepCounter.current.stepCounter.ReadValue();
        }
    }
}
