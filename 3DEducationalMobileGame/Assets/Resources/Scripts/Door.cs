using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    public bool isOpen = false;

    // Oyuncu trigger içinde mi?
    private bool playerInZone = false;
    // Oyuncu Dýþarý (Outside) triggerýnda mý? (False ise içeridedir)
    private bool playerIsOutside = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Sadece oyuncu alandaysa ve E tuþuna basarsa çalýþýr
        if (playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                // Kapý açýksa, hangi tarafta olursak olalým kapatýrýz
                CloseDoor();
            }
            else
            {
                // Kapý kapalýysa, bulunduðumuz tarafa göre doðru yöne açarýz
                if (playerIsOutside)
                {
                    OpenOutwards(); // Dýþarýdayýz -> Ýçeri aç
                }
                else
                {
                    OpenInwards(); // Ýçerideyiz -> Dýþarý aç
                }
            }
        }
    }

    // Triggerlar bu fonksiyonu çaðýrarak durumu bildirir
    public void SetPlayerZone(bool inZone, bool isOutsideSide)
    {
        playerInZone = inZone;
        playerIsOutside = isOutsideSide;
    }

    // --- Animasyon Fonksiyonlarý (Ayný) ---

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

    public void ResetClose()
    {
        animator.SetBool("Close", false);
    }
}