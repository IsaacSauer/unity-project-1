using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;
	private static readonly object _lock = new object();
	private static bool _applicationIsQuitting = false;

	// Optional flags
	[SerializeField] private bool _persistBetweenScenes = true;
	[SerializeField] private bool _enableLogging = true;

	public static T Instance
	{
		get
		{
			if (_applicationIsQuitting)
			{
				Log($"[Singleton] Instance '{typeof(T)}' already destroyed on application quit.");
				return null;
			}

			lock (_lock)
			{
				if (_instance == null)
				{
					_instance = Object.FindFirstObjectByType<T>();
					if (_instance == null)
					{
						// Auto-create instance
						GameObject singletonObject = new GameObject($"{typeof(T)} (Singleton)");
						_instance = singletonObject.AddComponent<T>();
						Log($"[Singleton] Auto-created instance of '{typeof(T)}'.");
					}

					(_instance as Singleton<T>)?.Initialize();
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
			if (_persistBetweenScenes)
				DontDestroyOnLoad(gameObject);

			Initialize();
		}
		else if (_instance != this)
		{
			Log($"[Singleton] Duplicate instance of '{typeof(T)}' found. Destroying the new one.");
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// Override this for post-initialization logic.
	/// </summary>
	protected virtual void Initialize()
	{
	}

	protected virtual void OnDestroy()
	{
		if (_instance == this)
		{
			_instance = null;
		}
	}

	protected virtual void OnApplicationQuit()
	{
		_applicationIsQuitting = true;
	}

	private static void Log(string message)
	{
		if ((_instance as Singleton<T>)?._enableLogging ?? true)
		{
			Debug.Log(message);
		}
	}
}