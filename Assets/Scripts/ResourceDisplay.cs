using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    public GameObject resourceSystem;
    private ResourceSystem resourceSystemScript;

    // Start is called before the first frame update
    void Start()
    {
        resourceSystemScript = resourceSystem.GetComponent<ResourceSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = resourceSystemScript.GetResourceAmount().ToString();
    }
}
