
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoodreadsAPI.Internal;
using GoodreadsAPI.Internal.Models;
using GoodreadsAPI.Models;

namespace GoodreadsAPI.Impl
{
    class RateLimitedGoodreadsApi : InternalGoodreadsApiBase, IDisposable
    {
        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();
        private readonly SemaphoreSlim _authorSemaphore;
        private readonly SemaphoreSlim _authorBooksSemaphore;
        private readonly SemaphoreSlim _bookSemaphore;
        private readonly SemaphoreSlim _seriesSemaphore;
        private readonly SemaphoreSlim _workBooksSemaphore;
        private readonly Task _semaphoreRefillTask;

        public RateLimitedGoodreadsApi(string developerKey, int maxQueryRate = 1) : base(developerKey)
        {
            _authorSemaphore = new SemaphoreSlim(maxQueryRate, maxQueryRate);
            _authorBooksSemaphore = new SemaphoreSlim(maxQueryRate, maxQueryRate);
            _bookSemaphore = new SemaphoreSlim(maxQueryRate, maxQueryRate);
            _seriesSemaphore = new SemaphoreSlim(maxQueryRate, maxQueryRate);
            _workBooksSemaphore = new SemaphoreSlim(maxQueryRate, maxQueryRate);

            _semaphoreRefillTask = Task.Run(async delegate
            {
                var token = _cancellation.Token;
                var semaphores = new SemaphoreSlim[]
                {
                    _authorSemaphore, _authorBooksSemaphore, _bookSemaphore,
                    _seriesSemaphore, _workBooksSemaphore
                };
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(1000, token);

                    foreach (var semaphore in semaphores)
                    {
                        try
                        {
                            semaphore.Release(maxQueryRate);
                        }
                        catch (SemaphoreFullException)
                        {
                        }
                    }
                }
            });
        }

        public override async Task<AuthorDto> GetInternalAuthor(int authorId)
        {
            await _authorSemaphore.WaitAsync(_cancellation.Token);
            return await base.GetInternalAuthor(authorId);
        }

        public override async Task<BookDto> GetInternalBook(int bookId)
        {
            await _bookSemaphore.WaitAsync(_cancellation.Token);
            return await base.GetInternalBook(bookId);
        }

        public override async Task<Tuple<int, IEnumerable<IBook>>> LoadBooksForAuthor(int authorId, int page = 1)
        {
            await _authorBooksSemaphore.WaitAsync(_cancellation.Token);
            return await base.LoadBooksForAuthor(authorId, page);
        }

        public override async Task<Tuple<int, IEnumerable<IBook>>> LoadBooksForWork(int workId, int page = 1)
        {
            await _workBooksSemaphore.WaitAsync(_cancellation.Token);
            return await base.LoadBooksForWork(workId, page);
        }

        public override async Task<SeriesDto> GetInternalSeries(int seriesId)
        {
            await _seriesSemaphore.WaitAsync(_cancellation.Token);
            return await base.GetInternalSeries(seriesId);
        }

        public void Dispose()
        {
            _cancellation.Cancel();
            _semaphoreRefillTask?.Dispose();
        }
    }
}
