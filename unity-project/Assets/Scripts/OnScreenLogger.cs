#if DEBUG
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class OnScreenLogger : MonoBehaviour
{
	public TextMeshProUGUI _loggerText;
	private Queue<string> _logMessages = new Queue<string>();
	public int _maxMessages = 20;

	void OnEnable()
	{
		Application.logMessageReceived += HandleLog;
	}

	void OnDisable()
	{
		Application.logMessageReceived -= HandleLog;
	}

	void HandleLog(string logString, string stackTrace, LogType type)
	{
		if (_logMessages.Count >= _maxMessages)
			_logMessages.Dequeue();

		string formattedMessage = FormatLogMessage(logString, type);
		_logMessages.Enqueue(formattedMessage);

		_loggerText.text = string.Join("\n", _logMessages.ToArray());
	}

	string FormatLogMessage(string message, LogType type)
	{
		switch (type)
		{
			case LogType.Warning:
				return $"<color=yellow>{message}</color>";
			case LogType.Error:
			case LogType.Exception:
				return $"<color=red>{message}</color>";
			case LogType.Assert:
				return $"<color=orange>{message}</color>";
			default:
				return $"<color=white>{message}</color>";
		}
	}
}
#endif