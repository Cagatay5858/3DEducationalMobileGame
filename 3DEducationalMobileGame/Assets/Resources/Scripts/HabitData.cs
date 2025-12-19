using UnityEngine;

[CreateAssetMenu(menuName = "Educational/Habit")]
public class HabitData : ScriptableObject
{
    public string habitName;
    public bool isGood;
    [TextArea] public string explanationIfWrong; // Yanlýþsa neden yanlýþ olduðu

    // ScriptableObject sahne objesi tutamaz, o yüzden string ID kullanýyoruz.
    // Örn: "MutfakLavabo", "YatakOdasiYatak"
    public string locationID;
}