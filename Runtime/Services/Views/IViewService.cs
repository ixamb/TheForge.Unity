using System.Collections.Generic;
using JetBrains.Annotations;

namespace TheForge.Services.Views
{
    /// <inheritdoc cref="ViewService"/>
    public interface IViewService
    {
        /// <inheritdoc cref="ViewService.RegisterView"/>
        void RegisterView(IView view);
        
        /// <inheritdoc cref="ViewService.UnregisterView"/>
        void UnregisterView(IView view);

        /// <inheritdoc cref="ViewService.GetActiveAndEnabledViews"/>
        IEnumerable<IView> GetActiveAndEnabledViews();
        
        /// <inheritdoc cref="ViewService.GetView"/>
        [CanBeNull] IView GetView(string code);
        
        /// <inheritdoc cref="ViewService.GetView{TIView}(string)"/>
        [CanBeNull] TIView GetView<TIView>(string code) where TIView : class, IView;

        /// <inheritdoc cref="ViewService.GetView{TIView}()"/>
        [CanBeNull] TIView GetView<TIView>() where TIView : class, IView;
    }
}