using Akka.Actor;

namespace akka_sample.Actors {
	public interface IChildCreatorActor {
		IActorRef Create<TActor>(IActorContext context, string name) where TActor : ActorBase;

		IActorRef Create<TActor>(IActorContext context) where TActor : ActorBase;

		Props GetProps<TActor>(IActorContext context) where TActor : ActorBase;
	}
}
