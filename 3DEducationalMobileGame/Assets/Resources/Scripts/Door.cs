using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    public bool isOpen = false;

    // Oyuncu kapýnýn hangi tarafýnda?
    private bool playerIsOutside = false;
    private bool isPlayerInZone = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Trigger alanýna girince Butonu Göster
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Oyuncunun tag'i "Player" olmalý
        {
            isPlayerInZone = true;
            UpdateDoorButton();
        }
    }

    // Trigger alanýndan çýkýnca Butonu Gizle
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            UIManager.Instance.HideActionButton();
        }
    }

    // Burasý "Outside" trigger'ý tarafýndan çaðrýlabilir (Eðer ayrý triggerlarýn varsa)
    // Eðer tek bir trigger kullanýyorsan ve yönü oyuncunun konumuna göre belirleyeceksen:
    public void SetPlayerZone(bool inZone, bool isOutside)
    {
        // Bu metodu mevcut yapýna göre uyarlayabilirsin. 
        // Eðer trigger scriptlerin ayrýysa ve bunu çaðýrýyorsa:
        isPlayerInZone = inZone;
        playerIsOutside = isOutside;

        if (isPlayerInZone) UpdateDoorButton();
        else UIManager.Instance.HideActionButton();
    }

    void UpdateDoorButton()
    {
        string actionText = isOpen ? "Kapýyý Kapat" : "Kapýyý Aç";

        // Butona basýlýnca "InteractWithDoor" fonksiyonu çalýþsýn
        UIManager.Instance.ShowActionButton(actionText, InteractWithDoor);
    }

    public void InteractWithDoor()
    {
        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            // Oyuncunun kapýya göre konumunu bul (Basit Yöntem)
            // Kapýnýn forward yönü ile oyuncu yönüne bakarak:
            Vector3 directionToPlayer = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);

            // Eðer dotProduct > 0 ise oyuncu kapýnýn önünde (Outside), < 0 ise arkasýndadýr (Inside)
            // Bu yöntem triggerlardan daha güvenilirdir.
            if (dotProduct > 0) OpenOutwards();
            else OpenInwards();
        }

        // Kapý durumu deðiþtiði için buton yazýsýný güncelle (Aç -> Kapat)
        UpdateDoorButton();
    }

    // ... OpenInwards, OpenOutwards, CloseDoor fonksiyonlarý AYNI kalacak ...
    public void OpenInwards()
    {
        if (!isOpen)
        {
            animator.SetBool("OpenInside", true);
            animator.SetBool("OpenOutside", false);
            animator.SetBool("Close", false);
            isOpen = true;
        }
    }
    // ... Diðerleri ...
    public void OpenOutwards()
    {
        if (!isOpen)
        {
            animator.SetBool("OpenOutside", true);
            animator.SetBool("OpenInside", false);
            animator.SetBool("Close", false);
            isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            animator.SetBool("Close", true);
            animator.SetBool("OpenInside", false);
            animator.SetBool("OpenOutside", false);
            isOpen = false;
        }
    }
}