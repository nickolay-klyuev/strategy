using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarScript : MonoBehaviour
{
    [SerializeField] private GameObject _fogPartPrefab;
    [SerializeField] private Vector2Int _fogSize;

    private float _partSizeX;
    private float _partSizeY;
    private Vector2 _position;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D box = _fogPartPrefab.GetComponent<BoxCollider2D>();
        _partSizeX = box.size.x;
        _partSizeY = box.size.y;

        _position = Vector2.zero;

        Transform line = new GameObject("FogLine").transform;
        line.SetParent(transform);

        _position.x += _partSizeX / 2;
        _position.y += _partSizeY / 2;
        Instantiate(_fogPartPrefab, _position, Quaternion.identity).transform.SetParent(line);

        for (int i = 1; i < _fogSize.x; i++)
        {
            _position.x += _partSizeX;
            Transform part = Instantiate(_fogPartPrefab, _position, Quaternion.identity).transform;
            part.SetParent(line);
        }

        _position.x = line.position.x;
        _position.y = line.position.y;

        for (int i = 1; i < _fogSize.y; i++)
        {
            _position.y += _partSizeY;
            Transform newLine = Instantiate(line, _position, Quaternion.identity);
            newLine.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
