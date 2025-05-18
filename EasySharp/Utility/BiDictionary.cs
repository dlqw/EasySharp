namespace EasySharp.Utility;

/// <summary>
/// 双向字典，支持通过键查值和通过值查键
/// </summary>
/// <typeparam name="TFirst">第一类型（键）</typeparam>
/// <typeparam name="TSecond">第二类型（值）</typeparam>
public class BiDictionary<TFirst, TSecond> : IEnumerable<KeyValuePair<TFirst, TSecond>>
    where TFirst : notnull
    where TSecond : notnull
{
    private readonly Dictionary<TFirst, TSecond> _firstToSecond;
    private readonly Dictionary<TSecond, TFirst> _secondToFirst;
    
    /// <summary>
    /// 获取字典中的条目数
    /// </summary>
    public int Count => _firstToSecond.Count;
    
    /// <summary>
    /// 获取所有第一类型键的集合
    /// </summary>
    public IEnumerable<TFirst> FirstKeys => _firstToSecond.Keys;
    
    /// <summary>
    /// 获取所有第二类型键的集合
    /// </summary>
    public IEnumerable<TSecond> SecondKeys => _secondToFirst.Keys;
    
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public BiDictionary() : this(0, null, null) { }
    
    /// <summary>
    /// 指定初始容量的构造函数
    /// </summary>
    /// <param name="capacity">初始容量</param>
    public BiDictionary(int capacity) : this(capacity, null, null) { }
    
    /// <summary>
    /// 指定比较器的构造函数
    /// </summary>
    /// <param name="firstComparer">第一类型的比较器</param>
    /// <param name="secondComparer">第二类型的比较器</param>
    public BiDictionary(IEqualityComparer<TFirst>? firstComparer, IEqualityComparer<TSecond>? secondComparer)
        : this(0, firstComparer, secondComparer) { }
    
    /// <summary>
    /// 指定初始容量和比较器的构造函数
    /// </summary>
    /// <param name="capacity">初始容量</param>
    /// <param name="firstComparer">第一类型的比较器</param>
    /// <param name="secondComparer">第二类型的比较器</param>
    public BiDictionary(int capacity, IEqualityComparer<TFirst>? firstComparer, IEqualityComparer<TSecond>? secondComparer)
    {
        _firstToSecond = new Dictionary<TFirst, TSecond>(capacity, firstComparer);
        _secondToFirst = new Dictionary<TSecond, TFirst>(capacity, secondComparer);
    }
    
    /// <summary>
    /// 添加键值对
    /// </summary>
    /// <param name="first">第一类型键</param>
    /// <param name="second">第二类型值</param>
    /// <exception cref="ArgumentException">当键或值已存在于字典中时抛出</exception>
    public void Add(TFirst first, TSecond second)
    {
        if (_firstToSecond.ContainsKey(first))
            throw new ArgumentException($"键已存在: {first}");
        
        if (_secondToFirst.ContainsKey(second))
            throw new ArgumentException($"值已存在: {second}");
        
        _firstToSecond.Add(first, second);
        _secondToFirst.Add(second, first);
    }
    
    /// <summary>
    /// 尝试添加键值对，如果键或值已存在则返回false
    /// </summary>
    /// <param name="first">第一类型键</param>
    /// <param name="second">第二类型值</param>
    /// <returns>添加成功返回true，否则返回false</returns>
    public bool TryAdd(TFirst first, TSecond second)
    {
        if (_firstToSecond.ContainsKey(first) || _secondToFirst.ContainsKey(second))
            return false;
        
        _firstToSecond.Add(first, second);
        _secondToFirst.Add(second, first);
        return true;
    }
    
    /// <summary>
    /// 通过第一类型键查找第二类型值
    /// </summary>
    /// <param name="first">第一类型键</param>
    /// <param name="second">输出第二类型值</param>
    /// <returns>找到返回true，否则返回false</returns>
    public bool TryGetByFirst(TFirst first, out TSecond second)
    {
        return _firstToSecond.TryGetValue(first, out second!);
    }
    
    /// <summary>
    /// 通过第二类型键查找第一类型值
    /// </summary>
    /// <param name="second">第二类型键</param>
    /// <param name="first">输出第一类型值</param>
    /// <returns>找到返回true，否则返回false</returns>
    public bool TryGetBySecond(TSecond second, out TFirst first)
    {
        return _secondToFirst.TryGetValue(second, out first!);
    }
    
    /// <summary>
    /// 检查是否包含指定的第一类型键
    /// </summary>
    /// <param name="first">要检查的第一类型键</param>
    /// <returns>包含返回true，否则返回false</returns>
    public bool ContainsFirst(TFirst first) => _firstToSecond.ContainsKey(first);
    
    /// <summary>
    /// 检查是否包含指定的第二类型键
    /// </summary>
    /// <param name="second">要检查的第二类型键</param>
    /// <returns>包含返回true，否则返回false</returns>
    public bool ContainsSecond(TSecond second) => _secondToFirst.ContainsKey(second);
    
    /// <summary>
    /// 通过第一类型键移除项
    /// </summary>
    /// <param name="first">要移除的第一类型键</param>
    /// <returns>成功移除返回true，否则返回false</returns>
    public bool Remove(TFirst first)
    {
        if (!_firstToSecond.TryGetValue(first, out var second))
            return false;
        
        _firstToSecond.Remove(first);
        _secondToFirst.Remove(second);
        return true;
    }
    
    /// <summary>
    /// 通过第二类型键移除项
    /// </summary>
    /// <param name="second">要移除的第二类型键</param>
    /// <returns>成功移除返回true，否则返回false</returns>
    public bool RemoveBySecond(TSecond second)
    {
        if (!_secondToFirst.TryGetValue(second, out var first))
            return false;
        
        _secondToFirst.Remove(second);
        _firstToSecond.Remove(first);
        return true;
    }
    
    /// <summary>
    /// 清空字典
    /// </summary>
    public void Clear()
    {
        _firstToSecond.Clear();
        _secondToFirst.Clear();
    }
    
    /// <summary>
    /// 通过第一类型键获取第二类型值
    /// </summary>
    /// <param name="first">第一类型键</param>
    /// <returns>对应的第二类型值</returns>
    /// <exception cref="KeyNotFoundException">当键不存在时抛出</exception>
    public TSecond this[TFirst first]
    {
        get
        {
            if (_firstToSecond.TryGetValue(first, out var second))
                return second;
            throw new KeyNotFoundException($"未找到键: {first}");
        }
        set
        {
            if (_firstToSecond.TryGetValue(first, out var oldSecond))
            {
                if (EqualityComparer<TSecond>.Default.Equals(oldSecond, value))
                    return;
                
                _secondToFirst.Remove(oldSecond);
            }
            
            if (_secondToFirst.ContainsKey(value))
                throw new ArgumentException($"值已存在: {value}");
            
            _firstToSecond[first] = value;
            _secondToFirst[value] = first;
        }
    }
    
    /// <summary>
    /// 通过第二类型键获取第一类型值
    /// </summary>
    /// <param name="second">第二类型键</param>
    /// <returns>对应的第一类型值</returns>
    /// <exception cref="KeyNotFoundException">当键不存在时抛出</exception>
    public TFirst this[TSecond second]
    {
        get
        {
            if (_secondToFirst.TryGetValue(second, out var first))
                return first;
            throw new KeyNotFoundException($"未找到键: {second}");
        }
        set
        {
            if (_secondToFirst.TryGetValue(second, out var oldFirst))
            {
                if (EqualityComparer<TFirst>.Default.Equals(oldFirst, value))
                    return;
                
                _firstToSecond.Remove(oldFirst);
            }
            
            if (_firstToSecond.ContainsKey(value))
                throw new ArgumentException($"键已存在: {value}");
            
            _secondToFirst[second] = value;
            _firstToSecond[value] = second;
        }
    }
    
    /// <summary>
    /// 获取枚举器
    /// </summary>
    public IEnumerator<KeyValuePair<TFirst, TSecond>> GetEnumerator() => _firstToSecond.GetEnumerator();
    
    /// <summary>
    /// 获取非泛型枚举器
    /// </summary>
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}