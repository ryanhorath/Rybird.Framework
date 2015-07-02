using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rybird.Framework
{
    /// T must be a class because it must be Nullable. During XAML initialization,
    /// CanExecute(null) will be called. In the case of value types, you cannot use
    /// default(T) because there is no way to distinguish that value from a real value
    /// of T. Therefore, the class forces the use of only types that can be null.
    public class DelegateCommand<T> : DelegateCommandBase where T : class
    {
        public DelegateCommand(Action<T> executeMethod) : this(executeMethod, (o) => true)
        {
            Guard.AgainstNull(executeMethod, "executeMethod");
        }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod) : base((o) => executeMethod((T)o), (o) => canExecuteMethod((T)o))
        {
            Guard.AgainstNull(executeMethod, "executeMethod");
            Guard.AgainstNull(canExecuteMethod, "canExecuteMethod");
        }

        private DelegateCommand(Func<T, Task> executeMethod) : this(executeMethod, (o) => true)
        {
            Guard.AgainstNull(executeMethod, "executeMethod");
        }

        private DelegateCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod) : base((o) => executeMethod((T)o), (o) => canExecuteMethod((T)o))
        {
            Guard.AgainstNull(executeMethod, "executeMethod");
            Guard.AgainstNull(canExecuteMethod, "canExecuteMethod");
        }

        public static DelegateCommand<T> FromAsyncHandler(Func<T, Task> executeMethod)
        {
            return new DelegateCommand<T>(executeMethod);
        }

        public static DelegateCommand<T> FromAsyncHandler(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod)
        {
            return new DelegateCommand<T>(executeMethod, canExecuteMethod);
        }

        public bool CanExecute(T parameter)
        {
            return base.CanExecute(parameter);
        }

        public async Task Execute(T parameter)
        {
            await base.Execute(parameter);
        }
    }

    public class DelegateCommand : DelegateCommandBase
    {
        public DelegateCommand(Action executeMethod) : this(executeMethod, () => true)
        {
            Guard.AgainstNull(executeMethod, "executeMethod");
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod) : base((o) => executeMethod(), (o) => canExecuteMethod())
        {
            Guard.AgainstNull(executeMethod, "executeMethod");
            Guard.AgainstNull(canExecuteMethod, "canExecuteMethod");
        }

        private DelegateCommand(Func<Task> executeMethod)
            : this(executeMethod, () => true)
        {
            Guard.AgainstNull(executeMethod, "executeMethod");
        }

        private DelegateCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod)
            : base((o) => executeMethod(), (o) => canExecuteMethod())
        {
            Guard.AgainstNull(executeMethod, "executeMethod");
            Guard.AgainstNull(canExecuteMethod, "canExecuteMethod");
        }

        public static DelegateCommand FromAsyncHandler(Func<Task> executeMethod)
        {
            return new DelegateCommand(executeMethod);
        }

        public static DelegateCommand FromAsyncHandler(Func<Task> executeMethod, Func<bool> canExecuteMethod)
        {
            return new DelegateCommand(executeMethod, canExecuteMethod);
        }

        public async Task Execute()
        {
            await Execute(null);
        }

        public bool CanExecute()
        {
            return CanExecute(null);
        }
    }
}
