using System.Collections.Generic;
using UI.Core;
using UI.Panels;
using UnityEngine; 
using UnityEngine.UIElements;
using Utils;

namespace UI
{
    public enum PanelType
    {
        Home,
        Loading,
        EnterName,
        Lobby,
        EnterJoinCode,
        Settings
    }
    
    public class MainMenuController : MonoBehaviour
    {
        private UIDocument _uiDocument;
        private VisualElement _root;
        private List<BasePanel> _panels;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument.rootVisualElement;
            
            _panels = new List<BasePanel>
            {
                new HomePanel(_root.Q<VisualElement>("HomeRoot"), false),
                new LoadingPanel(_root.Q<VisualElement>("LoadingRoot")),
                new EnterNamePanel(_root.Q<VisualElement>("NameRoot"), true, true),
                new LobbyPanel(_root.Q<VisualElement>("HostRoot"), true, true),
                new JoinPanel(_root.Q<VisualElement>("JoinRoot"), true, true),
                new SettingsPanel(_root.Q<VisualElement>("SettingsRoot"), true, true),
                new DialoguePopup(_root.Q<VisualElement>("DialogueRoot"))
            };
        }

        private void Start()
        {
            Locator.EventHub.Publish(new OpenHomePanelMessage());
        }

        private void OnDestroy()
        {
            foreach (var panel in _panels)
            {
                panel.Dispose();
            }
        }
    }
}