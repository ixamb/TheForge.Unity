using System;
using TheForge.Services.Views;
using UnityEngine;
using VContainer;

namespace TheForge.Systems.Actions
{
    [CreateAssetMenu(fileName = "View Action", menuName = "Core/Systems/Actions/View Action")]
    public sealed class ViewAction : GameAction
    {
        private enum ViewActionType { Show, Hide }
        
        [SerializeField] private ViewActionType viewActionType;
        [SerializeField] private string viewCode;

        private IViewService _viewService;
        
        public override void Init(IObjectResolver resolver)
        {
            _viewService = resolver.Resolve<IViewService>();
        }

        protected override void Executable()
        {
            switch (viewActionType)
            {
                case ViewActionType.Show: _viewService.GetView(viewCode)?.ShowView(); break;
                case ViewActionType.Hide: _viewService.GetView(viewCode)?.HideView(); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}