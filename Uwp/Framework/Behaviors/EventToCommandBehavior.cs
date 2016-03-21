using Microsoft.Xaml.Interactivity;
using System;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Rybird.Framework
{
    /// <summary>
    /// A behavior to imitate an EventToCommand trigger
    /// </summary>
    public class EventToCommandBehavior : DependencyObject, IBehavior
    {
        public void Attach(DependencyObject obj)
        {
            AssociatedObject = obj;
            var evt = AssociatedObject.GetType().GetRuntimeEvent(Event);
            if (evt != null)
            {
                Observable.FromEventPattern<RoutedEventArgs>(AssociatedObject, Event)
                  .Subscribe(se => FireCommand(se.EventArgs));
            }
        }

        public void Detach()
        {
        }

        private void FireCommand(RoutedEventArgs routedEventArgs)
        {
            var dataContext = ((FrameworkElement)AssociatedObject).DataContext;
            if (dataContext != null)
            {
                var dcType = dataContext.GetType();
                var commandGetter = dcType.GetRuntimeMethod("get_" + Command, new Type[0]);
                if (commandGetter != null)
                {
                    var command = commandGetter.Invoke(dataContext, null) as ICommand;
                    if (command != null && command.CanExecute(CommandParameter))
                    {
                        if (PassEventArgsToCommand && CommandParameter == null)
                        {
                            command.Execute(routedEventArgs);
                        }
                        else
                        {
                            command.Execute(CommandParameter);
                        }
                    }
                }
            }
        }

        public DependencyObject AssociatedObject { get; private set; }

        public bool PassEventArgsToCommand { get; set; }

        #region Event

        /// <summary>
        /// Event Property name
        /// </summary>
        public const string EventPropertyName = "Event";

        public string Event
        {
            get { return (string)GetValue(EventProperty); }
            set { SetValue(EventProperty, value); }
        }

        /// <summary>
        /// Event Property definition
        /// </summary>
        public static readonly DependencyProperty EventProperty = DependencyProperty.Register(
            EventPropertyName,
            typeof(string),
            typeof(EventToCommandBehavior),
            new PropertyMetadata(default(string)));

        #endregion

        #region Command

        /// <summary>
        /// Command Property name
        /// </summary>
        public const string CommandPropertyName = "Command";

        public string Command
        {
            get { return (string)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Command Property definition
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            CommandPropertyName,
            typeof(string),
            typeof(EventToCommandBehavior),
            new PropertyMetadata(default(string)));

        #endregion

        #region CommandParameter

        /// <summary>
        /// CommandParameter Property name
        /// </summary>
        public const string CommandParameterPropertyName = "CommandParameter";

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// CommandParameter Property definition
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            CommandParameterPropertyName,
            typeof(object),
            typeof(EventToCommandBehavior),
            new PropertyMetadata(default(object)));

        #endregion
    }
}
