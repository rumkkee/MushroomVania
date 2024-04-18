using UnityEngine;
using TMPro;

public class CoinManagement : MonoBehaviour
{
    public TMP_Text coinText;

    private int coinCount = 0;

    public void UpdateCoinText()
    {
        coinText.text = string.Format("-  {0:D4}", coinCount);
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateCoinText();
    }

    public void RemoveCoin()
    {
        if (coinCount > 0)
        {
            coinCount--;
            UpdateCoinText();
        }
    }
}
