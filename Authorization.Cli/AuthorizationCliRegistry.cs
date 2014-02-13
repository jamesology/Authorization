using StructureMap.Configuration.DSL;

namespace Authorization.Cli
{
	public class AuthorizationCliRegistry : Registry
	{
		public AuthorizationCliRegistry()
		{
			Scan(x =>
			{
				x.TheCallingAssembly();

				x.WithDefaultConventions();
			});
		}
	}
}
