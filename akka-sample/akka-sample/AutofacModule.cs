using System;
using Akka.Actor;
using akka_sample.Actors;
using akka_sample.Services;
using Autofac;

namespace akka_sample {
	public class AutofacModule : Module {
		protected override void Load(ContainerBuilder builder) {
			builder.RegisterType<SomeService>()
				.As<ISomeService>()
				.SingleInstance();

			builder.RegisterType<ChildCreatorActor>()
				.As<IChildCreatorActor>()
				.SingleInstance();

			builder.RegisterType<MainActor>();
			builder.RegisterType<ParserActor>();
			builder.RegisterType<FooActor>();

			var _runModelActorSystem = new Lazy<ActorSystem>(() => {
				return ActorSystem.Create("MySystem");
			});

			builder.Register(cont => _runModelActorSystem.Value);
		}
	}
}
