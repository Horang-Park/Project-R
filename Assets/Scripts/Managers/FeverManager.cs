using Horang.HorangUnityLibrary.Foundation;
using UI.Fever;

namespace Managers
{
	public class FeverManager : MonoSingleton<FeverManager>
	{
		public FeverTimeGageController FeverTimeGageController => _feverTimeGageController;
		private FeverTimeGageController _feverTimeGageController;

		protected override void Awake()
		{
			base.Awake();

			_feverTimeGageController = GetComponentInChildren<FeverTimeGageController>();
		}
	}
}