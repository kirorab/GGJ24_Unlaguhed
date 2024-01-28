using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPokemonUI : PokemonUI
{
    public Button[] SkillButtons = new Button[3];
    public List<SkillMaterial> SkillMaterials;
    private bool isBlank;

    public void Init(String nameText, Sprite s, int maxEnergy)
    {
        base.Init(nameText, s, maxEnergy);
        if (isBlank)
        {
            foreach (var button in SkillButtons)
            {
                DrawButton(button, SkillMaterials[4]);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                DrawButton(SkillButtons[i], SkillMaterials[i]);
            }
        }
    }

    private void DrawButton(Button b, SkillMaterial m)
    {
        b.GetComponentInChildren<TMP_Text>().text = m.description;
        b.image.sprite = m.icon;
    }
}
