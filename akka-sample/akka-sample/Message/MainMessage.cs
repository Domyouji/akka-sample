namespace akka_sample.Message {
	public class MainMessage {
		public MainMessage(string parseMessage) {
			ParseMessage = parseMessage;
		}

		public string ParseMessage { get; private set; }
	}
}
