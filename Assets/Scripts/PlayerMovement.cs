using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header ("Movement Parameters")]
    [SerializeField] float controlSpeed = 20f;
    [SerializeField] float xClampedRange = 5f;
    [SerializeField] float yClampedRange = 8f;

    [Header("Player Rotation")]

    [SerializeField] float rollControl = 20f;
    [SerializeField] float pitchControl = 15f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] Transform targetPoint; //bunu ben ekledim


    Vector2 movement;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();

    }

    
    public void OnMove(InputValue value)
    {
        Debug.Log(value.Get<Vector2>());
        movement = value.Get<Vector2>();

    }
    private void ProcessTranslation()
    {
        float xOffset = movement.x * controlSpeed * Time.deltaTime;
        float xPos = transform.localPosition.x + xOffset;
        float xClampedPos = Mathf.Clamp(xPos, -xClampedRange, xClampedRange);


        float yOffset = movement.y * controlSpeed * Time.deltaTime;
        float yPos = transform.localPosition.y + yOffset;
        float yClampedPos = Mathf.Clamp(yPos, -yClampedRange, yClampedRange);


        transform.localPosition = new Vector3(xClampedPos, yClampedPos, 0f);
    }

    //private void ProcessRotation()
    //{
    //    //Hard coding rotation...
    //    Quaternion targetRotation = Quaternion.Euler(-pitchControl * movement.y , 0f, rollControl * movement.x);
    //    transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * 5);
    //}
    private void ProcessRotation()
    {
        Vector3 directionWorld = targetPoint.position - transform.position;
        Vector3 directionLocal = transform.parent.InverseTransformDirection(directionWorld);
        Quaternion aimRotation = Quaternion.LookRotation(directionLocal);

        Quaternion inputRotation = Quaternion.Euler(-pitchControl * movement.y, 0f, rollControl * movement.x);

        Quaternion targetRotation = aimRotation * inputRotation;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
