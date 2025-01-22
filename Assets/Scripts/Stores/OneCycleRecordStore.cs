using Interfaces;
using UniRx;

namespace Stores
{
	public struct OneCycleRecordStore : IFlushable
	{
		public static readonly IntReactiveProperty KilledEnemies = new(0);
		public static readonly IntReactiveProperty Score = new(0);
		public static readonly FloatReactiveProperty CurrentFeverTime = new(ConstantStore.DefaultFeverTime);
		public static readonly FloatReactiveProperty CurrentFeverMultiplier = new(ConstantStore.DefaultFeverMultiplier);
		
		public void Flush()
		{
			KilledEnemies.Value = 0;
			Score.Value = 0;
			CurrentFeverTime.Value = ConstantStore.DefaultFeverTime;
			CurrentFeverMultiplier.Value = ConstantStore.DefaultFeverMultiplier;
		}
	}
}