using System;
using DG.Tweening;
using Interfaces.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Common
{
    public class InputFieldPopupController : BaseCommonUI, IGetTextComponents, IGetButtonComponents
    {
        public record Data(string Context, string Title = "", Action<string> ButtonAction = null, string ButtonCaption = "")
        {
            public string Title { get; } = string.IsNullOrEmpty(Title) || string.IsNullOrWhiteSpace(Title) ? "Project R" : Title;
            public string Context { get; } = string.IsNullOrEmpty(Context) || string.IsNullOrWhiteSpace(Context) ? "Context is empty" : Context;
            public string ButtonCaption { get; } = string.IsNullOrEmpty(ButtonCaption) || string.IsNullOrWhiteSpace(ButtonCaption) ? "OK" : ButtonCaption;
            public Action<string> ButtonAction { get; } = ButtonAction;
        }

        public Data Put
        {
            set => _data = value;
        }
        public string InputData => _inputField.text;

        private TMP_Text _title;
        private TMP_Text _context;
        private Button _button;
        private TMP_Text _buttonCaption;
        private TMP_InputField _inputField;
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
                    case "Right Caption":
                        _buttonCaption = text;
                        break;
                }
            }

            _inputField = GetComponentInChildren<TMP_InputField>();
        }

        public void GetButtonComponents()
        {
            var buttons = GetComponentsInChildren<Button>();

            foreach (var button in buttons)
            {
                if (button.gameObject.name.Equals("Right") is false)
                {
                    continue;
                }

                _button = button;

                break;
            }
        }

        public override void Show()
        {
            if ((int)CommonUIVisibility >> 1 == 0b0001)
            {
                return;
            }

            _button.onClick.AddListener(Hide);

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

            _button.onClick.RemoveAllListeners();

            base.Hide();

            MainBackground.DOAnchorPosY(-40.0f, 0.4f)
                .From(new Vector2(0.0f, 0.0f));
        }

        protected override void SetData()
        {
            _title.text = _data.Title;
            _context.text = _data.Context;
            _buttonCaption.text = _data.ButtonCaption;

            _button.onClick.AddListener(() => _data.ButtonAction?.Invoke(_inputField.text));
        }
    }
}