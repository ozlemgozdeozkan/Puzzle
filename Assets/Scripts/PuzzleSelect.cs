using UnityEngine;
using UnityEngine.UI;

public class PuzzleSelect : MonoBehaviour
{
    public GameObject _startPanel;

    public void SetPuzzlesPhoto(Image Photo)
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject.Find("Piece (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = Photo.sprite;
        }
        _startPanel.SetActive(false);
    }
}