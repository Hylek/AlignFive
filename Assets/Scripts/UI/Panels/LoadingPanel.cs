using UI.Core;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace UI.Panels
{
    public class LoadingPanel : BasePanel
    {
        
        public LoadingPanel(VisualElement parent, bool startHidden = true, bool hasBackButton = false)
            : base(parent, startHidden, hasBackButton)
        {
            Subscribe<ShowLoadingPanelMessage>(OnOpen);
            Subscribe<HideLoadingPanelMessage>(OnClose);
        }

        private void OnClose(HideLoadingPanelMessage obj)
        {
            Debug.Log("Loading Panel Closing.");
            Close();
        }

        private void OnOpen(ShowLoadingPanelMessage obj)
        {
            Debug.Log("Loading Panel Opening.");
            Open();
        }

        public override void AcquireReferences()
        {
            
        }
    }
}