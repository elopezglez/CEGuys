using System;
using Prism.Events;

namespace TrainingGuys
{
	public class PrismEvent: PubSubEvent<string>
	{
		public PrismEvent()
		{
		}
	}
}
