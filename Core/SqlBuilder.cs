using System;
using System.Collections.Concurrent;
using System.Linq;

public class SqlBuilder<T>
{
    readonly ConcurrentBag<(string category, Func<string> func)> _bag = new ConcurrentBag<(string category, Func<string> func)>();

    public void AddUnit((string category, Func<string> func) unit)
    {
        if (unit.category == nameof(SelectExtensions.Select) && _bag.Any(a => a.category == nameof(SelectExtensions.Select)))
        {
            throw new Exception("不支持多个Select");
        }
        _bag.Add(unit);
    }

    public string Build()
    {
        var selectUnit = _bag.FirstOrDefault(f => f.category == nameof(SelectExtensions.Select));
        var select = selectUnit.func?.Invoke() ?? "*";
        var whereList = _bag.Where(s => s.category == nameof(WhereExtensions.Where)).Select(s => s.func());
        var where = string.Join(" AND ", whereList);

        return $"SELECT {select} FROM {typeof(T).Name} WHERE {where}";
    }
}