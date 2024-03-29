using System;
using System.Collections.Generic;
using System.Linq;
using DC.MessageService;
using DC.ServiceLocator;
using UnityEngine;

namespace Utils
{
    public class EventBase : IDisposable
    {
        protected ITinyMessengerHub EventHub => _eventHub ??= BaseLocator.Find<ITinyMessengerHub>();

        private readonly Dictionary<Type, TinyMessageSubscriptionToken> _tokens = new();
        private ITinyMessengerHub _eventHub;

        protected void Subscribe<T>(Action<T> action) where T : class, ITinyMessage
        {
            if (!BaseLocator.DoesServiceExist(typeof(ITinyMessengerHub)))
            {
                BaseLocator.AddNewService<ITinyMessengerHub>(new TinyMessengerHub());
            }
            
            _tokens.Add(typeof(T), EventHub.Subscribe(action));
        }
        
        protected void Unsubscribe<T>()
        {
            var type = typeof(T);
            
            foreach (var token in
                     _tokens.Where(token => token.Key == type))
            {
                EventHub.Unsubscribe(token.Value);
                _tokens.Remove(token.Key);

                break;
            }
        }

        protected void Publish<T>(T message) where T : class, ITinyMessage
        {
            EventHub.Publish(message);
        }

        public void Dispose()
        {
            if (_tokens.Count <= 0) return;
            
            foreach (var token in _tokens)
            {
                EventHub.Unsubscribe(token.Value);
            }
            _tokens.Clear();
        }
    }
}