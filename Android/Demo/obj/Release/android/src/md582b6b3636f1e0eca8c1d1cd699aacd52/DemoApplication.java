package md582b6b3636f1e0eca8c1d1cd699aacd52;


public class DemoApplication
	extends md519372705af379f2b133dc01a30c86743.AndroidApp
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
	}

	public void onCreate ()
	{
		mono.android.Runtime.register ("Rybird.Framework.Android.Demo.DemoApplication, Rybird.Framework.Android.Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DemoApplication.class, __md_methods);
		super.onCreate ();
	}

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
