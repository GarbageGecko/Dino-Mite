using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    private int _height; // Add a variable to store the height
    public Vector2 Position => transform.position; // Add a property to get the tile's position

    // Modify the Init method to accept height
    public void Init(bool isOffset, int height)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _height = height; // Store the height
    }

    void OnMouseEnter()
    {
        if (_height == 0) // Only highlight if this is the bottom row
        {
            _highlight.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        if (_height == 0) // Only un-highlight if this is the bottom row
        {
            _highlight.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        GameManager.Instance.OnTileClicked(this);
    }
}
