using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;
using UnityEngine.Android;
using RiptideNetworking;
using System;


public class SensorComponent : MonoBehaviour
{
    // TOUCH
    // The current altitude of spaceship
    float altitude;

    // ACCELERATOR
    // The threshold for detecting device is shaking strong enough
    float shakeDetectionThreshold = 2f;

    // The minimum interval between two shakes
    float shakeInterval = 2f;

    // The acceleration of the device in the previous frame
    Vector3 lastAcceleration = new(0, 0, 0);

    // How long has been taken since the last shake
    float timeSinceLastShake = 0;

    // STEP COUNTER
    // The last measured steps count by the device
    int lastStepCount = 0;

    // Changes the color of spaceship
    void ChangeColor()
    {
        // Generates random color in RGBa with random values
        // https://docs.unity3d.com/ScriptReference/Random-value.html
        Color randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);

        // It "finds" the exact object (child of current object), and change its color by using its renderer
        // https://discussions.unity.com/t/renderer-material-color-not-changing-color-of-prefab/246469
        // https://docs.unity3d.com/ScriptReference/GameObject.Find.html
        // https://docs.unity3d.com/ScriptReference/Material.SetColor.html
        GameObject.Find("cutter").GetComponent<MeshRenderer>().material.SetColor("_Color", randomColor);
    }

    void Awake()
    {
        // It gets permission from the user (player) for getting activity information the moment the application opens (just once)
        // https://docs.unity3d.com/Manual/android-RequestingPermissions.html
        // https://docs.unity3d.com/Manual/overriding-android-manifest.html#creating-a-template-android-manifest-file
        Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION");
    }

    void Start()
    {
        // https://docs.unity3d.com/ScriptReference/Input-gyro.html
        Input.gyro.enabled = true;

        // https://www.reddit.com/r/Unity3D/comments/wjrg5q/how_to_use_the_pedometerstepcounter_sensor/jcesp2h/?utm_source=share&utm_medium=mweb3x&utm_name=mweb3xcss&utm_term=1&utm_content=share_button
        InputSystem.EnableDevice(AndroidStepCounter.current);
        AndroidStepCounter.current.MakeCurrent();
    }

    // Creates the message using value and type then sends it to server
    void SendValue(NetworkManager.MessageType messageType, string value)
    {
        Message message = Message.Create(MessageSendMode.unreliable, (ushort)messageType);
        message.AddString(value);
        NetworkManager.Singleton.Client.Send(message);
    }

    void Update()
    {
        // TOUCH
        // Checks if user is touching the screen
        if (Input.touchCount > 0)
        {
            // https://docs.unity3d.com/2022.1/Documentation/ScriptReference/Touch.html
            // Multi-touch is handled. Just handling the first finger.
            var touch = Input.GetTouch(0);

            // At the start (and end) of finger swiping, the altitude of spaceship should be stored to be used for adjusting the relative
            // altitude during moving phase of swipe.
            // "Ended" is considered for handling when player finger goes out of the screen.
            if (touch.phase == UnityEngine.TouchPhase.Began || touch.phase == UnityEngine.TouchPhase.Ended)
            {
                altitude = transform.position.y;
            }
            else if (touch.phase == UnityEngine.TouchPhase.Moved)
            {
                SendValue(NetworkManager.MessageType.TouchValue, touch.position.ToString());

                var delta = (touch.position - touch.rawPosition).y;
                // https://docs.unity3d.com/ScriptReference/Transform-position.html
                // The value of 0.05 is used to decrease the speed of spaceship movement. When changing the touch position,
                // the spaceship's rate of altitude change was too high, so I decreased it by 0.05 rate.
                transform.position = new Vector3(transform.position.x, delta * 0.05f + altitude, transform.position.z);

                // The spaceship's initial position on the y-axis is at 30 units. To make the movements more logical, it can reach
                // a maximum altitude of 50 and a minimum altitude of 10.
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

        // GYROSCOPE
        // Spaceship should be rotated only through the y-axis, so the angle with x and z axis are always 0.
        // By try and error the correct axis of device gyroscope attitude was found.
        // https://docs.unity3d.com/ScriptReference/Transform-eulerAngles.html
        // https://docs.unity3d.com/ScriptReference/Input-gyro.html
        transform.eulerAngles = new Vector3(0, -Input.gyro.attitude.eulerAngles.z, 0);
        SendValue(NetworkManager.MessageType.GyroscopeValue, Input.gyro.attitude.ToString());

        //ACCELERATOR 
        // Reads the device acceleration
        var accelerometerReading = Input.acceleration;
        SendValue(NetworkManager.MessageType.AcceleratorValue, accelerometerReading.ToString());

        // Calculates delta acceleration 
        Vector3 deltaAcceleration = accelerometerReading - lastAcceleration;

        // Calculates the magnitude ^ 2 of delta acceleration vector. The squared value is used because
        // calculating magnitude itself is slower and unnecessary for comparison
        var accelerationForce = deltaAcceleration.sqrMagnitude;

        // Updates time since last shake
        timeSinceLastShake += Time.deltaTime;


        // Checks if delta acceleration is big enough and it has been taken appropriate time from the last shake
        if (accelerationForce >= shakeDetectionThreshold && timeSinceLastShake > shakeInterval)
        {
            ChangeColor();
            timeSinceLastShake = 0;
        }

        // Updates the last acceleration
        lastAcceleration = accelerometerReading;

        // STEP COUNTER
        var stepCountRead = AndroidStepCounter.current.stepCounter.ReadValue();
        SendValue(NetworkManager.MessageType.PedometerValue, stepCountRead.ToString());
        // Checks if previous measured steps count has increased or not
        if (lastStepCount < stepCountRead)
        {
            // https://docs.unity3d.com/ScriptReference/Transform.Translate.html
            // Moves the spaceship 10 units forward
            transform.Translate(Vector3.forward * 10f);

            //Updates the last step count
            lastStepCount = stepCountRead;
        }
    }
}
