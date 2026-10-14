using UnityEngine;

public class CollisionSystem : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("player ship hit: " + other.gameObject.name);
        Instantiate(destroyVFX, transform.position, Quaternion.identity);
    }
}
