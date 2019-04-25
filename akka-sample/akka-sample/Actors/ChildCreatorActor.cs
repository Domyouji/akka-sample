using Akka.Actor;
using Akka.DI.Core;

namespace akka_sample.Actors {
	public class ChildCreatorActor : IChildCreatorActor {
		public IActorRef Create<TActor>(IActorContext context, string name) where TActor : ActorBase {
			return context.ActorOf(GetProps<TActor>(context), name);
		}

		public IActorRef Create<TActor>(IActorContext context) where TActor : ActorBase {
			return Create<TActor>(context, null);
		}

		public Props GetProps<TActor>(IActorContext context) where TActor : ActorBase {
			return context.DI().Props<TActor>();
		}
	}
}
