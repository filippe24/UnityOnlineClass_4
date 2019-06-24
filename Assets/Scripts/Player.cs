using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;



    [Tooltip("In ms^-1")] [SerializeField] float speed = 20f;
    //the range define how far we want to go on the sides
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       processTranslation();
       ProcessRotation();
    }

    private void processTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {

        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;

        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
