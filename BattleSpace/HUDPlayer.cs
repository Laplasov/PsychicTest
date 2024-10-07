using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerUnitScriptableObject stats;
    [SerializeField]
    private TMP_Text UnitName;
    [SerializeField]
    private TMP_Text UnitLevel;
    [SerializeField]
    private TMP_Text UnitAttack;
    [SerializeField]
    private TMP_Text UnitDefence;
    [SerializeField]
    private TMP_Text UnitSkillPoints;
    [SerializeField]
    private TMP_Text UnitCurrentHealth;
    [SerializeField]
    private Slider UnitSlider;
    public static Action onPlayerAttacked;

    private void Awake()
    {
        SetStats();
        onPlayerAttacked += SetStats;
    }

    private void SetStats()
    {
        UnitName.text = stats.UnitName;
        UnitLevel.text = "Level - " + stats.UnitLevel;
        UnitAttack.text = "ATK - " + stats.UnitAttack;
        UnitDefence.text = "DEF - " + stats.UnitDefence;
        UnitSkillPoints.text = "SP - " + stats.UnitSkillPoints;
        UnitCurrentHealth.text = "HP - " + stats.UnitHealth + " / " + stats.UnitMaxHealth;
        UnitSlider.maxValue = stats.UnitMaxHealth;
        UnitSlider.value = stats.UnitHealth;
    }
    private void OnDestroy()=> onPlayerAttacked -= SetStats;
}
