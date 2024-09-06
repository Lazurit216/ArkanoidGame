using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPtext : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    [SerializeField] private string defText;
    private Player player;
    private void Start()
    {
        player = Player.Instance;
        textMeshPro = GetComponent<TextMeshProUGUI>();
        player.onHPchange += ChangeText;
        ChangeText();
    }
    private void ChangeText()
    {
        textMeshPro.text = defText + player.factlifes.ToString();
    }
}
