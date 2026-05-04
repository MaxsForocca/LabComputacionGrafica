using UnityEngine;
using TMPro;

public class Contadores : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        GameManager.Instance.OnEnemyCountChanged += UpdateText;
    }

    void UpdateText(int defeated, int total)
    {
        text.text = defeated + " / " + total;
    }
}