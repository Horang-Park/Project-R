using System;
using DG.Tweening;
using Interfaces.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Common
{
    public class PopupController : BaseCommonUI, IGetTextComponents, IGetButtonComponents
    {
        public record Data(string Context, string Title = "", bool UseOneButton = false, Action RightButtonAction = null, Action LeftButtonAction = null, string RightButtonCaption = "", string LeftButtonCaption = "")
        {
            public string Title { get; } = string.IsNullOrEmpty(Title) || string.IsNullOrWhiteSpace(Title) ? "Project R" : Title;
            public string Context { get; } = string.IsNullOrEmpty(Context) || string.IsNullOrWhiteSpace(Context) ? "Context is empty" : Context;
            public string LeftButtonCaption { get; } = string.IsNullOrEmpty(LeftButtonCaption) || string.IsNullOrWhiteSpace(LeftButtonCaption) ? "No" : LeftButtonCaption;
            public string RightButtonCaption { get; } = string.IsNullOrEmpty(RightButtonCaption) || string.IsNullOrWhiteSpace(RightButtonCaption) ? "Yes" : RightButtonCaption;
            public Action LeftButtonAction { get; } = LeftButtonAction;
            public Action RightButtonAction { get; } = RightButtonAction;
            public bool UseOneButton { get; } = UseOneButton;
        }

        public Data Put
        {
            set => _data = value;
        }

        private TMP_Text _title;
        private TMP_Text _context;
        private Button _leftButton;
        private TMP_Text _leftButtonCaption;
        private Button _rightButton;
        private TMP_Text _rightButtonCaption;
        private bool _useOneButton;
        private Data _data;

        public void GetTextComponents()
        {
            var texts = GetComponentsInChildren<TMP_Text>();

            foreach (var text in texts)
            {
                switch (text.gameObject.name)
                {
                    case "Title":
                        _title = text;
                        break;
                    case "Context":
                        _context = text;
                        break;
                    case "Left Caption":
                        _leftButtonCaption = text;
                        break;
                    case "Right Caption":
                        _rightButtonCaption = text;
                        break;
                }
            }
        }

        public void GetButtonComponents()
        {
            var buttons = GetComponentsInChildren<Button>();

            foreach (var button in buttons)
            {
                if (button.gameObject.name.Equals("Left"))
                {
                    _leftButton = button;

                    continue;
                }

                _rightButton = button;
            }
        }

        public override void Show()
        {
            if ((int)CommonUIVisibility >> 1 == 0b0001)
            {
                return;
            }

            _leftButton.onClick.AddListener(Hide);
            _rightButton.onClick.AddListener(Hide);

            base.Show();

            MainBackground.DOAnchorPosY(0.0f, 0.2f)
                .From(new Vector2(0.0f, -40.0f));
        }

        protected override void Hide()
        {
            if ((int)CommonUIVisibility >> 3 == 0b0001)
            {
                return;
            }

            _leftButton.onClick.RemoveAllListeners();
            _rightButton.onClick.RemoveAllListeners();

            base.Hide();

            MainBackground.DOAnchorPosY(-40.0f, 0.4f)
                .From(new Vector2(0.0f, 0.0f));
        }

        protected override void SetData()
        {
            _title.text = _data.Title;
            _context.text = _data.Context;
            _leftButtonCaption.text = _data.LeftButtonCaption;
            _rightButtonCaption.text = _data.RightButtonCaption;
            _leftButton.gameObject.SetActive(_data.UseOneButton is false);

            _leftButton.onClick.AddListener(() => _data.LeftButtonAction?.Invoke());
            _rightButton.onClick.AddListener(() => _data.RightButtonAction?.Invoke());
        }
    }
}