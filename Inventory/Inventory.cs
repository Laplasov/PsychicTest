using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject _tapButton;
    [SerializeField]
    private TMP_Text _optionsText;
    [SerializeField]
    private GameObject _content;
    [SerializeField]
    private GameObject _optionPrefab;
    [SerializeField]
    private GameObject _HUDPlayer;
    private List<ActionFactorySO> _actionList;
    [SerializeField]
    private List<AttackItemsSO> _attackList;
    [SerializeField]
    private List<DefenceItemsSO> _defenceList;
    [SerializeField]
    private List<SkillsSO> _skillsList;

    private string _optionsTextHolder;

    enum ActionEnum { items, shields, skills }
    ActionEnum actionEnum;
    
    private void Awake()
    {
        _tapButton.SetActive(false);
        _optionsTextHolder = _optionsText.text;
    }
    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Tab))
            {
            _tapButton.SetActive(true);
            _HUDPlayer.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
            {
            ClearContent();
            _tapButton.SetActive(false);
            _HUDPlayer.SetActive(false);
            _optionsText.SetText(_optionsTextHolder);
            }


    }
    public void AddItem(ActionFactorySO actionFactory)
    {
        switch (actionFactory)
        {
            case AttackItemsSO attackItem:
                _attackList.Add((AttackItemsSO)actionFactory);
                OnButtonClickItems();
                //OnButtonClickItems();
                break;
            case DefenceItemsSO defenceItem:
                _defenceList.Add((DefenceItemsSO)actionFactory);
                OnButtonClickShields();
                OnButtonClickShields();

                break;
            case SkillsSO defenceItem:
                _skillsList.Add((SkillsSO)actionFactory);
                OnButtonClickSkills();
                OnButtonClickSkills();
                break;
        }
        Debug.Log(actionFactory);
    }
    public void RemoveItem(ActionFactorySO item, List<ActionFactorySO> _actionList)
    {
        if (item == null) return;
        _actionList.Remove(item);
        switch (actionEnum)
        {
            case ActionEnum.items:
                _attackList.Remove((AttackItemsSO)item);
                item = null;
                break;
            case ActionEnum.shields:
                _defenceList.Remove((DefenceItemsSO)item);
                item = null;
                break;
            case ActionEnum.skills:
                _skillsList.Remove((SkillsSO)item);
                item = null;
                break;
        }

        OnButtonClick(_actionList);
    }
    public void OnButtonClickItems()
    {
        actionEnum = ActionEnum.items;
        _optionsText.SetText("Items");
        OnButtonClick(_attackList.Cast<ActionFactorySO>().ToList());
    }
    public void OnButtonClickShields()
    {
        actionEnum = ActionEnum.shields;
        _optionsText.SetText("Shields");
        OnButtonClick(_defenceList.Cast<ActionFactorySO>().ToList());
    }
    public void OnButtonClickSkills()
    {
        actionEnum = ActionEnum.skills;
        _optionsText.SetText("Skills");
        OnButtonClick(_skillsList.Cast<ActionFactorySO>().ToList());
    }

    public void OnButtonClick(List<ActionFactorySO> _actionList)
    {
        ClearContent();

        foreach (ActionFactorySO item in _actionList)
        {
            ActionFactorySO currentItem = item;
            GameObject optionInstance = Instantiate(_optionPrefab);
            optionInstance.transform.SetParent(_content.transform, false);

            TMP_Text nameText = optionInstance.transform.GetChild(0).GetComponent<TMP_Text>();
            nameText.SetText(item.Name);

            TMP_Text damageText = optionInstance.transform.GetChild(1).GetComponent<TMP_Text>();
            damageText.SetText(damageText.text + " - " + item.Damage.ToString());

            Button button = optionInstance.transform.GetChild(2).GetComponent<Button>();
            if (item.Destructible) 
                button.onClick.AddListener(() => RemoveItem(item, _actionList));
            if (!item.Destructible) 
                button.transform.gameObject.SetActive(false);
        }
    }
    void ClearContent() 
    {
        foreach (Transform child in _content.transform)
            Destroy(child.gameObject);
    }
    public void LoadActionsToBattleInit()
    {
        InitBattleData.AttackItemsSO = _attackList;
        InitBattleData.DefenceItemsSO = _defenceList;
        InitBattleData.SkillsSO = _skillsList;
    }
}