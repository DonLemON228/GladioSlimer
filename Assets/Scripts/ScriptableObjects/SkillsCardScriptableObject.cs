using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/RandomSkills", order = 1)]
public class SkillsCardScriptableObject : ScriptableObject
{
    public Sprite Bg;
    public Sprite Logo;
    public string Name;
    public string Discription;
}
