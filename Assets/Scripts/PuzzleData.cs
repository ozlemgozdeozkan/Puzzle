using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ScriptableObjects/Data's/Puzzle")]
public class PuzzleData : ScriptableObject
{
    [SerializeField] private List<Pieces> _initializePrefabs;

    private List<Pieces> _currentPiecePrefabs = new();

    private bool _isInitialized = false;
    public bool IsInitialized { get => _isInitialized; private set => _isInitialized = value; }

    private Dictionary<Pieces, int> _piecesIndex = new();

    public void Initialize(System.Action _endAction = null)
    {
        _piecesIndex.Clear();

        _currentPiecePrefabs = new();

        _currentPiecePrefabs.AddRange(_initializePrefabs);

        foreach (Pieces _pieces in _currentPiecePrefabs)
            _piecesIndex.Add(_pieces, _currentPiecePrefabs.IndexOf(_pieces));

        IsInitialized = true;
    }

    /* public GameObject GetRandomAvailableObject()
     {
         GameObject _willReturnObject = _currentPiecePrefabs[Random.Range(0, _currentPiecePrefabs.Count - 1)]; //rastgele bir obje seçiyorum.

         _currentPiecePrefabs.Remove(_willReturnObject); //Bu objeyi birdaha adama yollamamak için listeden çýkartýyorum.

         return _willReturnObject; //En sonunda adama bu objeyi döndürüyorum.
     }*/

    public bool GetRandomAvailableObject(out Pieces _object)
    {
        if (_currentPiecePrefabs.Count.Equals(0))
        {
            _object = null;
            return false;
        }
        Pieces _willReturnObject = _currentPiecePrefabs[Random.Range(0, _currentPiecePrefabs.Count)]; //rastgele bir obje seçiyorum.

        _currentPiecePrefabs.Remove(_willReturnObject); //Bu objeyi birdaha adama yollamamak için listeden çýkartýyorum.

        _willReturnObject.TargetIndex = _piecesIndex[_willReturnObject];

        _object = _willReturnObject;
        return _object != null; //En sonunda adama bu objeyi döndürüyorum.
    }

    public int GetPiecesIndex<T>(T _pieces) where T : Pieces => _piecesIndex[_pieces];

}