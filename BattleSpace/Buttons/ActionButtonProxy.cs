using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;
using System.Linq;

public class ActionButtonProxy : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _optionsText;
    [SerializeField]
    private GameObject _optionsHolder;
    [SerializeField]
    private GameObject _content;
    [SerializeField]
    private GameObject _optionPrefab;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private BattleLogic BattleLogic;
    private List<ActionFactorySO> _type;

    private void Awake()
    {
        _optionsHolder.SetActive(false);
    }

    public void OnButtonClick()
    {
        if (HUD.LockedObject != null && PlayerState.WaitingForAction)
        {
            //Set Title
            _optionsHolder.SetActive(true);
            _optionsText.SetText(transform.GetChild(0).name);

            // Empty content befor
            foreach (Transform child in _content.transform)
                Destroy(child.gameObject);

            // Chose button to load by type
            PlayerUnit playerUnit = _player.GetComponent<PlayerUnit>();

            if (transform.GetChild(0).name == "Attack") _type = playerUnit.AttackOptions.Cast<ActionFactorySO>().ToList();
            if (transform.GetChild(0).name == "Defence") _type = playerUnit.DefenceOptions.Cast<ActionFactorySO>().ToList();
            if (transform.GetChild(0).name == "Skill") _type = playerUnit.SkillOptions.Cast<ActionFactorySO>().ToList();

            //Load array of options in HUD
            foreach (ActionFactorySO item in _type) { 

                GameObject optionInstance = Instantiate(_optionPrefab);
                optionInstance.transform.SetParent(_content.transform, false);

                TMP_Text nameText = optionInstance.transform.GetChild(0).GetComponent<TMP_Text>();
                nameText.SetText(item.Name);

                TMP_Text damageText = optionInstance.transform.GetChild(1).GetComponent<TMP_Text>();
                damageText.SetText(damageText.text + " - "+ item.Damage.ToString());

                // Adding inflict damage on option
                Button button = optionInstance.GetComponent<Button>();
                button.onClick.AddListener(() => InflictDamage(item));

            }
        }
    }
    private void InflictDamage(ActionFactorySO item)
    {
        EnemyUnit enemyUnit = HUD.LockedObject.GetComponent<EnemyUnit>();
        enemyUnit.TakeDamage(item.Damage);
        HUD.LockedObject = null;
        HUD.HUDholder.SetActive(false);
        _optionsHolder.SetActive(false);
        if (enemyUnit.UnitCurrentHealth <= 0)
        {
            BattleLogic.EnemyUnits.Remove(enemyUnit);
            InitBattleData.EnemyUnitsInitData.Remove(enemyUnit.stats);
            Destroy(enemyUnit.gameObject);
        }

        PlayerState.ActionEnded = true;
    }
}
