using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsSystem : MonoBehaviour
{
    [SerializeField] private GameObject chooseSkillBlock;

    private List<Skill> skills = new List<Skill>();
    class SkillBlock
    {
        public bool isUsed;
        public GameObject block;

        public SkillBlock(GameObject newBlock)
        {
            isUsed = false;
            block = newBlock;
        }
    }

    List<SkillBlock> skillBlocks = new List<SkillBlock>();

    void Start()
    {
        chooseSkillBlock.SetActive(false);

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! skills !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        skills.Add(new Skill("Battle Experience", "You will get more experience points for killing enemies. (+5%)", "BattleExperience"));
        skills.Add(new Skill("Old Material Gathering", "You will get back resources after your units destroyed. (25% of an unit cost)", "OldMaterialGathering"));
        skills.Add(new Skill("Die Hard", "Brave soldiers and machines don't want to die easily. Before die, your units will stay in battle for a bit longer. (3 sec extra live after loosing all health)", "DieHard"));
        skills.Add(new Skill("Vultures", "Gather resources from dead enemies. (5% of an unit cost)", "Vultures"));
        // TODO: figure out and add new skills, similar with those above

        skillBlocks.Add(new SkillBlock(chooseSkillBlock.transform.GetChild(1).gameObject));
        skillBlocks.Add(new SkillBlock(chooseSkillBlock.transform.GetChild(2).gameObject));
        skillBlocks.Add(new SkillBlock(chooseSkillBlock.transform.GetChild(3).gameObject));
    }

    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Skills !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public void ChooseSkill(int blockIndex) // called by pressing choose skill button
    {
        if (GlobalExpSystem.SpendSkillPoint())
        {
            Close();

            SkillBlock skillBlock = skillBlocks[blockIndex];
            skillBlock.isUsed = false;

            Skill activeSkill = skillBlock.block.GetComponent<SkillData>().GetActiveSkill();
            activeSkill.Use();

            // TODO: make choosing skills to affect on game 
            switch (activeSkill.GetMetaName())
            {
                case "BattleExperience":
                    GlobalExpSystem.extraExpPerc += .05f;
                    break;
                case "OldMaterialGathering":
                    DestroyScript.givingResourcesBack = .25f;
                    break;
                case "DieHard":
                    DestroyScript.isDieHard = true;
                    break;
                case "Vultures":
                    DestroyScript.gatherFromDeadEnemies = .05f;
                    break;
                default:
                    Debug.LogWarning("There are no such skill!!!!!!!!!!!!!!!");
                break;
            }
        }
    }

    public void OpenChooseSkillBlock()
    {
        AddSkillToChoose();
        Open();
    }

    public void Cancel()
    {
        Close();
    }

    private void AddSkillToChoose()
    {
        foreach (SkillBlock skillBlock in skillBlocks)
        {
            if (!skillBlock.isUsed)
            {
                skillBlock.isUsed = true;

                List<Skill> availableSkills = new List<Skill>(); // only skills that never been choosed before
                foreach (Skill skill in skills)
                {
                    if (!skill.GetIsUsed())
                    {
                        availableSkills.Add(skill);
                    }
                }

                if (availableSkills.Count > 0)
                {
                    Skill skillToShow = availableSkills[Random.Range(0, availableSkills.Count)];
                    skillToShow.Use();

                    skillBlock.block.GetComponent<SkillData>().SetActiveSkill(skillToShow);
                    skillBlock.block.transform.GetChild(0).GetComponent<Text>().text = skillToShow.GetName();
                    skillBlock.block.transform.GetChild(1).GetComponent<Text>().text = skillToShow.GetDesc();
                }
                else
                {
                    skillBlock.block.SetActive(false);
                }
            }
        }
    }

    private void Open()
    {
        Time.timeScale = 0;
        chooseSkillBlock.SetActive(true);
    }

    private void Close()
    {
        chooseSkillBlock.SetActive(false);
        Time.timeScale = 1;
    }
}
