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
        skills.Add(new Skill("Improved Learning", "You will get more experience points. (+5%)", "ImprovedLearning"));
        // TODO: figure out and add new skills, similar with that one above

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
                default:
                    Debug.Log("Choose a skill");
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
