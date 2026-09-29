using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float controlSpeed = 12f;

    [Header("Bounds")]
    [SerializeField] float xRange = 8f;
    [SerializeField] float yMin = -4f;
    [SerializeField] float yMax = 5f;

    [Header("Smoothing")]
    [SerializeField] float smoothTime = 0.12f;   // büyüdükçe daha yumuşak (ama daha geç tepki)

    [Header("Tilt")]
    [SerializeField] float rollAmount = 25f;     // sağa/sola giderken yatma
    [SerializeField] float pitchAmount = 12f;    // yukarı/aşağı giderken burun
    [SerializeField] float rotationSpeed = 8f;

    Vector2 movement;          // ham input (0 ya da 1)
    Vector2 smoothMovement;    // yumuşatılmış input
    Vector2 smoothVelocity;

    void Update()
    {
        // Input'u yavaş yavaş hedefe yaklaştır -> hızlanma / yavaşlama hissi
        smoothMovement = Vector2.SmoothDamp(smoothMovement, movement, ref smoothVelocity, smoothTime);

        float xOffset = smoothMovement.x * controlSpeed * Time.deltaTime;
        float yOffset = smoothMovement.y * controlSpeed * Time.deltaTime;

        float xPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange);
        float yPos = Mathf.Clamp(transform.localPosition.y + yOffset, yMin, yMax);

        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);

        // Hareket yönüne göre hafif yatma
        Quaternion targetRotation = Quaternion.Euler(-smoothMovement.y * pitchAmount, 0f, -smoothMovement.x * rollAmount);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
}