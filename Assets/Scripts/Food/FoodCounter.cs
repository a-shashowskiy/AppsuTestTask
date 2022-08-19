using UnityEngine;
namespace UI
{
    public class FoodCounter : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Debug.Log("COLLISION");
            if (other.CompareTag("Food"))
            {
                UIController.AddScore();
                Destroy(other.gameObject);
            }
        }
    }

}
