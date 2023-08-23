using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Field : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Pieces _currentPiece = null;
    public Pieces CurrentPiece { get => _currentPiece; set => _currentPiece = value; }

    public bool IsEmpty => _currentPiece == null;

    private int _index = 0;
    public int Index { get => _index; set => _index = value; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PuzzleControl.Instance.CurrentField = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PuzzleControl.Instance.CurrentField = null;
    }

    public void PutPiece(Pieces _pieces)
    {
        //_currentPiece = _pieces;
        CurrentPiece = _pieces;
        _pieces.OnPutRight();

        _pieces.CurrentField = this;

        //_pieces.transform.SetParent(transform);
        _pieces.transform.position = transform.position;
        _pieces.DefaultPosition = transform.position;

        //GetComponent<Image>().color = Index.Equals(CurrentPiece.TargetIndex) ? Color.green : Color.red;
    }
}