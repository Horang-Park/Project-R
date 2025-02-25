using Horang.HorangUnityLibrary.Foundation;
using Horang.HorangUnityLibrary.Modules.AudioModule;

namespace UI.Common
{
	public class CommonCanvasManager : MonoSingleton<CommonCanvasManager>
	{
		private InputFieldPopupController _inputFieldPopupController;
		private PopupController _popupController;
		private TopToastController _topToastController;

		public void ShowInputFieldPopup(InputFieldPopupController.Data popupContextData)
		{
			AudioModule.Play("notification");

			_inputFieldPopupController.Put = popupContextData;
			_inputFieldPopupController.Show();
		}

		public void ShowPopup(PopupController.Data popupContextData)
		{
			AudioModule.Play("notification");

			_popupController.Put = popupContextData;
			_popupController.Show();
		}

		public void ShowToast(string message)
		{
			AudioModule.Play("notification");

			_topToastController.Put = message;
			_topToastController.Show();
		}

		protected override void Awake()
		{
			base.Awake();

			_inputFieldPopupController = GetComponentInChildren<InputFieldPopupController>();
			_popupController = GetComponentInChildren<PopupController>();
			_topToastController = GetComponentInChildren<TopToastController>();

			InitializeComponents();
		}

		private void InitializeComponents()
		{
			_inputFieldPopupController.Initialize();
			_popupController.Initialize();
			_topToastController.Initialize();

			_inputFieldPopupController.GetTextComponents();
			_inputFieldPopupController.GetButtonComponents();

			_popupController.GetTextComponents();
			_popupController.GetButtonComponents();

			_topToastController.GetTextComponents();;
		}
	}
}