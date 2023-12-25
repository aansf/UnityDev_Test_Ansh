using UnityEngine;

namespace Player.Collectibles
{
    public class CubeCollector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // Check if the collided object is a cube
            if (other.CompareTag("Cube"))
            {
                // Destroy the cube
                Destroy(other.gameObject);

                // You can add additional logic or UI updates here
                Debug.Log("Collected and Destroyed Cube!");
            }
        }
    }
}
