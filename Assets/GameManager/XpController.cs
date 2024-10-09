using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class XpController : MonoBehaviour
{
    public GameManagerScript levelUp;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ExperienceText;
    public int Level;
    public float CurrentXp;
    public float TargetXp;
    public Image XpProgressBar;

    public void GetXP()
    {
        CurrentXp += 50;
    }
    void Update()
    {
        ExperienceText.text = CurrentXp + " / " + TargetXp;
        ExperienceController();
    }

    public void ExperienceController()
    {
        LevelText.text = "Level: " + Level.ToString();
        XpProgressBar.fillAmount = CurrentXp / TargetXp;

        while (CurrentXp >= TargetXp && levelUp.levelUpUI.activeSelf == false)
        {
            levelUp.LevelUp();
            CurrentXp -= TargetXp;
            Level++;
            TargetXp += 50;
        }
    }
}
