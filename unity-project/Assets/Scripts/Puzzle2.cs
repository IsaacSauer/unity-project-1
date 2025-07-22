using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Puzzle2 : MonoBehaviour
    {
        [SerializeField] private Button[] _artifactButtons;
        [SerializeField] private Text _feedbackText;
        [SerializeField] private EscapeRoomController _roomController;

        private int correctButtonIndex = 2; // e.g., third button is correct

        void Start()
        {
            _feedbackText.text =
                "Three artifacts sit on a shelf: a golden key, a silver chalice, and an old book. Which one unlocks the next clue?";
            for (int i = 0; i < _artifactButtons.Length; i++)
            {
                int idx = i;
                _artifactButtons[i].onClick.AddListener(() => OnArtifactSelected(idx));
            }
        }

        void OnArtifactSelected(int index)
        {
            if (index == correctButtonIndex)
            {
                _feedbackText.text = "The old book contains a secret passage!";
                _roomController.OnPuzzleSolved(1);
            }
            else
            {
                _feedbackText.text = "Nothing happens...";
            }
        }
    }
}