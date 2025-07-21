using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;
	private static readonly object _lock = new object();
	private static bool _applicationIsQuitting = false;

	public static T Instance
	{
		get
		{
			if (_applicationIsQuitting)
			{
				Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed on application quit.");
				return null;
			}

			lock (_lock)
			{
				if (_instance == null)
				{
					_instance = Object.FindFirstObjectByType<T>();

					if (_instance == null)
					{
						Debug.LogError($"[Singleton] Instance of '{typeof(T)}' not found in the scene.");
					}
				}
				return _instance;
			}
		}
	}

	protected virtual void Awake()
	{
		if (_instance == null)
		{
			_instance = this as T;
		}
		else if (_instance != this)
		{
			Debug.LogWarning($"[Singleton] Duplicate instance of '{typeof(T)}' found. Destroying the new one.");
			Destroy(gameObject);
		}
	}

	protected virtual void OnApplicationQuit()
	{
		_applicationIsQuitting = true;
	}
}