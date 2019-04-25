using System;
using Akka.Actor;
using Akka.DI.Core;
using akka_sample.Actors;
using Autofac;

namespace akka_sample {
	internal class Program {
		private static void Main(string[] args) {
			var system = ContainerOperations.Instance.Container.Resolve<ActorSystem>();
			system.UseAutofac(ContainerOperations.Instance.Container);
			var mainActor = system.ActorOf(system.DI().Props<MainActor>(), "MainActor");
			mainActor.Tell(true);

			Console.ReadLine();
		}
	}
}
