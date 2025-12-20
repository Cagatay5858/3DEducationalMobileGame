using UnityEngine;

public class GuidanceSystem : MonoBehaviour
{
    public Transform player; // Oyuncunun transform'u
    private Transform currentTarget;
    private bool isActive = false;

    [Header("Ayarlar")]
    public Vector3 rotationOffset = new Vector3(0, 0, 0); // Eðer ok yan duruyorsa buradan 90, 0, 0 veya 0, 90, 0 vererek düzelt.

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

    void LateUpdate()
    {
        if (!isActive || currentTarget == null) return;

        // 1. Okun Konumu:
        // Eðer ok oyuncunun child objesiyse, konumu zaten oyuncuyla gelir, buraya kod yazmaya gerek yok.
        // Eðer child deðilse: transform.position = player.position + Vector3.up * 2f;

        // 2. Hedefin Yönünü Hesapla
        Vector3 directionToTarget = currentTarget.position - transform.position;
        directionToTarget.y = 0; // Yükseklik farkýný sýfýrla (Ok yukarý/aþaðý bakmasýn)

        if (directionToTarget != Vector3.zero)
        {
            // Hedefe bakan saf rotasyonu bul
            Quaternion lookRot = Quaternion.LookRotation(directionToTarget);

            // Eðer modelin yönü bozuksa offset ekle
            // Çoðu 3D ok modeli "Yatýk" gelir, düzeltmek için (90, 0, 0) gerekebilir.
            // Inspector'dan "Rotation Offset" deðerleriyle oynayarak doðru açýyý bulabilirsin.
            transform.rotation = lookRot * Quaternion.Euler(rotationOffset);
        }
    }
}