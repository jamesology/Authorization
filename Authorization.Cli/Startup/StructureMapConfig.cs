using Authorization.Core;
using StructureMap;

namespace Authorization.Cli.Startup
{
	public class StructureMapConfig : IBootstrapper
	{
		private static bool _hasStarted;
		public void BootstrapStructureMap()
		{
			ObjectFactory.Initialize(x =>
			{
				x.AddRegistry<AuthorizationCoreRegistry>();
				x.AddRegistry<AuthorizationCliRegistry>();
			});
		}

		public static void Restart()
		{
			if (_hasStarted)
			{
				ObjectFactory.ResetDefaults();
			}
			else
			{
				Bootstrap();
				_hasStarted = true;
			}
		}

		public static void Bootstrap()
		{
			new StructureMapConfig().BootstrapStructureMap();
		}
	}
}
