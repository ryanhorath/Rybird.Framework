﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Rybird.Framework
{
    public sealed class WeakAction<T> : IWeakAction<T>
    {
        private Action<T> _staticAction;
        private WeakReference _actionTargetReference;
        private MethodInfo _method;

        public WeakAction(Action<T> action)
        {
            action.ThrowIfNull("action");
            _method = action.GetMethodInfo();
            if (action.GetMethodInfo().IsStatic)
            {
                _staticAction = action;
            }
            if (action.Target != null)
            {
                _actionTargetReference = new WeakReference(action.Target);
            }
        }

        public bool IsStatic
        {
            get
            {
                return _staticAction != null;
            }
        }

        public bool IsAlive
        {
            get
            {
                return _actionTargetReference != null
                    ? _actionTargetReference.IsAlive
                    : _staticAction != null;
            }
        }

        private object ActionTarget
        {
            get
            {
                return _actionTargetReference != null ? _actionTargetReference.Target : null;
            }
        }

        public void Execute(T obj)
        {
            if (_staticAction != null)
            {
                _staticAction(obj);
            }
            else
            {
                var actionTarget = ActionTarget;
                if (IsAlive)
                {
                    if (_method != null && actionTarget != null)
                    {
                        _method.Invoke(actionTarget, new object[] { obj });
                    }
                }
            }
        }
    }
}