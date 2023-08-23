using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleControl : MonoBehaviour
{
    public static PuzzleControl Instance { get; private set; }

    private Field _field = null;
    private Pieces _pieces = null;


    public Field CurrentField { get => _field; set => _field = value; }
    public Pieces CurrentPieces { get => _pieces; set => _pieces = value; }


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //Obje.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition); clickevent pieces
        //
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonUp("Fire1"))
        //{
        //    if (field != null && parcamiz != null && Mousecekildi)
        //    {
        //        parcamiz.rectTransform.anchoredPosition = field.transform.GetComponent<RectTransform>().anchoredPosition;
        //        field = null;
        //        parcamiz = null;
        //        Mousecekildi = false;
        //        print("birlesti");
        //    }
        //}
    }
    public bool PutPieceToField() => PutPieceToField(CurrentField, CurrentPieces);
    public bool PutPieceToField(Field _field, Pieces _pieces)
    {
        if (!_field || !_pieces)
            return false;

        if (!_field.IsEmpty)
            return false;

        _field.PutPiece(_pieces);

        return true;
    }

    public bool[] CheckIsRight(Pieces _pieces, PiecesData[] _datas, out List<Pieces> _neighbors, bool _isCalledOthers = false)
    {
        bool[] _bools = new bool[_datas.Length];
        List<Pieces> _neighBors = new();

        for (int i = 0; i < _datas.Length; i++)
        {
            PiecesEnum _enum = _datas[i].Enum;
            Pieces _target = _datas[i].TargetPieces;

            Field _field = FieldParent.Instance.GetField(_pieces, _enum);

            if (!_field.CurrentPiece)
                continue;

            int a = 0;
            for (int k = 0; k < _field.CurrentPiece.Datas.Length; k++)
            {
                //Directiondaki nin datasý ile istediðimin datasý aynýmý ona bakýyorum
                if (_target.Datas[k].TargetPieces.name.Equals(_field.CurrentPiece.Datas[k].TargetPieces.name)
                    && _target.Datas[k].Enum.Equals(_field.CurrentPiece.Datas[k].Enum))
                {
                    a++;
                }
            }

            _bools[i] = a.Equals(_field.CurrentPiece.Datas.Length);

            if (_bools[i] && !_isCalledOthers)
            {
                foreach (Pieces pieces in _field.CurrentPiece.RightPieces)
                    if (!_neighBors.Contains(pieces))
                        _neighBors.Add(pieces);

                if (!_neighBors.Contains(_field.CurrentPiece))
                    _neighBors.Add(_field.CurrentPiece);
            }

            if (!_isCalledOthers)
                _field.CurrentPiece.OnPutRight(_isCalledOthers: true);
        }

        _neighbors = _neighBors;
        return _bools;
    }

    //public void CheckPieceControl(RectTransform puzzleobjesi)
    //{
    //    puzzleobjesi.anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
    //}

    //public void puzzleexit()
    //{
    //    Mousecekildi = false;
    //}

    //public void puzzleenter()
    //{
    //    Mousecekildi = true;
    //}
}