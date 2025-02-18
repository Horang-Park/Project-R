namespace Stores
{
	public struct ConstantStore
	{
		public const int DefaultKillScore = 100; // 기본 적 처치 점수
		
		public const int RequireKilledEnemyCountToSetFeverMode = 5; // 피버모드 발동을 위해 필요한 처치된 적 수 
		
		public const float FeverTimeDecreaseStep = 0.01f; // 피버타임에서 감소시킬 값

		public const int DefaultFeverMultiplier = 10; // 기본 피버 곱 1배
		public const int FeverMultiplierIncreaseStep = 1; // 피버 타임 중, 적을 하나 처치할 때 마다 기존 점수에 곱해지는 값에 더할 값

		public const int DefaultLimitTime = 10; // 게임 시간 60초
	}
}