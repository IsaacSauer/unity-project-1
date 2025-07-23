using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class OnScreenLogger : MonoBehaviour
{
	public TextMeshProUGUI loggerText;
	private Queue<string> logMessages = new Queue<string>();
	public int maxMessages = 20;

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
		if (logMessages.Count >= maxMessages)
			logMessages.Dequeue();

		string formattedMessage = FormatLogMessage(logString, type);
		logMessages.Enqueue(formattedMessage);

		loggerText.text = string.Join("\n", logMessages.ToArray());
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