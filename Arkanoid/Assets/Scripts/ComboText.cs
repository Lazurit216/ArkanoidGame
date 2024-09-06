using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboText : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    [SerializeField] private string defText;
    private Player player;
    private void Start()
    {
        player = Player.Instance;
        textMeshPro = GetComponent<TextMeshProUGUI>();
        player.onComboText += ChangeText;
        ChangeText(1);
    }
    private void ChangeText(int bonus)
    {
        textMeshPro.text = defText + bonus.ToString();
    }
}
