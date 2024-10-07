using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ChestScript : MonoBehaviour, IInteracrable, IPopUp
{
    [SerializeField]
    private List<ActionFactorySO> _items;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _stash;
    [SerializeField]
    private GameObject _optionPrefab;
    [SerializeField]
    private GameObject _eButton;
    [SerializeField]
    private GameObject _content;
    private bool isInteracting = false;

    private void Awake() => Light();
    public string PopUpText() => "E";

    public void InteractWithTrigger()=> _player.GetComponent<NewPlayer>().OpenStash += StashHandler;
    
    private void StashHandler()
    {
        if (isInteracting) return;

        _stash.transform.GetChild(1).GetComponent<Button>().
            onClick.AddListener(() => _stash.SetActive(false));

        isInteracting = true;
        ClearContent();

        _stash.SetActive(true);
        List<ActionFactorySO> itemsCopy = new List<ActionFactorySO>();

        foreach (ActionFactorySO item in _items)
        {
            ActionFactorySO currentItem = item;
            GameObject optionInstance = Instantiate(_optionPrefab);
            optionInstance.transform.SetParent(_content.transform, false);

            TMP_Text nameText = optionInstance.transform.GetChild(0).GetComponent<TMP_Text>();
            nameText.SetText(item.Name);

            TMP_Text damageText = optionInstance.transform.GetChild(1).GetComponent<TMP_Text>();
            damageText.SetText(damageText.text + " - " + item.Damage.ToString());

            Button buttonRemove = optionInstance.transform.GetChild(2).GetComponent<Button>();
            if (item.Destructible)
                buttonRemove.onClick.AddListener(() => {
                    RemoveItem(item);
                    StashHandler();
                });
            if (!item.Destructible)
                buttonRemove.transform.gameObject.SetActive(false);

            Button buttonAdd = optionInstance.transform.GetChild(3).GetComponent<Button>();
            buttonAdd.onClick.AddListener(() => 
            {
                _player.GetComponent<Inventory>().AddItem(item);
                RemoveItem(item);
                StashHandler();
            });
        }
        Light();
        isInteracting = false;
    }
    public void RemoveItem(ActionFactorySO item)=> _items.Remove(item);
    
    public void InteractWithTriggerStay() { }
    public void InteractWithTriggerExit()
    {
        _player.GetComponent<NewPlayer>().OpenStash -= StashHandler;
        isInteracting = false;
        _stash.SetActive(false);
    }
    public void InteractWithCollision() { }
    private void Light() 
    {
        if (_items.Count > 0) 
            transform?.GetChild(0).gameObject.SetActive(true); 

        if (_items.Count <= 0) 
            transform?.GetChild(0).gameObject.SetActive(false); 
    }
    void ClearContent()
    {
        foreach (Transform child in _content.transform)
            Destroy(child.gameObject);
    }
}