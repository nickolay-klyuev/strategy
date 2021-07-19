using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    private ResourceSystem resourceSystemScript;

    // Start is called before the first frame update
    void Start()
    {
        resourceSystemScript = GameObject.FindGameObjectWithTag("ResourceSystem").GetComponent<ResourceSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = $"Resources: {resourceSystemScript.GetResourceAmount().ToString()};";
    }
}
