using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Rybird.Framework
{
    public sealed class WeakFunc<T, TResult> : IWeakFunc<T, TResult>
    {
        private Func<T, TResult> _staticFunc;
        private WeakReference _funcTargetReference;
        private MethodInfo _method;

        public WeakFunc(Func<T, TResult> func)
            : this(func == null ? null : func.Target, func)
        {
        }

        public WeakFunc(object target, Func<T, TResult> func)
        {
            func.ThrowIfNull("func");
            _method = func.GetMethodInfo();
            if (func.GetMethodInfo().IsStatic)
            {
                _staticFunc = func;
            }
            if (target != null)
            {
                _funcTargetReference = new WeakReference(target);
            }
        }

        public bool IsStatic
        {
            get
            {
                return _staticFunc != null;
            }
        }

        public bool IsAlive
        {
            get
            {
                return _funcTargetReference != null
                    ? _funcTargetReference.IsAlive
                    : _staticFunc != null;
            }
        }

        private object FuncTarget
        {
            get
            {
                return _funcTargetReference != null ? _funcTargetReference.Target : null;
            }
        }

        public TResult Execute(T obj)
        {
            if (_staticFunc != null)
            {
                return _staticFunc(obj);
            }
            else
            {
                var funcTarget = FuncTarget;
                if (IsAlive)
                {
                    if (_method != null && funcTarget != null)
                    {
                        return (TResult)_method.Invoke(funcTarget, new object[] { obj });
                    }
                }
                return default(TResult);
            }
        }
    }
}