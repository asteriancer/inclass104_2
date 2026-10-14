using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject[] lasers;
    [SerializeField] RectTransform crosshair;
    [SerializeField] Transform targetPoint;

    [Header("Aiming")]
    [SerializeField] float maxAimDistance = 500f;     
    [SerializeField] float defaultAimDistance = 200f; 

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

    
    public void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
    }

    
    void ProcessFire()
    {
        foreach (GameObject laser in lasers)
        {
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = isFiring;
        }
    }

    
    void MoveCrosshair()
    {
        crosshair.position = Mouse.current.position.ReadValue();
    }

    
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

    
    void AimLasers()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint.position - transform.position);

        foreach (GameObject laser in lasers)
        {
            laser.transform.rotation = targetRotation;
        }
    }
}