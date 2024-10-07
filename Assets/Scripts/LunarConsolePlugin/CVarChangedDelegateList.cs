using LunarConsolePluginInternal;
using System;
using System.Runtime.CompilerServices;

namespace LunarConsolePlugin
{
	internal class CVarChangedDelegateList : BaseList<CVarChangedDelegate>
	{
		private static CVarChangedDelegate __f__mg_cache0;

        public CVarChangedDelegateList(int capacity)
            : base(NullCVarChangedDelegate, capacity)
        {

        }

        public void NotifyValueChanged(CVar cvar)
		{
			try
			{
				base.Lock();
				int count = this.list.Count;
				for (int i = 0; i < count; i++)
				{
					try
					{
						this.list[i](cvar);
					}
					catch (Exception exception)
					{
						Log.e(exception, "Exception while calling value changed delegate for '{0}'", new object[]
						{
							cvar.Name
						});
					}
				}
			}
			finally
			{
				base.Unlock();
			}
		}

		private static void NullCVarChangedDelegate(CVar cvar)
		{
		}
	}
}
