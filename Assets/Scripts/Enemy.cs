using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] int hitPoints = 1; //Initial enemy health point
    [SerializeField] GameObject destroyVFX;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnParticleCollision(GameObject other)
    {
        Destroy();
    }

    private void Destroy()
    {
        hitPoints--;
        if (hitPoints <= 0)
        {
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Debug.Log("Enemy destroyed");
        }
    }
}
