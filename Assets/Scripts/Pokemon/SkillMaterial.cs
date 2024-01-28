using UnityEngine;


[CreateAssetMenu(fileName = "skillMaterial", menuName = "Pokemon/SkillMaterial", order = 0)]
public class SkillMaterial : ScriptableObject
{
    public Sprite icon;
    [TextArea] public string description;
}
