using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDescroptionUI : MonoBehaviour
{
    [SerializeField] GameObject _unit;
    [SerializeField] DescType _type;

    private UnitProperties _unitProperties;
    private Text _description;

    enum DescType
    {
        Title,
        Description,
        Cost
    }

    // Start is called before the first frame update
    void Start()
    {
        _unitProperties = _unit.transform.GetChild(0).GetComponent<UnitProperties>();
        if (_unitProperties == null)
        {
            _unitProperties = _unit.GetComponent<UnitProperties>();
        }

        _description = GetComponent<Text>();

        if (_type == DescType.Title)
        {
            _description.text = _unitProperties.GetUnitName();
        }
        else if (_type == DescType.Description)
        {
            _description.text = _unitProperties.GetUnitDesc();
        }
        else if (_type == DescType.Cost)
        {
            _description.text = _unitProperties.cost.ToString();
        }
    }
}
