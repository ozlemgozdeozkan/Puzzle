using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class Pieces : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private PiecesData[] _datas = null;

    private List<Pieces> _rightPieces = new();
    private Field _currentField = null;

    private Vector3 _defaultPosition = Vector3.zero;
    private Canvas _canvas = null;

    private int _targetIndex = 0;
    private bool _isPutRight = false;

    public static event System.Action OnPutAction = null;

    #region Encapsulations
    public PiecesData[] Datas => _datas;

    public Field CurrentField { get => _currentField; set => _currentField = value; }

    public Vector3 DefaultPosition { get => _defaultPosition; set => _defaultPosition = value; }

    public int TargetIndex { get => _targetIndex; set => _targetIndex = value; }

    public List<Pieces> RightPieces => _rightPieces;
    #endregion

    private void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
    }

    private void Update()
    {
        /*if (_follow)
        {
            PuzzleControl.Instance.parcamiz = transform.GetComponent<Image>();
            PuzzleControl.Instance.parcamiz.raycastTarget = false;
            transform.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;

            if (Input.GetButtonUp("Fire1"))
            {
                transform.GetComponent<Image>().raycastTarget = true;
                _follow = false;
            }
        }
        else
        {
            transform.GetComponent<Image>().raycastTarget = true;
        }*/
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!CurrentField)
            DefaultPosition = transform.position;
        else
        {
            CurrentField.CurrentPiece = null;

            CurrentField = null;
        }

        CallNeighbors();
        GetComponent<Image>().raycastTarget = false;

        PuzzleControl.Instance.CurrentPieces = this;

        transform.SetParent(_canvas.transform);

        transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        OnPutAction?.Invoke();

        if (PuzzleControl.Instance.PutPieceToField()) //When put any field
            return;

        transform.position = DefaultPosition;
    }

    public void OnPutRight(bool _isCalledOthers = false)
    {
        bool[] _checkList = PuzzleControl.Instance.CheckIsRight(this, _datas, out _rightPieces, _isCalledOthers: _isCalledOthers);
        foreach (Pieces pieces in _rightPieces)
        {
            pieces.transform.SetParent(transform);
        }

        int a = 0;

        for (int i = 0; i < _checkList.Length; i++)
            if (_checkList[i])
                a++;

        _isPutRight = a.Equals(_checkList.Length);

        Image _image = GetComponent<Image>();
        _image.color = _isPutRight ? Color.green : Color.red;
    }

    private void CallNeighbors()
    {
        if (_rightPieces.Count.Equals(0))
            return;

        for (int i = 0; i < _rightPieces.Count; i++)
            _rightPieces[i].OnPutRight(_isCalledOthers: true);
    }


    public bool GetFieldByPiecesDirection(out Field _field)
    {
        if (GetEnumByRightPieces(out PiecesEnum _enum))
        {
            Debug.Log(_enum);
            _field = FieldParent.Instance.GetField(this, _enum);
            return true;
        }
        _field = default;
        return false;
    }

    private bool GetEnumByRightPieces(out PiecesEnum _enum)
    {
        foreach (Pieces pieces in _rightPieces)
            if (GetEnumByPieces(pieces, out _enum))
                return true;

        _enum = default;
        return false;
    }

    private bool GetEnumByPieces(Pieces _pieces, out PiecesEnum _enum)
    {
        for (int i = 0; i < _datas.Length; i++)
        {
            Debug.Log("Data Target: " + Datas[i].TargetPieces.Datas[i].TargetPieces.name);
            Debug.Log("Gave Pieces: " + _pieces.Datas[i].TargetPieces.name);

            if (Datas[i].TargetPieces.Datas[i].TargetPieces.name.Equals(_pieces.Datas[i].TargetPieces.name))
            {
                Debug.Log(i);
                _enum = Datas[i].Enum;
                return true;
            }
        }

        _enum = default;
        return false;
    }
}

[System.Serializable]
public class PiecesData
{
    [SerializeField] private PiecesEnum _checkEnum = PiecesEnum.Top;

    [SerializeField] private Pieces _targetPieces = null;

    public PiecesEnum Enum => _checkEnum;
    public Pieces TargetPieces => _targetPieces;

    //public Pieces TargetPieces { get => _targetPieces; }
    // public Pieces TargetPieces { get { return _targetPieces; } }
    /*public Pieces TargetPieces
    {
        get
        {
            return _targetPieces;
        }
    }*/


}

public enum PiecesEnum
{
    Top,
    Bottom,
    Right,
    Left
}