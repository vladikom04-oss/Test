using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickView : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform joystickBackground;
    [SerializeField] private RectTransform joystickHandle;
    [SerializeField] private Image joystickArea;

    private IPlayerViewModel _playerViewModel;

    private Vector2 _startPos;
    private Vector2 _inputVector;
    private bool _isDragging;

    public void Initialize(IPlayerViewModel playerViewModel)
    {
        _playerViewModel = playerViewModel;
    }

    private void Start()
    {
        _startPos = joystickBackground.anchoredPosition;

        if (!_isDragging)
        {
            joystickHandle.anchoredPosition = Vector2.zero;
        }
    }

    private void Update()
    {
        if (_isDragging)
        {
            _playerViewModel?.Move(_inputVector);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickArea.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        joystickBackground.anchoredPosition = localPoint;

        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;

        joystickBackground.anchoredPosition = _startPos;
        joystickHandle.anchoredPosition = Vector2.zero;

        _inputVector = Vector2.zero;
        _playerViewModel?.Move(Vector2.zero);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        {
            localPoint.x = localPoint.x / (joystickBackground.sizeDelta.x * 0.5f);
            localPoint.y = localPoint.y / (joystickBackground.sizeDelta.y * 0.5f);

            _inputVector = new Vector2(localPoint.x, localPoint.y);

            if (_inputVector.magnitude > 1f)
            {
                _inputVector = _inputVector.normalized;
            }

            joystickHandle.anchoredPosition = new Vector2(
                _inputVector.x * (joystickBackground.sizeDelta.x * 0.5f),
                _inputVector.y * (joystickBackground.sizeDelta.y * 0.5f)
            );

        }
    }
}