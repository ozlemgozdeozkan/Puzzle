using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; } = null;

    [SerializeField] private int _mapSizeX = 0;
    [SerializeField] private int _mapSizeY = 0;

    public int MapSizeX => _mapSizeX;
    public int MapSizeY => _mapSizeY;

    public GameObject[] puzzlePrefabs; // 3 adet prefab
    public GameObject piecePrefab; // Tek bir par�a prefab�

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        
    }

    //public transform[] gettrueneighboors(transform _origin)
    //{
    //    debug.log(_origin.name);

    //    int _sibling�ndex = _origin.getsibling�ndex();

    //    debug.log(_sibling�ndex);

    //    transform _parent = _origin.parent;

    //    debug.log(_parent.name);

    //    pieces _leftneighboor = _parent.getchild(_sibling�ndex - 1).getcomponent<pieces>();

    //    if (_leftneighboor)
    //        if (_leftneighboor.�nrightposition)
    //            debug.log("left is right");

    //    return null;
    //}
}