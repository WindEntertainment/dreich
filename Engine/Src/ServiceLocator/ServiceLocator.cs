namespace Wind {
  public class ServiceLocator {
    private readonly Dictionary<Type, object> _services = [];
    private readonly Dictionary<Type, Action> _registerCallbacks = [];

    public void Register<T>(T service) where T : notnull {
      _services.Add(typeof(T), service);

      if (_registerCallbacks.ContainsKey(typeof(T))) {
        _registerCallbacks[typeof(T)]?.Invoke();
        _registerCallbacks.Remove(typeof(T));
      }
    }

    public void SetCallback<T>(Action callback) {
      if (_registerCallbacks.ContainsKey(typeof(T)))
        _registerCallbacks[typeof(T)] += callback;
      else
        _registerCallbacks.Add(typeof(T), callback);
    }

    public bool Contains<T>() where T : notnull {
      return _services.ContainsKey(typeof(T));
    }

    public T Get<T>() where T : notnull {
      return (T)_services[typeof(T)];
    }
  }
}