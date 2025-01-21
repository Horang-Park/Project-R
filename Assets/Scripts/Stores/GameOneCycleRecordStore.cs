using Interfaces;

namespace Stores
{
	public struct GameOneCycleRecordStore : IFlushable
	{
		public static int KilledEnemies;
		public static int Score;
		public static float CurrentFeverTime;
		public static float CurrentFeverMultiplier;
		
		public void Flush()
		{
			KilledEnemies = 0;
			Score = 0;
			CurrentFeverTime = ConstantStore.DefaultFeverTime;
			CurrentFeverMultiplier = ConstantStore.DefaultFeverMultiplier;
		}
	}
}