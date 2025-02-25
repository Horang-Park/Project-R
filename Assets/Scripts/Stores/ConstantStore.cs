namespace Stores
{
	public struct ConstantStore
	{
		public const int DefaultKillScore = 100; // 기본 적 처치 점수
		
		public const int RequireKilledEnemyCountToSetFeverMode = 5; // 피버모드 발동을 위해 필요한 처치된 적 수 
		
		public const float FeverTimeDecreaseStep = 0.01f; // 피버타임에서 감소시킬 값

		public const int DefaultFeverMultiplier = 10; // 기본 피버 곱 1배
		public const int FeverMultiplierIncreaseStep = 1; // 피버 타임 중, 적을 하나 처치할 때 마다 기존 점수에 곱해지는 값에 더할 값

		public const int DefaultLimitTime = 60; // 게임 시간 60초

		public const string IsFirstLaunchSaveKey = "Is First Launch Game"; // 첫 실행 저장 키

		public const string BgmVolumeSaveKey = "Bgm Volume"; // 배경음 볼륨 로컬 저장 키
		public const string BgmOnOffSaveKey = "Bgm On Off"; // 배경음 뮤트 상태 로컬 저장 키
		public const string SfxVolumeSaveKey = "Sfx Volume"; // 효과음 볼륨 로컬 저장 키
		public const string SfxOnOffSaveKey = "Sfx On Off"; // 효과음 뮤트 상태 로컬 저장 키

		public const string UseAntialiasingSaveKey = "Use Antialiasing"; // 안티에일리어싱 사용 로컬 저장 키
		public const string UseHalfRenderScaleSaveKey = "Use Half Render Scale"; // 반 렌더 스케일 사용 로컬 저장 키
		public const string UsePostProcessingSaveKey = "Use Post Processing"; // 포스트프로세싱 사용 로컬 저장 키
	}
}