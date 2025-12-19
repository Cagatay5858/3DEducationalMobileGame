using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro kullandýðýný varsayýyorum
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("Choice Panel")]
    public GameObject choicePanel;
    public TextMeshProUGUI titleText;
    public Button buttonA;
    public Button buttonB;
    public TextMeshProUGUI textA;
    public TextMeshProUGUI textB;

    [Header("Result Panel")]
    public GameObject resultPanel;
    public Transform resultListContainer;
    public GameObject resultItemPrefab; // Sonuçlarýn listeleneceði prefab
    public TextMeshProUGUI finalScoreText;

    // Seçim ekranýný göster
    public void ShowChoices(string title, HabitData h1, HabitData h2, System.Action<HabitData> onSelected)
    {
        choicePanel.SetActive(true);
        resultPanel.SetActive(false);

        titleText.text = title;

        textA.text = h1.habitName;
        textB.text = h2.habitName;

        // Butonlarý temizle ve yeni listener ekle
        buttonA.onClick.RemoveAllListeners();
        buttonB.onClick.RemoveAllListeners();

        buttonA.onClick.AddListener(() => { choicePanel.SetActive(false); onSelected(h1); });
        buttonB.onClick.AddListener(() => { choicePanel.SetActive(false); onSelected(h2); });
    }

    // Oyun sonu ekranýný göster
    public void ShowFinalResults(List<HabitData> playerChoices)
    {
        choicePanel.SetActive(false);
        resultPanel.SetActive(true);

        int score = 0;

        // Önceki sonuçlarý temizle
        foreach (Transform child in resultListContainer) Destroy(child.gameObject);

        foreach (var habit in playerChoices)
        {
            GameObject item = Instantiate(resultItemPrefab, resultListContainer);
            TextMeshProUGUI itemText = item.GetComponentInChildren<TextMeshProUGUI>();

            if (habit.isGood)
            {
                score += 20; // Her doðru 20 puan (5 soru * 20 = 100)
                itemText.text = $"<color=green>DOÐRU:</color> {habit.habitName}";
            }
            else
            {
                // Yanlýþsa açýklamasýný ekle
                itemText.text = $"<color=red>YANLIÞ:</color> {habit.habitName}\n<size=80%><i>Nedeni: {habit.explanationIfWrong}</i></size>";
            }
        }

        finalScoreText.text = "Toplam Puan: " + score;
    }
}