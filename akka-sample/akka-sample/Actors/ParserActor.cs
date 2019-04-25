using System;
using Akka.Actor;
using akka_sample.Message;

namespace akka_sample.Actors {
	public class ParserActor : ReceiveActor {
		private readonly IChildCreatorActor childCreatorActor;

		public ParserActor(IChildCreatorActor childCreatorActor) {
			this.childCreatorActor = childCreatorActor;

			Receive<MainMessage>((x) => ParseMessage(x));
		}

		public void ParseMessage(MainMessage message) {
			Console.WriteLine(message.ParseMessage);
			var fooActor = childCreatorActor.Create<FooActor>(Context, "FooActor");
			fooActor.Tell("doNothing");
			Sender.Tell(message);
		}
	}
}
