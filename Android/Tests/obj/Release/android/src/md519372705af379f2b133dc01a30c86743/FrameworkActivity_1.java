package md519372705af379f2b133dc01a30c86743;


public class FrameworkActivity_1
	extends md519372705af379f2b133dc01a30c86743.FrameworkActivity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Rybird.Framework.FrameworkActivity`1, Rybird.Framework.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", FrameworkActivity_1.class, __md_methods);
	}


	public FrameworkActivity_1 () throws java.lang.Throwable
	{
		super ();
		if (getClass () == FrameworkActivity_1.class)
			mono.android.TypeManager.Activate ("Rybird.Framework.FrameworkActivity`1, Rybird.Framework.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
