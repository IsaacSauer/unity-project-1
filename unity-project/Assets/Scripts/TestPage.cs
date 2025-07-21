using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class TestPage : Page
	{
		#region Editor Fields

		[SerializeField] private string _id;
		[SerializeField] private Button _button;

		#endregion

		#region Properties

		public override string ID => _id;

		#endregion

		private void Awake()
		{
			_button.onClick.AddListener(() => PageHandler.Instance.ToPage("HOMEPAGE"));
		}
	}
}