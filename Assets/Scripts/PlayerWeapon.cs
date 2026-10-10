using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject[] lasers;
    [SerializeField] RectTransform crosshair;
    [SerializeField] Transform targetPoint;

    [Header("Aiming")]
    [SerializeField] float maxAimDistance = 500f;     // raycast'in ne kadar uzağa bakacağı
    [SerializeField] float defaultAimDistance = 200f; // hiçbir şeye çarpmazsa hedef mesafesi

    bool isFiring = false;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        ProcessFire();
        MoveCrosshair();
        MoveTargetPoint();
        AimLasers();
    }

    // Input Actions'taki Fire aksiyonu tarafından çağrılır
    public void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
    }

    // Ateş tuşu basılıyken lazerlerin emission'ını açar, bırakınca kapatır
    void ProcessFire()
    {
        foreach (GameObject laser in lasers)
        {
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = isFiring;
        }
    }

    // UI nişangahı mouse'un ekrandaki konumuna taşır
    void MoveCrosshair()
    {
        crosshair.position = Mouse.current.position.ReadValue();
    }

    // Mouse konumundan dünyaya ışın atar; bir şeye çarparsa hedef orası olur
    void MoveTargetPoint()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, maxAimDistance))
        {
            targetPoint.position = hit.point;
        }
        else
        {
            targetPoint.position = ray.GetPoint(defaultAimDistance);
        }
    }

    // İki lazeri de geminin merkezinden hedefe doğru aynı açıyla çevirir
    void AimLasers()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint.position - transform.position);

        foreach (GameObject laser in lasers)
        {
            laser.transform.rotation = targetRotation;
        }
    }
}