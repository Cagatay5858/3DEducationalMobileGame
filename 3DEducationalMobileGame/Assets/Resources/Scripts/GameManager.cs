using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public Scenario scenario;
    public float interactionDistance = 3.0f;

    [Header("References")]
    public UIManager uiManager;

    // DÝKKAT: Burasý artýk GuidanceSystem deðil, Arrow(1)'deki script olan GuidanceArrow olacak
    public GuidanceArrow guidanceArrow;

    public Transform playerTransform;

    // State Variables
    private int currentStepIndex = 0;
    private List<HabitData> playerChoices = new List<HabitData>();
    private HabitData currentTargetHabit;
    private Transform currentTargetTransform;
    private bool isNavigating = false;
    private bool canCompleteAction = false;

    void Start()
    {
        // UI Action button setup...
        // (Eðer UIManager singleton yaptýysan buradaki listener'a gerek kalmayabilir, 
        // ama eski yapýn duruyorsa kalsýn)
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
        uiManager.ShowChoices(step.stepTitle, step.habitOptionA, step.habitOptionB, OnHabitSelected);
    }

    // --- EN ÖNEMLÝ KISIM BURASI ---
    void OnHabitSelected(HabitData selectedHabit)
    {
        playerChoices.Add(selectedHabit);
        currentTargetHabit = selectedHabit;

        // 1. Seçilen alýþkanlýðýn ID'sini kullanarak LocationManager'dan gerçek Transformu al
        currentTargetTransform = LocationManager.Instance.GetLocation(selectedHabit.locationID);

        if (currentTargetTransform != null)
        {
            // 2. Bulunan hedefi Arrow(1) üzerindeki GuidanceArrow scriptine gönder
            guidanceArrow.SetTarget(currentTargetTransform);

            isNavigating = true;
            canCompleteAction = false;
        }
        else
        {
            Debug.LogError("HATA: LocationManager'da bu ID bulunamadý: " + selectedHabit.locationID);
            // Hedef yoksa direkt tamamla ki oyun takýlmasýn
            CompleteAction();
        }
    }

    void Update()
    {
        if (isNavigating && currentTargetTransform != null)
        {
            // Mesafeyi kontrol et
            float distance = Vector3.Distance(
                new Vector3(playerTransform.position.x, 0, playerTransform.position.z),
                new Vector3(currentTargetTransform.position.x, 0, currentTargetTransform.position.z)
            );

            if (distance <= interactionDistance)
            {
                if (!canCompleteAction)
                {
                    canCompleteAction = true;
                    // Hedefe varýldý, butonu göster
                    UIManager.Instance.ShowActionButton("Görevi Yap", CompleteAction);
                }
            }
            else
            {
                if (canCompleteAction)
                {
                    canCompleteAction = false;
                    UIManager.Instance.HideActionButton();
                }
            }
        }
    }

    public void CompleteAction()
    {
        isNavigating = false;
        canCompleteAction = false;

        UIManager.Instance.HideActionButton();

        // Görev bitti, oku gizle
        guidanceArrow.StopGuidance();

        Debug.Log("Eylem tamamlandý: " + currentTargetHabit.habitName);

        currentStepIndex++;
        ShowCurrentStepChoices();
    }

    void EndGame()
    {
        uiManager.ShowFinalResults(playerChoices);
    }
}