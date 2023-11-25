using UnityEngine;

public class DeathLimit : MonoBehaviour
{
    private float respawnHeight = -5f;

    void Update()
    {
        if (gameObject.CompareTag("Player") && transform.position.y < respawnHeight)
        {
           
        }
    }
}

    