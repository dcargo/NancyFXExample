using System;
using System.IO;
using System.Web;
using System.Dynamic;
using Nancy;
using Nancy.Conventions;

namespace SixProject
{
	public class IndexModule : NancyModule
	{
		public IndexModule()
		{
			Get["/"] = _ => {
				return View["index"];
			};
		}
	}

	public class BootStrapper : DefaultNancyBootstrapper
	{
		protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
		{
			base.ApplicationStartup(container, pipelines);
			StaticConfiguration.DisableErrorTraces = false;
		}

		protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
		{
			base.ConfigureConventions(nancyConventions);
			nancyConventions.StaticContentsConventions.Add(Nancy.Conventions.StaticContentConventionBuilder.AddDirectory("/", "public"));
		}
	}

	public class MainApp
	{
		public static void Main(string[] args)
		{
			var config = new Nancy.Hosting.Self.HostConfiguration();
			config.UnhandledExceptionCallback += (ex) => {
				Console.WriteLine(ex);
			};
			var nancyHost = new Nancy.Hosting.Self.NancyHost(config, new Uri("http://localhost:7000"));
			nancyHost.Start();
			Console.ReadLine();
			nancyHost.Stop();
			System.Threading.Thread.Sleep(1000);
		}
	}
}

