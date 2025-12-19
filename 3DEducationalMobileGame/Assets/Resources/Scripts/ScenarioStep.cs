using UnityEngine;

[System.Serializable]
public class ScenarioStep
{
    public string stepTitle;

    // Önceki 'goodHabit' ve 'badHabit' yerine bunlarý kullanýyoruz
    // Böylece GameManager içindeki kod (step.habitOptionA) hata vermez.
    public HabitData habitOptionA;
    public HabitData habitOptionB;
}