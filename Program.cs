using System;

namespace ExpressionToSql
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new SqlBuilder<TestClass>();
            
            builder
            .Where(a => a.Id > 18 && (a.Name != "tom" || a.Name=="job"))
            .Select(s => new
            {
                s.Name
            });

            var sql = builder.Build();
            Console.WriteLine(sql);
        }
    }
}