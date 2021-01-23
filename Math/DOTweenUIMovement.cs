using DG.Tweening;
using UnityEngine;

public class DOTweenUIMovement : DOTweenUI
{
    [SerializeField] private Vector2 targetPos;
    protected RectTransform rect;


    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    protected override void SetTweener()
    {
        Tweener = rect.DOAnchorPos(targetPos, tweenDuration);
        base.SetTweener();
    }

    public void SetTweenerWithTarget(Vector2 target)
    {
        targetPos = target;
        SetTweener();
    }





    public void SetCurrentPositionAsTarget()
    {
        targetPos = GetComponent<RectTransform>().anchoredPosition;
    }
}
