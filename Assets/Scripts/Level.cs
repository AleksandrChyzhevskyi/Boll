using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;
    [SerializeField] private PoolCircles _pool;
    [SerializeField] private CameraHit _cameraHit;

    private Border _border;
    private Check _check;
    private Movement _movement;
    
    private Circle _currentSelectable;

    public Circle _target;
    public Circle _currentChose;

    private void Awake()
    {
        _border = new Border(_size, _pool);
        _movement = new Movement(_pool, _size);
        _check = new Check();
    }

    private void Start()
    {
        _border.CreateBord();
        _check.Initialization(_pool);

        _check.CheckHorizontal();
        _check.CheckVertical();
    }

    private void Update()
    {
        Select();

        if (Input.GetMouseButtonDown(0) && _target == null) //отслеживание перемещения 
        {
            _target = _cameraHit.GetCircle();
            _target.Select(true);
        } 
            

        if (Input.GetMouseButtonUp(0))
        {
            _currentChose = _cameraHit.GetCircle();
            
            if(_target == null || _currentChose == null)
                return;

            Color color = _target.ElementColor;
            _target.SetColor(_currentChose.ElementColor);
            _currentChose.SetColor(color);
            _target = null;
            _currentChose = null;
        }
        
        if (Input.GetMouseButtonDown(2))
        {
            _movement.Move();

            _border.ActiveCircle();

            _check.CheckHorizontal();
            _check.CheckVertical();
        }
    }

    private void Select()
    {
        Circle selectable = _cameraHit.GetCircle();

        if (selectable == null)
        {
            if (_currentSelectable)
            {
                _currentSelectable.Deselect();
                _currentSelectable = null;
            }

            return;
        }

        if (_currentSelectable && _currentSelectable != selectable)
            _currentSelectable.Deselect();

        _currentSelectable = selectable;
        _currentSelectable.Select(false);
    }
}