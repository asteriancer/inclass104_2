using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool isFiring = false;

    [SerializeField] GameObject[] lasers;
    [SerializeField] RectTransform crosshair;
    [SerializeField] float targetDistance =100f;
    [SerializeField] Transform targetPoint;
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessFire();
        MoveCrosshair();
        MoveTargetPoint();
    }
    public void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
        //Debug.Log("Firing");
    }

    void ProcessFire()
    {
        if (isFiring)
        {
            Debug.Log("Fire");
        }
        
        foreach (GameObject laser in lasers)
        {
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = isFiring;
        }
    }
    void MoveCrosshair()
    {
        crosshair.position = Input.mousePosition;

    }

    void MoveTargetPoint()
    {
        Vector3 targetPositionPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetDistance);
        targetPoint.position = Camera.main.ScreenToWorldPoint(targetPositionPos);
    }
}
