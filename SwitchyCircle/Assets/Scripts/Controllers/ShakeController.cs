using System.Collections;
using UnityEngine;

public class ShakeController : MonoBehaviour
{
    // The original position of the object
    public Vector3 objectOrigin;

    // How violently to shake the object
    public Vector3  strength;
    private Vector3 strengthDefault;

    // How quickly to settle down from shaking
    public float decay = 0.8f;

    // How many seconds to shake
    public float    shakeTime = 1f;
    private float   shakeTimeDefault;

    // Is this effect playing now?
    public bool isShaking = false;

    void Start()
    {
        objectOrigin = transform.position;
        strengthDefault = strength;
        shakeTimeDefault = shakeTime;
    }

    /// Update is called every frame, if the MonoBehaviour is enabled.
    void Update()
    {
        if (isShaking == true)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                Vector3 tempPosition = transform.position;

                // Move the camera in all directions based on strength
                tempPosition.x = objectOrigin.x + Random.Range(-strength.x, strength.x);
                tempPosition.y = objectOrigin.y + Random.Range(-strength.y, strength.y);
                tempPosition.z = objectOrigin.z + Random.Range(-strength.z, strength.z);
                transform.position = tempPosition;

                // Gradually reduce the strength value
                strength *= decay;
            }
            else if (transform.position != objectOrigin)
            {
                shakeTime = 0;

                // Reset the camera position
                transform.position = objectOrigin;
                isShaking = false;
                strength = strengthDefault;
                shakeTime = shakeTimeDefault;
            }
        }
    }

    /// Starts the shake of the camera.
    public void StartShake()
    {
        isShaking = true;
        strength = strengthDefault;
        shakeTime = shakeTimeDefault;
    }
}