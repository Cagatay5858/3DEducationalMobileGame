using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public Scenario scenario;
    public float interactionDistance = 2.0f; // Hedefe ne kadar yaklaþýnca iþlem tamamlansýn?

    [Header("References")]
    public UIManager uiManager;
    public GuidanceSystem guidanceArrow;
    public Transform playerTransform;

    // State Variables
    private int currentStepIndex = 0;
    private List<HabitData> playerChoices = new List<HabitData>();
    private HabitData currentTargetHabit;
    private Transform currentTargetTransform;
    private bool isNavigating = false;

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        currentStepIndex = 0;
        playerChoices.Clear();
        ShowCurrentStepChoices();
    }

    void ShowCurrentStepChoices()
    {
        if (currentStepIndex >= scenario.steps.Count)
        {
            EndGame();
            return;
        }

        ScenarioStep step = scenario.steps[currentStepIndex];

        // UI Manager'a hangi alýþkanlýklarý göstereceðini ve seçilince ne yapacaðýný söylüyoruz
        uiManager.ShowChoices(step.stepTitle, step.habitOptionA, step.habitOptionB, OnHabitSelected);
    }

    // Oyuncu butona bastýðýnda çalýþýr
    void OnHabitSelected(HabitData selectedHabit)
    {
        playerChoices.Add(selectedHabit); // Seçimi kaydet
        currentTargetHabit = selectedHabit;

        // LocationManager'dan transformu bul
        currentTargetTransform = LocationManager.Instance.GetLocation(selectedHabit.locationID);

        if (currentTargetTransform != null)
        {
            guidanceArrow.SetTarget(currentTargetTransform);
            isNavigating = true;
        }
        else
        {
            Debug.LogError("Lokasyon bulunamadý: " + selectedHabit.locationID);
            CompleteAction(); // Lokasyon yoksa direkt geç
        }
    }

    void Update()
    {
        // Oyuncu hedefe ulaþtý mý kontrolü
        if (isNavigating && currentTargetTransform != null)
        {
            float distance = Vector3.Distance(playerTransform.position, currentTargetTransform.position);

            if (distance <= interactionDistance)
            {
                // Hedefe ulaþtý
                CompleteAction();
            }
        }
    }

    public void CompleteAction()
    {
        isNavigating = false;
        guidanceArrow.StopGuidance();

        Debug.Log("Eylem tamamlandý: " + currentTargetHabit.habitName);

        // Bir sonraki adýma geç
        currentStepIndex++;
        ShowCurrentStepChoices();
    }

    void EndGame()
    {
        uiManager.ShowFinalResults(playerChoices);
    }
}