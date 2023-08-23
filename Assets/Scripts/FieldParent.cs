using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FieldParent : MonoBehaviour
{
    public static FieldParent Instance { get; private set; }

    [SerializeField] private Vector2Int _puzzleLength = Vector2Int.zero;

    private List<Field> _fields = new();

    private void Awake()
    {
        Instance = this;
        GiveIndex();
    }

    private void GiveIndex()
    {
        _fields.AddRange(GetComponentsInChildren<Field>());

        //x = 3 , y = 3

        // [] [] []
        // [] [] []
        // [] [] []


        for (int i = 0; i < _puzzleLength.x * _puzzleLength.y; i++)
            _fields[i].Index = i;
    }

    public Field GetField(Pieces _pieces, PiecesEnum _enum)
    {
        int _piecesIndex = GetPiecesIndex(_pieces);

        return _enum switch
        {
            PiecesEnum.Top => _fields[_piecesIndex - _puzzleLength.x],
            PiecesEnum.Bottom => _fields[_piecesIndex + _puzzleLength.x],
            PiecesEnum.Right => _fields[_piecesIndex + 1],
            PiecesEnum.Left => _fields[_piecesIndex - 1],
            _ => null,
        };

        /*switch (_enum)
        {
            case PiecesEnum.Top: return _fields[GetPiecesIndex(_pieces) - _puzzleLength.x];
            case PiecesEnum.Bottom: return _fields[GetPiecesIndex(_pieces) + _puzzleLength.x];
            case PiecesEnum.Right: return _fields[GetPiecesIndex(_pieces) + 1];
            case PiecesEnum.Left: return _fields[GetPiecesIndex(_pieces) - 1];
        }

        return null;*/
    }

    private int GetPiecesIndex(Pieces _pieces)
    {
        foreach (Field field in _fields)
        {
            if (field.CurrentPiece == null)
                continue;

            if (!field.CurrentPiece.Equals(_pieces))
                continue;

            return _fields.IndexOf(field);
        }
        return 0;
        //return _fields.IndexOf(_fields.Find(x => x.CurrentPiece.Equals(_pieces)));
    }
}