using TMPro;
using UnityEngine;

namespace Game
{
	public class NameViewer : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _nameText;

		private void Awake()
		{
			_nameText.text = $"Logged in as: {PlayfabManager.CurrentLoggedInUser}";
		}
	}
}