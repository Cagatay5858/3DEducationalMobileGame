using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton yaptýk ki Door.cs kolayca ulaþsýn

    [Header("Choice Panel")]
    public GameObject choicePanel;
    public TextMeshProUGUI titleText;
    public Button buttonA;
    public Button buttonB;
    public TextMeshProUGUI textA;
    public TextMeshProUGUI textB;

    [Header("Interaction")]
    public Button actionButton;
    public TextMeshProUGUI actionButtonText; // Butonun üzerindeki yazý (Örn: "Kapýyý Aç", "Tamamla")

    [Header("Result Panel")]
    public GameObject resultPanel;
    public Transform resultListContainer;
    public GameObject resultItemPrefab;
    public TextMeshProUGUI finalScoreText;

    void Awake()
    {
        Instance = this;
    }

    public void ShowActionButton(string buttonLabel, UnityAction onClickAction)
    {
        if (actionButton != null)
        {
            actionButton.gameObject.SetActive(true);

            // Eðer butonun içinde text varsa onu deðiþtir
            if (actionButtonText != null) actionButtonText.text = buttonLabel;

            // Eski týklama olaylarýný sil ve yenisini ekle
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(onClickAction);
        }
    }

    public void HideActionButton()
    {
        if (actionButton != null)
        {
            actionButton.onClick.RemoveAllListeners();
            actionButton.gameObject.SetActive(false);
        }
    }

    // ... ShowChoices metodu AYNI kalacak ...
    public void ShowChoices(string title, HabitData h1, HabitData h2, System.Action<HabitData> onSelected)
    {
        choicePanel.SetActive(true);
        resultPanel.SetActive(false);
        HideActionButton(); // Seçim sýrasýnda buton gizlensin

        titleText.text = title;
        textA.text = h1.habitName;
        textB.text = h2.habitName;

        buttonA.onClick.RemoveAllListeners();
        buttonB.onClick.RemoveAllListeners();

        buttonA.onClick.AddListener(() => { choicePanel.SetActive(false); onSelected(h1); });
        buttonB.onClick.AddListener(() => { choicePanel.SetActive(false); onSelected(h2); });
    }

    // YENÝ: Butonu açýp kapatmak için yardýmcý fonksiyon
    public void SetActionButtonState(bool isActive)
    {
        if (actionButton != null)
            actionButton.gameObject.SetActive(isActive);
    }

    // ... ShowFinalResults metodu AYNI kalacak ...
    public void ShowFinalResults(System.Collections.Generic.List<HabitData> playerChoices)
    {
        HideActionButton();
        choicePanel.SetActive(false);
        resultPanel.SetActive(true);
        SetActionButtonState(false); // Oyun bittiðinde butonu gizle

        int score = 0;
        foreach (Transform child in resultListContainer) Destroy(child.gameObject);

        foreach (var habit in playerChoices)
        {
            GameObject item = Instantiate(resultItemPrefab, resultListContainer);
            TextMeshProUGUI itemText = item.GetComponentInChildren<TextMeshProUGUI>();

            if (habit.isGood)
            {
                score += 20;
                itemText.text = $"<color=green>DOÐRU:</color> {habit.habitName}";
            }
            else
            {
                itemText.text = $"<color=red>YANLIÞ:</color> {habit.habitName}\n<size=80%><i>Nedeni: {habit.explanationIfWrong}</i></size>";
            }
        }
        finalScoreText.text = "Toplam Puan: " + score;
    }
}