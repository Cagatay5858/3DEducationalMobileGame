using UnityEngine;

public class InsideTrigger : MonoBehaviour
{
    public Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Oyuncu alanda (true) ve Dýþarý tarafýnda DEÐÝL (false -> yani içeride)
            door.SetPlayerZone(true, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Oyuncu alandan çýktý
            door.SetPlayerZone(false, false);
        }
    }
}