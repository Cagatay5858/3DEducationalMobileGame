using UnityEngine;

public class OutsideTrigger : MonoBehaviour
{
    public Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Oyuncu alanda (true) ve Dýþarý tarafýnda (true)
            door.SetPlayerZone(true, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Oyuncu alandan çýktý (false)
            // Ýkinci parametrenin önemi kalmadý ama format gereði gönderiyoruz
            door.SetPlayerZone(false, true);
        }
    }
}