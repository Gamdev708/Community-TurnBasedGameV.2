using System.Collections;
using System.Collections.Generic;
using TBRPG.Combat;
using TBRPG.Control;
using TMPro;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [Header("Configurations"), Space]
    [SerializeField] GameController GameController;

    [Header("Battle UI"), Space]
    [SerializeField] GameObject BattleUIPanel;
    [SerializeField] TextMeshPro UIPanelText;
    [SerializeField] GameObject ActionButtonPrefab;
    [SerializeField] GameObject ActionButtonsPanel;
    [SerializeField] GameObject TeamButtonsPanel;
    [SerializeField] GameObject SkillButtonsPanel;
    [SerializeField] GameObject TargetButtonsPanel;


    [Header("Team Status UI"),Space]
    [SerializeField] GameObject StatusBar;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnableStatusBar(bool enabled)
    {
        StatusBar.SetActive(enabled);
    }
    public void EnableBattleUIPanel(bool enabled)
    {
        BattleUIPanel.SetActive(enabled);
    }

    public void GenerateSkillButtons(CharacterConfig character)
    {

    } 

    //public void GenerateTeamButtons()
    //{

    //}
}
