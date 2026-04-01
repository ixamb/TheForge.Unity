using System.Collections.Generic;
using System.Linq;

namespace TheForge.Services.Views
{
    /// <summary>
    /// A service manages the display of views within the uGUI framework.
    /// If the service is configured, each view present in the scene is registered with it upon initialization.
    /// The user can then access these views via getter functions, without using references.
    /// </summary>
    public sealed class ViewService : IViewService
    {
        private readonly Dictionary<string, IView> _views = new();

        /// <summary>
        /// Registers a specified view.
        /// <remarks>Even tho the function is a public one due to interface usages, it should mainly be used by views for self registration.</remarks>
        /// </summary>
        void IViewService.RegisterView(IView view)
        {
            var viewCode = view.GetCode();
            if (string.IsNullOrWhiteSpace(viewCode))
                return;
            
            _views.TryAdd(viewCode, view);
        }

        /// <summary>
        /// Unregister a specified view.
        /// <remarks>Even tho the function is a public one due to interface usages, it should mainly be used by views for self unregistration.</remarks>
        /// </summary>
        void IViewService.UnregisterView(IView view)
        {
            var viewCode = view.GetCode();
            if (string.IsNullOrWhiteSpace(viewCode))
                return;
            
            _views.Remove(viewCode);
        }
        
        /// <summary>
        /// Retrieve views based on their active and enabled property.
        /// </summary>
        IEnumerable<IView> IViewService.GetActiveAndEnabledViews()
        {
            return _views.Values.Where(view => view.IsVisibleAndActive());
        }
        
        /// <summary>
        /// Retrieves a view based on its code, defined on the <c>View</c> object.
        /// <remarks>If you need a typed view without having to manually cast it, consider using the strongly typed <c>GetView(code)</c> function.</remarks>
        /// </summary>
        IView IViewService.GetView(string code)
        {
            return _views.GetValueOrDefault(code);
        }

        /// <summary>
        /// Retrieves a strongly typed view based on its code, defined on the <c>View</c> object.<br />
        /// The required view type has to match the view type defined with the parameter code.
        /// </summary>
        TIView IViewService.GetView<TIView>(string code)
        {
            if (_views.TryGetValue(code, out var view))
            {
                return view as TIView;
            }
            return null;
        }
        
        /// <summary>
        /// Retrieves a strongly typed view based on its type.
        /// </summary>
        TIView IViewService.GetView<TIView>()
        {
            return _views.Values.OfType<TIView>().FirstOrDefault();
        }
    }
}