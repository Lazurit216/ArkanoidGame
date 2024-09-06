using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    [SerializeField] private string defText;
    private LevelManager levelManager;
    private int curLevel;
    private void Start()
    {
        levelManager = LevelManager.Instance;
        textMeshPro = GetComponent<TextMeshProUGUI>();
        levelManager.onNextLevel += ChangeText;

        curLevel = 0;
        ChangeText();
    }
    private void ChangeText()
    {
        curLevel += 1;
        textMeshPro.text = defText + curLevel.ToString();
    }
}
