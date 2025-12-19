using UnityEngine;

public class GuidanceSystem : MonoBehaviour
{
    public Transform player; // Oyuncunun karakteri
    private Transform currentTarget;
    private bool isActive = false;

    public void SetTarget(Transform target)
    {
        currentTarget = target;
        isActive = true;
        gameObject.SetActive(true);
    }

    public void StopGuidance()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isActive || currentTarget == null) return;

        // Ok her zaman hedefe baksýn
        transform.LookAt(currentTarget);

        // Ok oyuncunun biraz üzerinde veya önünde dursun (Opsiyonel)
        // transform.position = player.position + Vector3.up * 2; 
    }
}