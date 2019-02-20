### 初步实现转换
* where
* select

表达式
```
var builder = new SqlBuilder<TestClass>();
            
            builder
            .Where(a => a.Id > 18 && (a.Name != "tom" || a.Name=="job"))
            .Select(s => new
            {
                s.Name
            });

            var sql = builder.Build();
            Console.WriteLine(sql);
```
转换后
```
SELECT [Name] FROM TestClass WHERE (([Id] > 18)  AND (([Name] <> 'tom')  OR ([Name] = 'job') ) )
```