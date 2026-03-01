using System;
using System.Collections.Generic;
using TheForge.Extensions;
using UnityEngine;
using VContainer;

namespace TheForge.Services.Views
{
    [RequireComponent(typeof(CanvasGroup), typeof(Animator))]
    public class View : MonoBehaviour, IView
    {
        [SerializeField] protected string viewCode;
        [SerializeField] protected bool useAnimation;
        [SerializeField] protected bool keepViewDisplayed;

        public Action OnShow;
        public Action OnHide;
        
        private CanvasGroup _canvasGroup;
        private Animator _animator;
        
        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");
        
        private IViewService _viewService;
        
        [Inject]
        public void Construct(IViewService viewService)
        {
            _viewService = viewService;
            _viewService.RegisterView(this);
        }
        
        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _animator = useAnimation ? GetComponent<Animator>() : null;

            if (keepViewDisplayed)
            {
                TurnCanvasGroupOn();
            }
        }

        public void ShowView()
        {
            if (useAnimation)
            {
                _animator.SetTrigger(Show);
            }
            else
            {
                TurnCanvasGroupOn();
            }
            OnShow?.Invoke();
        }

        public void HideView()
        {
            if (keepViewDisplayed)
                return;
            
            if (useAnimation)
            {
                _animator.SetTrigger(Hide);
            }
            else
            {
                TurnCanvasGroupOff();
            }
            OnHide?.Invoke();
        }

        private void TurnCanvasGroupOn()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        private void TurnCanvasGroupOff()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public bool IsVisibleAndActive()
        {
            return _canvasGroup.alpha > 0
                   && _canvasGroup.interactable
                   && _canvasGroup.blocksRaycasts;
        }

        public string GetCode() => viewCode;
    }
}