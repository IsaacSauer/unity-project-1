using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Puzzle3 : MonoBehaviour
    {
        [SerializeField] private Toggle[] _symbolToggles;
        [SerializeField] private Button _submitButton;
        [SerializeField] private Text _feedbackText;
        [SerializeField] private EscapeRoomController _roomController;

        // Suppose the correct combination is toggles 1 and 3 are ON
        private bool[] correctStates = { true, false, true };

        void Start()
        {
            _feedbackText.text = "Three symbols glow on the door. Select the correct combination to open it.";
            _submitButton.onClick.AddListener(OnSubmit);
        }

        void OnSubmit()
        {
            bool isCorrect = true;
            for (int i = 0; i < _symbolToggles.Length; i++)
            {
                if (_symbolToggles[i].isOn != correctStates[i])
                {
                    isCorrect = false;
                    break;
                }
            }

            if (isCorrect)
            {
                _feedbackText.text = "The door opens with a creak! You are free!";
                _roomController.OnPuzzleSolved(2);
            }
            else
            {
                _feedbackText.text = "The symbols flash red. Try again.";
            }
        }
    }
}