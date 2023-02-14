using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _coinText, _livesText;
    [SerializeField] private Text _endText;

    private void Start()
    {
        _endText.gameObject.SetActive(false);
    }
    public void UpdateCoinDisplay(int coins)
    {
        _coinText.text = "Coins: " + coins.ToString();
    }

    public void UpdateLivesDisplay(int lives)
    {
        _livesText.text = "Lives: " + lives.ToString();
    }

    public void UpdateEndTextDisplay()
    {
        _endText.gameObject.SetActive(true);
    }
}
