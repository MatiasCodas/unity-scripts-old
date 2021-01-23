using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RectTransform))]
public abstract class DOTweenUI : MonoBehaviour
{
    [HideInInspector] public Tweener Tweener;

    [SerializeField] private bool playOnStart;

    [SerializeField] protected float delay = 0f;
    [SerializeField] protected float tweenDuration = 1f;
    [SerializeField] protected Ease easeType = Ease.InOutSine;

    public UnityEvent OnStartTween;
    public UnityEvent OnCompleteTween;

    private void Start()
    {
        //OnCompleteTween.AddListener(() => Debug.Log($"{gameObject} Tween Completed"));
        //OnCompleteTween.AddListener(() => Debug.Break());

        if (playOnStart)
        {
            Play();
        }
    }

    public virtual void Play()
    {
        SetTweener();

        Tweener.isBackwards = false;
        Tweener.Play();
    }

    public virtual void PlayBackwards()
    {
        SetTweener();

        Tweener.isBackwards = true;
        Tweener.Play();
    }

    protected virtual void SetTweener()
    {
        Tweener
               .SetDelay(delay)
               .SetEase(easeType);

        Tweener
            .OnStart(() => OnStartTween.Invoke())
            .OnComplete(() => OnCompleteTween.Invoke());
    }
}
