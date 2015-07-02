package md519372705af379f2b133dc01a30c86743;


public class JavaWrapper_1
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Rybird.Framework.JavaWrapper`1, Rybird.Framework.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", JavaWrapper_1.class, __md_methods);
	}


	public JavaWrapper_1 () throws java.lang.Throwable
	{
		super ();
		if (getClass () == JavaWrapper_1.class)
			mono.android.TypeManager.Activate ("Rybird.Framework.JavaWrapper`1, Rybird.Framework.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
