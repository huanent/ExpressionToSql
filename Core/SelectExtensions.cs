using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

public static class SelectExtensions
{
    public static SqlBuilder<T> Select<T, TResult>(this SqlBuilder<T> builder, Expression<Func<T, TResult>> expression)
    {
        string Analysis(Expression e)
        {
            if (expression.Body is NewExpression newExpression)
            {
                var members = newExpression.Members.Select(s => $"[{s.Name}]");
                return string.Join(',', members);
            }
            else throw new NotImplementedException("未实现此表达式转换");
        }

        builder.AddUnit((nameof(Select),()=>Analysis(expression)));
        return builder;
    }
}