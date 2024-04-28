using System.Collections;
using System.Linq.Expressions;

namespace SmallBookLibrary.Test
{
    public class TestAsyncEnumerable<T> : IAsyncEnumerable<T>, IQueryable<T>
    {
        private readonly IEnumerable<T> _source;

        public TestAsyncEnumerable(IEnumerable<T> source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new TestAsyncEnumerator<T>(_source.GetEnumerator());
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => _source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _source.GetEnumerator();

        Type IQueryable.ElementType => typeof(T);

        Expression IQueryable.Expression => _source.AsQueryable().Expression;

        IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(_source.AsQueryable().Provider);
    }
}
