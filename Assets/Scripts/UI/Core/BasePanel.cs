using System;
using UnityEngine;
using UnityEngine.UIElements;
using EventBase = Utils.EventBase;

namespace UI.Core
{
    public abstract class BasePanel : EventBase
    {
        public const string HiddenClass = "rootpanel-hide";
        
        public readonly bool StartHidden;
        public readonly bool HasBackButton;
        
        protected VisualElement Root;
        protected Button BackButton;

        protected BasePanel(VisualElement parent, bool startHidden = true, bool hasBackButton = false)
        {
            StartHidden = startHidden;
            HasBackButton = hasBackButton;
            Root = parent ?? throw new ArgumentNullException(nameof(parent));
            Init();
        }
        
        public virtual void Init()
        {
            if (StartHidden)
            {
                HideImmediately();
                Root.SendToBack();
            }

            if (HasBackButton)
            {
                BackButton = Root.Q<Button>("BackButton");
                BackButton.RegisterCallback<ClickEvent>(OnBackButton);
            }
            
            Root.RegisterCallback<TransitionEndEvent>(OnTransitionComplete);
            
            AcquireReferences();
        }

        protected virtual void OnBackButton(ClickEvent evt) { }

        public abstract void AcquireReferences();

        private void OnTransitionComplete(TransitionEndEvent evt)
        {
            if (evt.target == Root && Root.ClassListContains(HiddenClass))
            {
                Debug.Log("Transition Complete: Hide Immediately");
                HideImmediately();
            }
        }

        public virtual void Open()
        {
            if (Root.style.display == DisplayStyle.None)
            {
                Debug.Log("Setting display to flex");
                Root.style.display = DisplayStyle.Flex;
            }
            Root.BringToFront();
            
            if (Root.ClassListContains(HiddenClass))
            {
                Debug.Log("Removing Hidden Class");
                Root.RemoveFromClassList(HiddenClass);
            }
        }

        public virtual void Close()
        {
            if (!Root.ClassListContains(HiddenClass))
            {
                Root.AddToClassList(HiddenClass);
            }
        }

        public virtual void HideImmediately()
        {
            if (!Root.ClassListContains(HiddenClass))
            {
                Debug.Log("Adding hidden class");
                Root.AddToClassList(HiddenClass);
            }
            
            Root.style.display = DisplayStyle.None;
        }
    }
}