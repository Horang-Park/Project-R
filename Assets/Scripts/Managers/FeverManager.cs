using Horang.HorangUnityLibrary.Foundation;
using UI.Game.Fever;

namespace Managers
{
	public class FeverManager : MonoSingleton<FeverManager>
	{
		public float FeverTimeGageAddValue
		{
			set =>  _feverTimeGageController.AddValue = value;
		}

		private FeverTimeGageController _feverTimeGageController;

		protected override void Awake()
		{
			base.Awake();

			_feverTimeGageController = GetComponentInChildren<FeverTimeGageController>();
		}
	}
}