using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Player;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    [SerializeField] private string defText;
    [SerializeField] private float plusTime = 1;
    private Player player;
    private void Start()
    {
        player = Player.Instance;
        textMeshPro = GetComponent<TextMeshProUGUI>();
        player.onGetScore += ChangeText;
        textMeshPro.text = defText + 0.ToString();
    }
    private void ChangeText(int scorePlus, int factScore)
    {
        StartCoroutine(ShowPlus(scorePlus, factScore));
    }
    private IEnumerator ShowPlus(int scorePlus, int factScore)
    {
        textMeshPro.text="+"+scorePlus.ToString();
        yield return new WaitForSeconds(plusTime);
        textMeshPro.text = defText + factScore.ToString();
    }
}

