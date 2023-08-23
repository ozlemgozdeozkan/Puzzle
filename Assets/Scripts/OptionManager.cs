using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OptionManager : MonoBehaviour
{
    [SerializeField] private List<PuzzleData> puzzleData;
    public GameObject optionSquareImage;

    [SerializeField] private List<Field> _optionSlots = null;

    private void OnEnable()
    {
        Pieces.OnPutAction += PrepareToOptions;
    }
    private void OnDisable()
    {
        Pieces.OnPutAction -= PrepareToOptions;
    }

    private void Awake()
    {
        puzzleData.ForEach(data => data.Initialize());

        /*foreach (PuzzleData data in puzzleData)
        {
            data.Initialize();
        }*/
    }

    private void Start()
    {
        PrepareToOptions();
    }

    public void PrepareToOptions()
    {
        //Debug.Log(IsOptionsEmpty());
        if (!IsOptionsEmpty())
            return;

        for (int i = 0; i < _optionSlots.Count; i++)
        {
            if (!puzzleData[0].GetRandomAvailableObject(out Pieces _randomObject))
                return;

            Pieces _spawned = Instantiate(_randomObject, _optionSlots[i].transform);
            _spawned.TargetIndex = _randomObject.TargetIndex;

            _spawned.CurrentField = _optionSlots[i];
            _optionSlots[i].CurrentPiece = _spawned;

            //_getRandomObject.transform.SetParent(_optionSlots[i]);
            _spawned.transform.localPosition = Vector3.zero;
        }
    }

    private bool IsOptionsEmpty()
    {
        int a = 0;

        for (int i = 0; i < _optionSlots.Count; i++)
        {
            if (_optionSlots[i].CurrentPiece)
                continue;
            a++;
        }
        return a.Equals(_optionSlots.Count);
    }
}