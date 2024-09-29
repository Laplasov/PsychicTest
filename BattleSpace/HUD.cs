using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class HUD : MonoBehaviour
{
    public static GameObject HUDholder;
    private static GameObject _hoveredObject;

    [HideInInspector]
    public static GameObject LockedObject;

    [SerializeField]
    private TMP_Text UnitName;
    [SerializeField]
    private TMP_Text Loyalty;
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
    [SerializeField]
    private Light _light;

    public static UnityEvent<EnemyUnit> onObjectHovered = new UnityEvent<EnemyUnit>();
    public static UnityEvent<EnemyUnit> onObjectDown = new UnityEvent<EnemyUnit>();
    public static Action onObjectExit;

    void Start()
    {
        onObjectHovered.AddListener(OnObjectHovered);
        onObjectDown.AddListener(OnObjectDown);
        onObjectExit = OnObjectExit;
        HUDholder = this.gameObject.transform.GetChild(0).gameObject;
        HUDholder.SetActive(false);
        LockedObject = null;
    }
    public void OnObjectHovered(EnemyUnit enemyUnit)
    {
        if (LockedObject == null) {
            HUDholder.SetActive(true);
            SetStats(enemyUnit);
        }
    }
    public void OnObjectExit()
    {
        if (LockedObject == null)
            HUDholder.SetActive(false);
    }
    public void OnObjectDown(EnemyUnit enemyUnit)
    {
        if (PlayerState.IsPlayerState == true)
        {
            LockedObject = enemyUnit.gameObject;
            if (LockedObject != null)
                SetStats(enemyUnit);
        }
    }
    public void SetStats(EnemyUnit enemyUnit)
    {
        UnitName.text = enemyUnit.UnitName;
        UnitLevel.text = "Level - " + enemyUnit.UnitLevel;
        UnitAttack.text = "ATK - " + enemyUnit.UnitAttack;
        UnitDefence.text = "DEF - " + enemyUnit.UnitDefence;
        UnitSkillPoints.text = "SP - " + enemyUnit.UnitSkillPoints;
        UnitCurrentHealth.text = "HP - " + enemyUnit.UnitCurrentHealth + " / " + enemyUnit.UnitMaxHealth;
        _light.transform.position = enemyUnit.transform.position;
        UnitSlider.maxValue = enemyUnit.UnitMaxHealth;
        UnitSlider.value = enemyUnit.UnitCurrentHealth;
    }
}
