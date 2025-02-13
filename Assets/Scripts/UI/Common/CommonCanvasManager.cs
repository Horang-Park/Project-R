using Horang.HorangUnityLibrary.Foundation;

namespace UI.Common
{
	public class CommonCanvasManager : MonoSingleton<CommonCanvasManager>
	{
		private PopupController _popupController;
		private TopToastController _topToastController;

		public void ShowPopup(PopupController.Data popupContextData)
		{
			_popupController.Put = popupContextData;
			_popupController.Show();
		}

		public void ShowToast(string message)
		{
			_topToastController.Put = message;
			_topToastController.Show();
		}

		protected override void Awake()
		{
			base.Awake();

			_popupController = GetComponentInChildren<PopupController>();
			_topToastController = GetComponentInChildren<TopToastController>();

			InitializeComponents();
		}

		private void InitializeComponents()
		{
			_popupController.Initialize();
			_topToastController.Initialize();

			_popupController.GetTextComponents();
			_popupController.GetButtonComponents();

			_topToastController.GetTextComponents();;
		}
	}
}