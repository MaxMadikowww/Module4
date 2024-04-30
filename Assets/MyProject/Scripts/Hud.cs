using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private Image playerBar;
    [SerializeField] private Health playerHealth;
    private void Update()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        playerBar.fillAmount = playerHealth.currentHealth / 100;
    }
}
