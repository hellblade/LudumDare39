using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField]
    RectTransform healthBar;
    [SerializeField]
    RectTransform PowerBar;

    [SerializeField]
    Text healthText;

    [SerializeField]
    Text PowerText;

    [SerializeField]
    Health healthToShow;

    [SerializeField]
    Power PowerToShow;

    float healthBarSize;
    Vector2 healthBarSizeDelta;

    float PowerBarSize;
    Vector2 PowerBarSizeDelta;


    void Start ()
    {
        healthBarSizeDelta = healthBar.sizeDelta;
        healthBarSize = healthBar.sizeDelta.x;

        PowerBarSizeDelta = PowerBar.sizeDelta;
        PowerBarSize = PowerBar.sizeDelta.x;
    }
	

	void Update ()
    {
        float hpScale = healthToShow.CurrentHealth / healthToShow.maxHealth;

        healthBarSizeDelta.x = hpScale * healthBarSize;
        healthBar.sizeDelta = healthBarSizeDelta;

        healthText.text = $"{(int)healthToShow.CurrentHealth} / {healthToShow.maxHealth} ";


        float PowerScale = PowerToShow.CurrentPower / PowerToShow.maxPower;

        PowerBarSizeDelta.x = PowerScale * PowerBarSize;
        PowerBar.sizeDelta = PowerBarSizeDelta;

        PowerText.text = $"{(int)PowerToShow.CurrentPower} / {PowerToShow.maxPower} ";
    }
}
