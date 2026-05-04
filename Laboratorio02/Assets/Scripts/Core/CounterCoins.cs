using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CounterCoins : MonoBehaviour
{
    
    public UIManager currentCoins;
    private TMP_Text tmp_text;
    void Start()
    {
        tmp_text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        tmp_text.text = currentCoins.coinsText.text;
    }
}