using Akka.Actor;
using akka_sample.Message;
using akka_sample.Services;

namespace akka_sample.Actors {
	public class MainActor : ReceiveActor {
		private readonly ISomeService someService;
		private readonly IChildCreatorActor childCreatorActor;

		public MainActor(ISomeService someService, IChildCreatorActor childCreatorActor) {
			this.someService = someService;
			this.childCreatorActor = childCreatorActor;

			Receive<bool>((x) => SendMessage(x));
		}

		public void SendMessage(bool send) {
			var parserActor = childCreatorActor.Create<ParserActor>(Context, "ParserActor");

			if (send) {
				var messageToParse = someService.ReturnValue("test");
				var message = new MainMessage(messageToParse);

				parserActor.Tell(message);
			}

			Sender.Tell(send);
		}
	}
}
