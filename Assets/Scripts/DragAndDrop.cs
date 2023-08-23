using UnityEngine;
using UnityEngine.Rendering;
using UnityEngineInternal;

public class DragAndDrop : MonoBehaviour
{
    public GameObject _selectedPiece;
    private int _orderInLayer;

    [SerializeField] private LayerMask _pieceLayer = 0, _fieldLayer = 0;

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        RaycastHit2D hit = GetHit(GetMousePosition(), Vector2.zero, _pieceLayer);

    //        if (!hit.collider)
    //            return;

    //        if (hit.transform.parent)
    //            hit.transform.SetParent(null);
    //    }
    //    if (Input.GetMouseButton(0))
    //    {
    //        RaycastHit2D hit = GetHit(GetMousePosition(), Vector2.zero, _pieceLayer);

    //        if (!hit.collider)
    //            return;

    //        if (hit.transform.CompareTag("Puzzle"))
    //        {
    //            if (!hit.transform.GetComponent<Pieces>().InRightPosition)
    //            {
    //                _selectedPiece = hit.transform.gameObject;
    //                _selectedPiece.GetComponent<Pieces>().Selected = true;
    //                _selectedPiece.GetComponent<SortingGroup>().sortingOrder = _orderInLayer;
    //                _orderInLayer++;
    //            }
    //        }
    //    }
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        Transform _fieldTransform = GetFieldTransform();

    //        if (_fieldTransform && _selectedPiece)
    //        {
    //            if (_fieldTransform.transform.childCount > 0)
    //            {
    //                _selectedPiece.GetComponent<Pieces>().Selected = false;
    //                _selectedPiece = null;
    //                return;
    //            }

    //            _selectedPiece.transform.SetParent(_fieldTransform);
    //            _selectedPiece.transform.localPosition = Vector3.zero;
    //        }

    //        if (_selectedPiece != null)
    //        {
    //            _selectedPiece.GetComponent<Pieces>().Selected = false;
    //            _selectedPiece = null; 
    //        }
    //    }
    //    if (_selectedPiece != null)
    //    {
    //        Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        _selectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y, 0);
    //    } 
    //}


    private RaycastHit2D GetHit(Vector2 _origin, Vector2 _direction, LayerMask _layerMask, float _distance = Mathf.Infinity)
    {
        return Physics2D.Raycast(_origin, _direction, _distance, _layerMask);
    }

    private Transform GetFieldTransform()
    {
        RaycastHit2D hit = GetHit(GetMousePosition(), Vector2.zero, _fieldLayer);

        if (!hit.collider)
            return null;

        if (!hit.collider.CompareTag("CurrentField"))
            return null;

        return hit.collider.transform;
    }

    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}