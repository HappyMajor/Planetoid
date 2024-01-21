using System;

namespace Planetoid.DateTime {
	public class MilliDateTime
	{
        public long ElapsedMillis { get => NowMillis - this.startMillis; }
		public long NowMillis { get => NowTicks / 10000; }
		public long NowTicks { get => System.DateTime.Now.Ticks; }

		private readonly long startMillis;

		public MilliDateTime()
		{
			this.startMillis = NowMillis;
        }
	}
}