using UnityEngine;

public class GuidanceArrow : MonoBehaviour
{
    [Header("Hedef Ayarlarý")]
    public Transform currentTarget; // Gidilecek hedef (GameManager atayacak)
    private bool isActive = false;

    [Header("Model Düzeltme (ÖNEMLÝ)")]
    // Ok modeli genelde yan veya dik gelir. Bunu düzeltmek için bu ayarlarý kullanacaðýz.
    public Vector3 rotationOffset = new Vector3(0, 0, 0);

    public void SetTarget(Transform target)
    {
        currentTarget = target;
        isActive = true;
        gameObject.SetActive(true); // Oku görünür yap
    }

    public void StopGuidance()
    {
        isActive = false;
        gameObject.SetActive(false); // Oku gizle
    }

    void LateUpdate()
    {
        if (!isActive || currentTarget == null) return;

        // 1. Hedefe olan yönü bul
        Vector3 direction = currentTarget.position - transform.position;
        direction.y = 0; // Ok yukarý/aþaðý bakmasýn, sadece saða/sola dönsün

        if (direction != Vector3.zero)
        {
            // 2. Hedefe bakacak temel rotasyonu hesapla
            Quaternion lookRot = Quaternion.LookRotation(direction);

            // 3. Modelin duruþ bozukluðunu (Offset) ekle ve uygula
            // Bu kod, üst objesi (Stickman) dönse bile okun hedefe kilitli kalmasýný saðlar.
            transform.rotation = lookRot * Quaternion.Euler(rotationOffset);
        }
    }
}