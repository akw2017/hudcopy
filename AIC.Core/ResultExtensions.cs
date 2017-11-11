using System;
using System.Threading.Tasks;

namespace AIC.Core
{
    public static class ResultExtensions
    {
        public static Result<T> ToResult<T>(this Maybe<T> maybe, string errorMessage) where T : class
        {
            if (maybe.HasNoValue)
                return Result.Fail<T>(errorMessage);

            return Result.Ok(maybe.Value);
        }

        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return Result.Ok(func(result.Value));
        }

        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, Result<K>> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return func(result.Value);
        }


        public static Result<K> OnSuccess<K>(this Result result, Func<Result, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return Result.Ok(func(result));
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
        {
            if (result.IsFailure)
                return Result.Fail<T>(result.Error);

            return Result.Ok(func());
        }





        //public static async Task<Result<TResult>> OnSuccessAsync<TSource,TResult>(this Task<Result<TSource>> result, Func<TSource,Task<TResult>> func)
        //{
        //    var sourceResult = await result;
        //    if (sourceResult.IsFailure)
        //        return Result.Fail<TResult>(sourceResult.Error);

        //    var r = await func(sourceResult.Value);
        //    return Result.Ok(r);
        //}

        //public static async Task<Result> OnSuccessAsync(this Result result, Func<Task<Result>> func)
        //{
        //    if (result.IsFailure)
        //        return Result.Fail(result.Error);

        //    return await func();
        //}


        public static async Task<Result> OnSuccessAsync<TSource>(this Task<Result<TSource>> result, Func<TSource, Result> func)
        {
            var sourceResult = await result;
            if (sourceResult.IsFailure)
                return Result.Fail(sourceResult.Error);

            return func(sourceResult.Value);
        }

        public static async Task<Result> OnSuccessAsync(this Task<Result> result, Func<Task<Result>> func)
        {
            var sourceResult = await result;
            if (sourceResult.IsFailure)
                return Result.Fail(sourceResult.Error);

            return await func();
        }

        public static async Task<Result> OnSuccessAsync(this Task<Result> result, Func<Result> func)
        {
            var sourceResult = await result;
            if (sourceResult.IsFailure)
                return Result.Fail(sourceResult.Error);

            return func();
        }

        public static async Task<Result> OnSuccessAsync(this Task<Result> result, Action action)
        {
            var sourceResult = await result;
            if (sourceResult.IsFailure)
                return Result.Fail(sourceResult.Error);

            action();
            return sourceResult;
        }

        public static async Task<Result> OnFailureAsync(this Task<Result> result, Action<Result> action)
        {
            var soruceResult = await result;
            if (soruceResult.IsFailure)
            {
                action(soruceResult);
            }
            return soruceResult;
        }


        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Fail<T>(errorMessage);

            return result;
        }

        public static Result<K> Map<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return Result.Ok(func(result.Value));
        }

        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            if (result.IsFailure)
                return result;

            return func();
        }

        public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
        {
            if (result.IsFailure)
                return result;

            return func(result.Value);
        }


        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action<Result> action)
        {
            if (result.IsFailure)
            {
                action(result);
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }
    }
}
