using System;

namespace Rybird.Framework
{
    [AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RestorableStateAttribute : Attribute
    {

    }
}
