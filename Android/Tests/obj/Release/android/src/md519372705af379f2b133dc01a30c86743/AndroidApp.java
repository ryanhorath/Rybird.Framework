package md519372705af379f2b133dc01a30c86743;


public abstract class AndroidApp
	extends mono.android.app.Application
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:()V:GetOnCreateHandler\n" +
			"";
	}

	public void onCreate ()
	{
		mono.android.Runtime.register ("Rybird.Framework.AndroidApp, Rybird.Framework.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AndroidApp.class, __md_methods);
		n_onCreate ();
	}

	private native void n_onCreate ();

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
