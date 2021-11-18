using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData : MonoBehaviour
{
    private Skill activeSkill;
    public void SetActiveSkill(Skill skill)
    {
        activeSkill = skill;
    }
    public Skill GetActiveSkill()
    {
        return activeSkill;
    }
}
