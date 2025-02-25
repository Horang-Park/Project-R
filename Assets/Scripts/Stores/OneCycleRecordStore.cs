using UniRx;

namespace Stores
{
	public struct OneCycleRecordStore
	{
		public static readonly IntReactiveProperty KilledEnemies = new(0);
		public static readonly IntReactiveProperty KilledEnemiesForFeverTime = new(0);
		public static readonly IntReactiveProperty Score = new(0);
		public static readonly IntReactiveProperty FeverTimeScore = new(0);
		public static readonly IntReactiveProperty CurrentFeverMultiplier = new(ConstantStore.DefaultFeverMultiplier);
		public static readonly BoolReactiveProperty IsTimeOver = new(false);
		public static readonly BoolReactiveProperty IsFeverTime = new(false);
		public static readonly BoolReactiveProperty IsCountdown = new(true);
		
		public static void Flush()
		{
			KilledEnemies.Value = 0;
			KilledEnemiesForFeverTime.Value = 0;
			Score.Value = 0;
			FeverTimeScore.Value = 0;
			CurrentFeverMultiplier.Value = ConstantStore.DefaultFeverMultiplier;
			IsTimeOver.Value = false;
			IsFeverTime.Value = false;
			IsCountdown.Value = true;
		}
	}
}