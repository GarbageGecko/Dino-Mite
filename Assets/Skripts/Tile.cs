using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    private int _height;
    public Vector2 Position => transform.position;

    public void Init(bool isOffset, int height)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _height = height;
    }

    void OnMouseEnter()
    {
        if (IsTileAdjacentToPlayer() && _height == 0)
        {
            _highlight.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        if (_height == 0)
        {
            _highlight.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (IsTileAdjacentToPlayer())
        {
            GameManager.Instance.OnTileClicked(this);
        }
    }

    private bool IsTileAdjacentToPlayer()
    {
        Player player = GameManager.Instance.Player;
        if (player != null)
        {
            Vector2 playerPosition = player.CurrentTilePosition;
            float horizontalDistance = Mathf.Abs(playerPosition.x - transform.position.x);
            return horizontalDistance == 2 && transform.position.y == 0; // Adjacent horizontal tiles on the bottom row
        }
        return false;
    }
}
