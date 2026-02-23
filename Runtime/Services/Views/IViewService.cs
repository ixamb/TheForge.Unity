using System.Collections.Generic;

namespace TheForge.Services.Views
{
    /// <inheritdoc cref="ViewService"/>
    public interface IViewService : ISingleton
    {
        /// <inheritdoc cref="ViewService.RegisterView"/>
        void RegisterView(IView view);
        
        /// <inheritdoc cref="ViewService.UnregisterView"/>
        void UnregisterView(IView view);

        /// <inheritdoc cref="ViewService.GetActiveAndEnabledViews"/>
        IEnumerable<IView> GetActiveAndEnabledViews();
        
        /// <inheritdoc cref="ViewService.GetView"/>
        IView GetView(string code);
        
        /// <inheritdoc cref="ViewService.GetView{TIView}"/>
        TIView GetView<TIView>(string code) where TIView : class, IView;
    }
}