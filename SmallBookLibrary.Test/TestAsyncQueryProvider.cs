using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace SmallBookLibrary.Test
{
    public class TestAsyncQueryProvider<T> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        public TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public IQueryable CreateQuery(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            Type elementType = expression.Type.GetGenericArguments().First();
            Type queryType = typeof(TestAsyncEnumerable<>).MakeGenericType(elementType);

            return (IQueryable)Activator.CreateInstance(queryType, new object[] { expression });
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return null;// new TestAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return _inner.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var resultType = typeof(TResult).GetGenericArguments()[0];
            var executeAsyncMethod = typeof(IQueryProvider)
                .GetMethod(nameof(IAsyncQueryProvider.ExecuteAsync))
                .MakeGenericMethod(resultType);

            return (TResult)executeAsyncMethod.Invoke(_inner, new object[] { expression, cancellationToken });
        }
    }
}
