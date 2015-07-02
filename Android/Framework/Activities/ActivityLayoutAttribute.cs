using System;

namespace Rybird.Framework
{
	/// <summary>
	/// Used to identify the layout file(s) that are used for element property generation.
	/// </summary>
	/// <remarks>
	/// Mark the class as partial and include this attribute for each layout file associated with an activity.
	/// Add the Layout.tt template to the project and run it. It will generate partial classes that contain
	/// property accessors for all controls referenced in your layout.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class ActivityLayoutAttribute : Attribute
	{
		public int Layout { get; set; }

		public ActivityLayoutAttribute(int layout)
		{
			Layout = layout;
		}
	}
}