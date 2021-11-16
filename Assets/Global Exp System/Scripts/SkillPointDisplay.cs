using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointDisplay : MonoBehaviour
{
    private Text skillPointsText;
    private Vector3 initialUIPos;
    private float xOffSet = 50f;
    private float UISpeed = 300f;
    private float xHidePos;

    // Start is called before the first frame update
    void Start()
    {
        skillPointsText = transform.GetChild(0).GetComponent<Text>();
        initialUIPos = transform.position;

        xHidePos = initialUIPos.x + GetComponent<RectTransform>().sizeDelta.x + xOffSet;

        transform.position = new Vector3(xHidePos, initialUIPos.y, initialUIPos.z);
    }

    void Update()
    {
        int skillPointsAmount = GlobalExpSystem.GetSkillPoints();

        if (skillPointsAmount > 0 && transform.position.x > initialUIPos.x)
        {
            transform.Translate(Vector3.left * Time.deltaTime * UISpeed);
        }
        else if (skillPointsAmount == 0 && transform.position.x < xHidePos)
        {
            transform.Translate(Vector3.right * Time.deltaTime * UISpeed);
        }
    }

    void FixedUpdate()
    {
        skillPointsText.text = $"Skill points: {GlobalExpSystem.GetSkillPoints().ToString()}";
    }
}
