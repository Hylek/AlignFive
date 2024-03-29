using UI.Core;
using UnityEngine.UIElements;

namespace UI.Panels
{
    public class TransitionPanel : BasePanel
    {
        public TransitionPanel(VisualElement parent, bool startHidden = true, bool hasBackButton = false)
            : base(parent, startHidden, hasBackButton)
        {
            
        }

        protected override void OnBackButton(ClickEvent evt) { }

        public override void AcquireReferences()
        {
            
        }
    }
}