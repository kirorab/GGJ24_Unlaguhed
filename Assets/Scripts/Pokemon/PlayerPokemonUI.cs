using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPokemonUI : PokemonUI
{
    public GameObject[] SkillButtons = new GameObject[3];
    public List<SkillMaterial> SkillMaterials;
    public PokemonInfo blank;
    public Turtle t;
    protected override void Awake()
    {
        base.Awake();
        
        EventSystem.Instance.AddListener<int>(EEvent.OnKoopaTurnStart, HandlePlayerTurnStart);
        EventSystem.Instance.AddListener(EEvent.OnPikachuTurnStart, HandlePlayerTurnEnd);
        HandlePokemonBlank(t.TurtleState == ETurtleState.Follow);
    }

    public void HandlePokemonBlank(bool isForgive)
    {
        if (!isForgive)
        {
            foreach (var button in SkillButtons)
            {
                DrawButton(button, SkillMaterials[SkillMaterials.Count - 1]);
                
            }
            _PokemonInfo = blank;
            base.Init();
        }
        
    }
    
    public void HandlePlayerTurnStart(int energy)
    {
        EnableSkill();
        JudgeEnergy(energy);
    }
    
    public void HandlePlayerTurnEnd()
    {
        DisableSkill();
    }
    
    public override void JudgeEnergy(int cur)
    {
        if (cur < 2)
        {
            HandleButtonAble(SkillButtons[2], false);
            if (cur < 1)
            {
                HandleButtonAble(SkillButtons[1], false);
            }
        }
        
    }

    public override void EnableSkill()
    {
        for (int i = 0; i < 3; i++)
        {
            HandleButtonAble(SkillButtons[i], true);
        }
    }
    
    public override void DisableSkill()
    {
        for (int i = 0; i < 3; i++)
        {
            HandleButtonAble(SkillButtons[i], false);
        }
    }
    
    public override void Init()
    {
        base.Init();
        Debug.Log("player init");
        for (int i = 0; i < 3; i++)
        {
            DrawButton(SkillButtons[i], SkillMaterials[i]);
        }
        
    }


    private void HandleButtonAble(GameObject g, bool b)
    {
        var childIcon = g.transform.Find("SkillIcon");
        childIcon.GetComponent<Button>().enabled = b;
        var color = childIcon.GetComponent<Button>().image.color;
        color.a = b ? 1 : 0.5f;
    }
    
    
    
    private void DrawButton(GameObject b, SkillMaterial m)
    {
        var childIcon = b.transform.Find("SkillIcon");
        childIcon.GetComponent<Image>().sprite = m.icon;
        var childDescription = b.transform.Find("Description");
        Debug.Log(childDescription.name + " " + childDescription.GetComponentInChildren<TMP_Text>().gameObject.name);
        childDescription.GetComponentInChildren<TMP_Text>().text = m.description;
    }
}
