namespace Infrastructure;

class SimpleDB<K, V> 
    where K : notnull 
    where V : class
{
    private readonly Dictionary<K, V> _repository = new();

    public V? ReadItem(K key)
    {
        if (_repository.ContainsKey(key))
        {
            return _repository[key];
        }
        return null;
    }

    public V PutItem(K key, V item, Boolean update = true)
    {
        if (_repository.ContainsKey(key))
        {
            if (!update)
            {
                return _repository[key];
            } else {
                _repository[key] = item;
                return item;
            }
        } 
        else 
        {
                _repository[key] = item;
                return item;            
        }
    }

    public V? DeleteItem(K key)
    {
        if (_repository.ContainsKey(key))
        {
            var oldValue = _repository[key];
            _repository.Remove(key);
            return oldValue;
        }
        else
        {
            return null;
        }
    }

}
