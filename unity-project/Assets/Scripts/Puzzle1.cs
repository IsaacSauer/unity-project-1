using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Puzzle1 : MonoBehaviour
    {
        [SerializeField] private InputField _answerInput;
        [SerializeField] private Button _submitButton;
        [SerializeField] private Text _feedbackText;
        [SerializeField] private EscapeRoomController _roomController;

        private string correctAnswer = "moon";

        void Start()
        {
            _submitButton.onClick.AddListener(OnSubmit);
            _feedbackText.text =
                "A riddle is carved into the wall: 'I am always with you at night, but disappear by day. What am I?'";
        }

        void OnSubmit()
        {
            if (_answerInput.text.Trim().ToLower() == correctAnswer)
            {
                _feedbackText.text = "Correct! A secret compartment opens.";
                _roomController.OnPuzzleSolved(0);
            }
            else
            {
                _feedbackText.text = "Incorrect. Try again.";
            }
        }
    }
}