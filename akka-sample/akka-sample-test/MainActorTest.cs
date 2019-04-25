using Akka.Actor;
using Akka.DI.Core;
using Akka.TestKit.Xunit2;
using akka_sample;
using akka_sample.Actors;
using akka_sample.Message;
using akka_sample.Services;
using Autofac;
using Moq;
using Xunit;

namespace akka_sample_test {
	public class MainActorTest : TestKit {
		[Fact]
		public void SendMessage_Boolean_Success() {
			ContainerOperations.Instance.ReInitialise();

			Mock<ISomeService> mockSomeService = new Mock<ISomeService>();
			mockSomeService.Setup(x => x.ReturnValue(It.IsAny<string>())).Returns("In a test mock");
			ContainerOperations.Instance.AddExtraModulesCallBack = builder => {
				builder.Register(x => mockSomeService.Object)
					.As<ISomeService>()
					.SingleInstance();
			};

			var system = ContainerOperations.Instance.Container.Resolve<ActorSystem>();
			system.UseAutofac(ContainerOperations.Instance.Container);

			var mainActor = system.ActorOf(system.DI().Props<MainActor>(), "MainActor");

			mainActor.Tell(true);

			ExpectMsg<bool>();
		}

		[Fact]
		public void SendMessage_MainMessage_Success() {
			ContainerOperations.Instance.ReInitialise();
			var message = new MainMessage("test");
			Mock<ISomeService> mockSomeService = new Mock<ISomeService>();
			mockSomeService.Setup(x => x.ReturnValue(It.IsAny<string>())).Returns("In a test mock");
			ContainerOperations.Instance.AddExtraModulesCallBack = builder => {
				builder.Register(x => mockSomeService.Object)
					.As<ISomeService>()
					.SingleInstance();
			};

			var system = ContainerOperations.Instance.Container.Resolve<ActorSystem>();
			system.UseAutofac(ContainerOperations.Instance.Container);

			var parserActor = system.ActorOf(system.DI().Props<ParserActor>(), "ParserActor");

			parserActor.Tell(message);

			ExpectMsg<MainMessage>();
		}
	}
}
